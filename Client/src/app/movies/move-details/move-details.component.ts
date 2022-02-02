import { Component, OnInit } from '@angular/core';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { Coords } from 'leaflet';
import { RatingService } from 'src/app/utilities/rating.service';
import { movieDTO } from '../movies.model';
import { MoviesService } from '../movies.service';

@Component({
  selector: 'app-move-details',
  templateUrl: './move-details.component.html',
  styleUrls: ['./move-details.component.css']
})
export class MoveDetailsComponent implements OnInit {

  constructor(
    private moviesService: MoviesService,
    private activatedRoute: ActivatedRoute,
    private sanitizer: DomSanitizer,
    private ratingsService: RatingService
    ) { }

  movie!: movieDTO;
  releaseDate!: Date;
  trailerURL!: SafeResourceUrl;
  //coordinates: movieDTO[] = []

  ngOnInit(): void {
    this.activatedRoute.params.subscribe(params => {
      this.moviesService.getById(params['id']).subscribe(movie =>{
        this.movie = movie;
        this.releaseDate = new Date(movie.releaseDate);
        this.trailerURL = this.generateYoutubeURLForEmbeddedVideo(movie.trailer)
      })
    })
  }

  generateYoutubeURLForEmbeddedVideo(url : any): SafeResourceUrl{
    if (!url){
      return '';
    }
    //https://www.youtube.com/watch?v=9ix7TUGVYIo&t=1s&ab_channel=WarnerBros.Pictures
    let videoId = url.split('v=')[1];
    const ampersandPosition = videoId.indexOf('&');
    if (ampersandPosition !== -1){
      videoId = videoId.substring(0, ampersandPosition);
    }

    return this.sanitizer.bypassSecurityTrustResourceUrl(`https://www.youtube.com/embed/${videoId}`)
  }

  onRating(rate:number){
    this.ratingsService.rate(this.movie.id, rate).subscribe(()=>{
      
    })
  }
}
