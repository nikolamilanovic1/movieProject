import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { movieCreationDTO } from '../movies/movies.model';
import { movieTeatersCreationDTO, movieTeatersDTO } from './movie-teaters.model';

@Injectable({
  providedIn: 'root'
})
export class MovieTeatersService {

  constructor(private http: HttpClient) { }

  private apiURL = environment.apiURL + '/movieTheaters';

  public get(): Observable<movieTeatersDTO[]>{
    return this.http.get<movieTeatersDTO[]>(this.apiURL);
  }

  public create(movieTeatersCreationDTO : movieTeatersCreationDTO){
    return this.http.post(this.apiURL, movieTeatersCreationDTO)
  }

  public getById(id: number): Observable<movieTeatersDTO>{
    return this.http.get<movieTeatersDTO>(`${this.apiURL}/${id}`);
  }

  public edit(id: number, movieTheaterDTO: movieTeatersCreationDTO){
    return this.http.put(`${this.apiURL}/${id}`,movieTheaterDTO);
  }

  public delete(id:number){
    return this.http.delete(`${this.apiURL}/${id}`)
  }
}
