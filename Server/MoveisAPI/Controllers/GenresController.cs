using AutoMapper;
using DatabaseP.models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoveisAPI.DTOs;
using ServicesP.Interface;


namespace MoveisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]

    public class GenresController : ControllerBase
    {
        private readonly ILogger<GenresController> _logger;
        private readonly IMapper _mapper;
        private readonly IGenreService _genreService;
        public GenresController(ILogger<GenresController> logger, IMapper mapper, IGenreService genreService)
        {
            _logger = logger;
            _mapper = mapper;
            _genreService = genreService;
        }

        [HttpGet] // api/genres
        [AllowAnonymous]
        public async Task<ActionResult<List<GenreDTO>>> Get()
        {
            _logger.LogInformation("Getting all the genres");
            var genres = await _genreService.GetAllGenres();
            return _mapper.Map<List<GenreDTO>>(genres);
        }

        [HttpGet("{Id:int}")] // api/genres/
        public async Task<ActionResult<GenreDTO>> Get(int Id)
        {
            var genre = await _genreService.GetGenreById(Id);

            return (genre == null) ? NotFound() : _mapper.Map<GenreDTO>(genre); 
                     
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GenreCreationDTO genreCreationDTO)
        {
            var genre = _mapper.Map<Genre>(genreCreationDTO);
            await _genreService.AddGenre(genre); 
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] GenreCreationDTO genreCreationDTO)
        {
            var genre = _mapper.Map<Genre>(genreCreationDTO)?? throw new ArgumentNullException(nameof(Genre));
            await _genreService.UpdateGenre(id, genre);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var genre = await _genreService.GetGenreById(Id);

            if(genre == null) return NotFound();
            
            await _genreService.DeleteGenre(Id);
            return NoContent();
        }
    }
}
