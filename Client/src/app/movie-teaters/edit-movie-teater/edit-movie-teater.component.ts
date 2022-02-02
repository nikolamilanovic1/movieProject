import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { movieTeatersCreationDTO, movieTeatersDTO } from '../movie-teaters.model';
import { MovieTeatersService } from '../movie-teaters.service';

@Component({
  selector: 'app-edit-movie-teater',
  templateUrl: './edit-movie-teater.component.html',
  styleUrls: ['./edit-movie-teater.component.css']
})
export class EditMovieTeaterComponent implements OnInit {

  constructor(private activatedRoute:ActivatedRoute, private movieTheaterService: MovieTeatersService, private router:Router) { }

  model!: movieTeatersDTO;

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params=>{
        this.movieTheaterService.getById(params['id']).subscribe(movieTheater => this.model = movieTheater)
    });
  }

  saveChanges(movieTeater: movieTeatersCreationDTO){
    this.movieTheaterService.edit(this.model.id, movieTeater).subscribe(()=>this.router.navigate(['/movieteaters']))
  }

}
