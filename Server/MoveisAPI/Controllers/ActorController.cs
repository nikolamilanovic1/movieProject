using AutoMapper;
using DatabaseP.models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveisAPI.DTOs;
using MoveisAPI.Helpers;
using ServicesP.Implementation;
using ServicesP.Implementation.Interface;

namespace MoveisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
    public class ActorController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IActorService _actorService;
        private readonly IFileStorageService _fileStorageService;
        private readonly string containerName = "actors";

        public ActorController(IMapper mapper, IActorService actorService, IFileStorageService fileStorageService)
        {
           _mapper = mapper;
           _actorService = actorService;
           _fileStorageService = fileStorageService;
        }

        [HttpGet] //getAll
        public async Task<ActionResult<List<ActorDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = _actorService.GetQueryable();
            await HttpContext.InsertParametarsPaginationInHeader(queryable);
            var actor = await queryable.OrderBy(x => x.Name).Paginate(paginationDTO).ToListAsync();
            return _mapper.Map<List<ActorDTO>>(actor);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var actor = await _actorService.GetActorById(id);
            return (actor == null) ?  NotFound() : _mapper.Map<ActorDTO>(actor);       
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreationDTO actorCreationDTO)
        {
            var actor = _mapper.Map<Actor>(actorCreationDTO);
            if(actorCreationDTO.Picture != null)
            {
                actor.Picture = await _fileStorageService.SaveFile(containerName, actorCreationDTO.Picture);
            }
            await _actorService.addActor(actor);
            return NoContent();
        }

        [HttpPost("searchByName")]
        public async Task<ActionResult<List<ActorsMovieDTO>>> SearchByName([FromBody]string name)
        {
            if(string.IsNullOrWhiteSpace(name)) { return new List<ActorsMovieDTO>(); }
            var actors = await _actorService.GetAllActorsByName(name);
            return _mapper.Map<List<ActorsMovieDTO>>(actors);
        }

        [HttpPut ("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorCreationDTO actorCreationDTO)
        {
            //ovo ne mora ovako, nego namapiras tog sto je dosao, proveris mu sliku pa puknes na taj entitet u kontroleru
            var actor = await _actorService.GetActorById(id);
            if (actor == null)
            { 
                return NotFound(); 
            } 
            //ovaj gore deo koda je nepotreban
            actor = _mapper.Map(actorCreationDTO,actor);

            if(actorCreationDTO.Picture != null)
            {
                actor.Picture = await _fileStorageService.EditFile(containerName, actorCreationDTO.Picture, actor.Picture);
            }
            await _actorService.SaveActor();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var actor = await _actorService.GetActorById(id);

            if (actor == null) return NotFound();

            await _actorService.DeleteActor(id);
            await _fileStorageService.DeleteFile(actor.Picture, containerName);
            return NoContent();
        }
    }
}
