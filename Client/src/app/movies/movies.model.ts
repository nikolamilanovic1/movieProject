import { actorsMovieDTO } from "../actors/actor.model";
import { genreDTO } from "../genres/genre.model";
import { movieTeatersDTO } from "../movie-teaters/movie-teaters.model";

export interface movieCreationDTO{
  title:string;
  summary: any; //pri slanju se moze desiti da je undefined ne znam sto {string}
  poster: any; //moze da se salje i FILE i string da se cita pri primanju {File}
  inTheaters: boolean;
  releaseDate: Date;
  trailer: string;
  genresIds: number[];
  movieTheatersIds: number[];
  actors: actorsMovieDTO[];
}

export interface movieDTO{
  id: number;
  title:string;
  summary: any; //pri slanju se moze desiti da je undefined ne znam sto {string}
  poster: any; //moze da se salje i FILE i string da se cita pri primanju {string}
  inTheaters: boolean;
  releaseDate: Date;
  trailer: string;
  genres: genreDTO[];
  movieTheaters: movieTeatersDTO[];
  actors: actorsMovieDTO[];
  averageVote: number;
  userVote: number;
}

export interface MoviePostGetDTO{
  genres: genreDTO[];
  movieTheaters: movieTeatersDTO[];
}

export interface homeDTO{
  inTheatres: movieCreationDTO[];
  upcomingReleases: movieCreationDTO[];
}

export interface MoviePutGetDTO{
  movie: movieDTO;
  selectedGenres: genreDTO[];
  nonSelectedGenres: genreDTO[];
  selectedMovieTheaters: movieTeatersDTO[];
  nonSelectedMovieTheaters: movieTeatersDTO[];
  actors: actorsMovieDTO[];
}
