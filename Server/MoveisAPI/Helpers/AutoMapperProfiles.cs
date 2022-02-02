using AutoMapper;
using DatabaseP.models;
using Microsoft.AspNetCore.Identity;
using MoveisAPI.DTOs;
using MoveisAPI.DTOs.Rating;
using MoveisAPI.DTOs.Security;

namespace MoveisAPI.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles(/*GeometryFactory geometryFactory*/)
        {
            CreateMap<GenreDTO, Genre>().ReverseMap();
            CreateMap<GenreCreationDTO, Genre>();

            CreateMap<ActorDTO, Actor>().ReverseMap();
            CreateMap<ActorCreationDTO, Actor>().ForMember(x => x.Picture, opt => opt.Ignore());
            CreateMap<ActorsMovieDTO, Actor>()
                .ForMember(x => x.DateOfBirth, opt => opt.Ignore())
                .ForMember(x => x.Biography, opt => opt.Ignore()).ReverseMap();

            CreateMap<MovieTheater, MovieTheaterDTO>().ReverseMap();
            /*CreateMap<MovieTheater, MovieTheaterDTO>().ForMeber(x => x.Latitude, dto => dto.MapFrom(prop => prop.Location.Y))
            .ForMeber(x => x.Longitude, dto => dto.MapFrom(prop => prop.Location.X));*/          
            CreateMap<MovieTheaterCreationDTO, MovieTheater>();
            /*CreateMap<MovieTheaterCreationDTO, MovieTheater>()
             .ForMeber(x => x.Location, x => x.MapFrom(dto => geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))));*/

            CreateMap<MovieCreationDTO, Movie>()
                .ForMember(x => x.Poster, options => options.Ignore())
                .ForMember(x => x.MoviesGenres, options => options.MapFrom(MapMoviesGenres))
                .ForMember(x => x.MovieTheatersMovies, options => options.MapFrom(MapMoviesTheaters))
                .ForMember(x => x.MoviesActors, options => options.MapFrom(MapMoviesActors)).ReverseMap();

            CreateMap<Movie, MovieDTO>()
                .ForMember(x => x.Genres, options => options.MapFrom(MapMoviesGenres))
                .ForMember(x => x.MovieTheaters, options => options.MapFrom(MapMoviesTheaters))
                .ForMember(x => x.Actors, options => options.MapFrom(MapMoviesActors));

            CreateMap<RatingDTO, Rating>().ReverseMap();

            CreateMap<IdentityUser, UserDTO>();
               
        }

        private List<ActorsMovieDTO> MapMoviesActors(Movie movie, MovieDTO movieDTO)
        {
            var result = new List<ActorsMovieDTO>();

            if(movie.MoviesActors != null)
            {
                foreach(var moviesActors in movie.MoviesActors)
                {
                    result.Add(new ActorsMovieDTO()
                    {
                        Id = moviesActors.ActorId,
                        Name = moviesActors.Actor.Name,
                        Character = moviesActors.Character,
                        Picture = moviesActors.Actor.Picture,
                        Order = moviesActors.Order
                    });
                }
            }

            return result;
        }

        private List<GenreDTO> MapMoviesGenres(Movie movie, MovieDTO movieDTO)
        {
            var result = new List<GenreDTO>();

            if(movie.MoviesGenres != null)
            {
                foreach (var genre in movie.MoviesGenres)
                {
                    result.Add(new GenreDTO() { Id = genre.GenreId, Name = genre.Genre.Name });
                }
            }

            return result;
        }

        private List<MovieTheaterDTO> MapMoviesTheaters(Movie movie, MovieDTO movieDTO)
        {
            var result = new List<MovieTheaterDTO>();

            if(movie.MovieTheatersMovies != null)
            {
                foreach(var movieTheatreMovies in movie.MovieTheatersMovies)
                {
                    result.Add(new MovieTheaterDTO() { Id = movieTheatreMovies.MovieTheaterId, Name = movieTheatreMovies.MovieTheater.Name});
                }
            }

            return result;
        }
        private List<MoviesGenres> MapMoviesGenres(MovieCreationDTO movieCreationDTO, Movie movie)
        {
            var result = new List<MoviesGenres>();

            if(movieCreationDTO.GenresIds == null) { return result; }
            
            foreach(var id in movieCreationDTO.GenresIds)
            {
                result.Add(new MoviesGenres() { GenreId = id });
            }
            return result;
        }

        private List<MovieTheatersMovies> MapMoviesTheaters(MovieCreationDTO movieCreationDTO, Movie movie)
        {
            var result = new List<MovieTheatersMovies>();

            if (movieCreationDTO.movieTheatersIds == null) { return result; }

            foreach (var id in movieCreationDTO.movieTheatersIds)
            {
                result.Add(new MovieTheatersMovies() { MovieTheaterId = id });
            }
            return result;
        }

        private List<MoviesActors> MapMoviesActors(MovieCreationDTO movieCreationDTO, Movie movie)
        {
            var result = new List<MoviesActors>();

            if (movieCreationDTO.Actors == null) { return result; }

            foreach (var actor in movieCreationDTO.Actors)
            {
                result.Add(new MoviesActors() { ActorId = actor.Id, Character = actor.Character });
            }
            return result;
        }
    }
}
