import { Component, OnInit } from '@angular/core';
import { movieTeatersDTO } from '../movie-teaters.model';
import { MovieTeatersService } from '../movie-teaters.service';

@Component({
  selector: 'app-index-movie-teater',
  templateUrl: './index-movie-teater.component.html',
  styleUrls: ['./index-movie-teater.component.css']
})
export class IndexMovieTeaterComponent implements OnInit {

  constructor(private movieTheatersService: MovieTeatersService) { }

  movieTheaters!:movieTeatersDTO[];

  columnsToDisplay = ['name','actions']

  ngOnInit(): void {
    this.loadData()
  }

  loadData(){
    this.movieTheatersService.get().subscribe(movieTheaters => {
      this.movieTheaters = movieTheaters;
    })
  }

  delete(id:number){
    this.movieTheatersService.delete(id).subscribe(()=> this.loadData())
  }
}
