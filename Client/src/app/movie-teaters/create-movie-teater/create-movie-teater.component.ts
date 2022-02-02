import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { movieTeatersCreationDTO } from '../movie-teaters.model';
import { MovieTeatersService } from '../movie-teaters.service';

@Component({
  selector: 'app-create-movie-teater',
  templateUrl: './create-movie-teater.component.html',
  styleUrls: ['./create-movie-teater.component.css']
})
export class CreateMovieTeaterComponent implements OnInit {

  constructor(private movieTheaterService: MovieTeatersService, private router: Router) { }

  ngOnInit(): void {
  }

  saveChanges(movieTheater: movieTeatersCreationDTO){
    console.log(movieTheater);
    this.movieTheaterService.create(movieTheater).subscribe(()=>{
      this.router.navigate(['/movieteaters']);
    })
  }
}
