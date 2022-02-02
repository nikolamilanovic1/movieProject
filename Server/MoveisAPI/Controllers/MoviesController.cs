using AutoMapper;
using DatabaseP.models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoveisAPI.DTOs;
using MoveisAPI.Helpers;
using ServicesP.Implementation;
using ServicesP.Implementation.Interface;
using ServicesP.Interface;

namespace MoveisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
    public class MoviesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMovieService _movieService;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMovieTheaterService _movieTheaterService;
        private readonly IGenreService _genreService;
        private readonly IRatingService _ratingService;
        private readonly UserManager<IdentityUser> _userManager;
        private string container = "movies";

        public MoviesController(IMovieService movieService, IMapper mapper, IFileStorageService fileStorageService,
            IMovieTheaterService movieTheaterService, IGenreService genreService, IRatingService ratingService, UserManager<IdentityUser> userManager)
        {
            _movieService = movieService;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
            _movieTheaterService = movieTheaterService;
            _genreService = genreService;
            _ratingService = ratingService;
            _userManager = userManager;
        }

        [HttpGet("PostGet")]
        public async Task<ActionResult<MoviePostGetDTO>> PostGet()
        {
            var movieTheaters = await _movieTheaterService.GetAllMovieTheaters();
            var genres = await _genreService.GetAllGenres();

            var moveTheatersDTO = _mapper.Map<List<MovieTheaterDTO>>(movieTheaters);
            var genresDTO = _mapper.Map<List<GenreDTO>>(genres);

            return new MoviePostGetDTO() { Genres = genresDTO, MovieTheaters = moveTheatersDTO };
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<HomeDTO>> Get()
        {
            var top = 6;
            var today = DateTime.Today;

            var upcomingReleases = await _movieService.GetAllMoviesByReleaseDate(today, top);
            var inTheaters = await _movieService.GetAllMoviesInTheaters(top);

            var homeDTO = new HomeDTO();
            homeDTO.UpcomingReleases = _mapper.Map<List<MovieDTO>>(upcomingReleases);
            homeDTO.InTheatres = _mapper.Map<List<MovieDTO>>(inTheaters);
            return homeDTO;
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<MovieDTO>> Get(int id)
        {
            var movie = await _movieService.GetMovieById(id);

            if (movie == null)
            {
                return NotFound();
            }

            var avregeVote = 0.0;
            var userVote = 0;

            avregeVote = await _ratingService.avergeRate(id);

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
                var user = await _userManager.FindByEmailAsync(email);
                var userId = user.Id;

                userVote = await _ratingService.userRate(id,userId);
            }

            var dto = _mapper.Map<MovieDTO>(movie);
            dto.AverageVote = avregeVote;
            dto.UserVote = userVote;
            dto.Actors = dto.Actors.OrderBy(x => x.Order).ToList();
            return dto;

        }

        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<ActionResult<List<MovieDTO>>> Filter([FromQuery] FilterMoviesDTO filterMoviesDTO)
        {
            var moviesQueryable = _movieService.GetQueryable();

            if (!string.IsNullOrEmpty(filterMoviesDTO.Title))
            {
                moviesQueryable = moviesQueryable.Where(x => x.Title.Contains(filterMoviesDTO.Title));
            }

            if (filterMoviesDTO.inTheaters)
            {
                moviesQueryable = moviesQueryable.Where(x => x.InTheaters == true);
            }

            if (filterMoviesDTO.UpcomingReleases)
            {
                var today = DateTime.Today;
                moviesQueryable = moviesQueryable.Where(x => x.ReleaseDate > today);
            }

            if(filterMoviesDTO.GenreId != 0)
            {
                moviesQueryable = moviesQueryable.Where(x => x.MoviesGenres.Select(y=>y.GenreId).Contains(filterMoviesDTO.GenreId));
            }

            await HttpContext.InsertParametarsPaginationInHeader(moviesQueryable);
            var movies = await moviesQueryable.OrderBy(x => x.Title).Paginate(filterMoviesDTO.PaginationDTO).ToListAsync();
            return _mapper.Map<List<MovieDTO>>(movies);
        }

        [HttpGet("putget/{id:int}")]
        public async Task<ActionResult<MoviePutGetDTO>> PutGet(int id)
        {
            var movieActionResult = await Get(id);
            if (movieActionResult.Result is NotFoundResult)
            {
                return NotFound();
            }

            var movie = movieActionResult.Value;

            var genresSelectedId = movie.Genres.Select(x => x.Id).ToList();
            var nonSelectedGenres = await _genreService.MovieGenre(genresSelectedId);

            var movieTheatersIds = movie.MovieTheaters.Select(x => x.Id).ToList();
            var nonSelectedMovieTheaters = await _movieTheaterService.MovieTheater(movieTheatersIds);

            var nonSelectedGenresDTOs = _mapper.Map<List<GenreDTO>>(nonSelectedGenres);
            var nonSelectedMovieTheatersDTOs = _mapper.Map<List<MovieTheaterDTO>>(nonSelectedMovieTheaters);

            var response = new MoviePutGetDTO();
            response.Movie = movie;
            response.SelectedGenres = movie.Genres;
            response.NonSelectedGenres = nonSelectedGenresDTOs;
            response.SelectedMovieTheaters = movie.MovieTheaters;
            response.NonSelectedMovieTheaters = nonSelectedMovieTheatersDTOs;
            response.Actors = movie.Actors;
            return response;
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] MovieCreationDTO movieCreationDTO)
        {
            var movie = await _movieService.GetMovieById(id);

            if (movie == null) 
            {
                return NotFound();
            } 
            
            movie = _mapper.Map(movieCreationDTO, movie);

            if(movieCreationDTO.Poster != null)
            {
                movie.Poster = await _fileStorageService.EditFile(container, movieCreationDTO.Poster, movie.Poster);
            }

            await _movieService.UpdateMovie(movie);

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<int>> Post([FromForm] MovieCreationDTO movieCreationDTO)
        {
            var movie = _mapper.Map<Movie>(movieCreationDTO);

            if(movieCreationDTO.Poster != null)
            {
                movie.Poster = await _fileStorageService.SaveFile(container, movieCreationDTO.Poster);
            }

            await _movieService.AddMovie(movie);
            return movie.Id;
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var movie = await _movieService.GetMovieById(id);
            if(movie == null)
            {
                return NotFound();
            }
            await _movieService.Delete(movie);
            await _fileStorageService.DeleteFile(movie.Poster, container);
            return NoContent();
        }



    }
}
