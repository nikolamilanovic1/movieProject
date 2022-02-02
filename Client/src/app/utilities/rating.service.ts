import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RatingService {

  constructor(private http: HttpClient) { }

  private apiURL = environment.apiURL + "/rating";

  public rate(movieId: number, rate: number){
    console.log({movieId, rate})
    return this.http.post(this.apiURL,{rate, movieId})
  }
}
