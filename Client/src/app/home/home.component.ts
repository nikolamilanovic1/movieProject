import { Component, OnInit } from '@angular/core';
import { movieDTO } from '../movies/movies.model';
import { MoviesService } from '../movies/movies.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(private moviesService: MoviesService) { }

  ngOnInit(): void {
    this.loadData();
  }

  moviesInTheaters!:any;
  moviesFutureReleases!:any;

  loadData(){
    this.moviesService.getHomePageMovies().subscribe(homeDTO => {
      this.moviesFutureReleases = homeDTO.upcomingReleases;
      this.moviesInTheaters = homeDTO.inTheatres;
    })
  }

  onDelete(){
    this.loadData();
  }

}
