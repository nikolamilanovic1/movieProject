import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { actorCreationDTO, actorsMovieDTO } from 'src/app/actors/actor.model';
import { multipleSelectorModel } from 'src/app/utilities/multiple-selector/multiple-selector.model';
import { movieCreationDTO, movieDTO } from '../movies.model';
import { MoviesService } from '../movies.service';

@Component({
  selector: 'app-edit-movie',
  templateUrl: './edit-movie.component.html',
  styleUrls: ['./edit-movie.component.css']
})
export class EditMovieComponent implements OnInit {

  constructor(private activatedRoute:ActivatedRoute, private moviesService:MoviesService, private router:Router) { }

  model!: movieDTO;

  selectedGenres!: multipleSelectorModel[];
  nonSelectedGenres!: multipleSelectorModel[];
  selectedMovieTheaters!: multipleSelectorModel[];
  nonSelectedMovieTheaters!: multipleSelectorModel[];
  selectedActors!: actorsMovieDTO[];

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params=>{
      this.moviesService.putGet(params['id']).subscribe(putGetDto => {
        this.model = putGetDto.movie;

        this.selectedGenres = putGetDto.selectedGenres.map(genre => {
          return <multipleSelectorModel>{key: genre.id, value: genre.name};
        });

        this.nonSelectedGenres = putGetDto.nonSelectedGenres.map(genre => {
          return <multipleSelectorModel>{key: genre.id, value: genre.name};
        });

        this.selectedMovieTheaters = putGetDto.selectedMovieTheaters.map(movieTheater => {
          return <multipleSelectorModel>{key: movieTheater.id, value: movieTheater.name}
        });

        this.nonSelectedMovieTheaters = putGetDto.nonSelectedMovieTheaters.map(movieTheater => {
          return <multipleSelectorModel>{key: movieTheater.id, value: movieTheater.name}
        });

        this.selectedActors = putGetDto.actors;

        console.log(this.model)
        console.log(this.selectedActors);
        console.log(this.nonSelectedGenres)

      })
    })
  }

  saveChanges(movieCreationDTO: movieCreationDTO){
    this.moviesService.edit(this.model.id, movieCreationDTO).subscribe(()=>{
      this.router.navigate(['/movie/'+this.model.id]);
    });
  }

}
