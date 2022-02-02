using AutoMapper;
using DatabaseP.models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoveisAPI.DTOs;
using ServicesP.Implementation.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoveisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]

    public class MovieTheatersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMovieTheaterService _movieTheaterService;

        public MovieTheatersController(IMapper mapper, IMovieTheaterService movieTheaterService)
        {
            _mapper = mapper;
            _movieTheaterService = movieTheaterService;
        }

        // GET: api/<MovieTheatersController>
        [HttpGet]
        public async Task<ActionResult<List<MovieTheaterDTO>>> Get()
        {
            var entities = await _movieTheaterService.GetAllMovieTheaters();
            return _mapper.Map<List<MovieTheaterDTO>>(entities);
        }

        // GET api/<MovieTheatersController>/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieTheaterDTO>> Get(int id)
        {
            var movieTheater = await _movieTheaterService.GetMovieTheaterById(id);
            return (movieTheater == null) ? NotFound() : _mapper.Map<MovieTheaterDTO>(movieTheater);
        }

        // POST api/<MovieTheatersController>
        [HttpPost]
        public async Task<ActionResult> Post(MovieTheaterCreationDTO movieTheaterCreation)
        {
            var movieTheater = _mapper.Map<MovieTheater>(movieTheaterCreation);
            await _movieTheaterService.AddMovieTheater(movieTheater);
            return NoContent();
        }

        // PUT api/<MovieTheatersController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,MovieTheaterCreationDTO movieTheaterCreationDTO)
        {
            var movieTheate = _mapper.Map<MovieTheater>(movieTheaterCreationDTO) ?? throw new ArgumentNullException(nameof(MovieTheater));
            await _movieTheaterService.UpdateMovieTheater(id, movieTheate);
            return NoContent();
        }

        // DELETE api/<MovieTheatersController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var movieTheater = await _movieTheaterService.GetMovieTheaterById(id);

            if (movieTheater == null) return NotFound();

            await _movieTheaterService.DeleteMovieTheater(id);
            return NoContent();
        }
    }
}
