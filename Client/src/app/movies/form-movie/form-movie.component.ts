import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { actorsMovieDTO } from 'src/app/actors/actor.model';
import { multipleSelectorModel } from 'src/app/utilities/multiple-selector/multiple-selector.model';
import { movieCreationDTO, movieDTO } from '../movies.model';

@Component({
  selector: 'app-form-movie',
  templateUrl: './form-movie.component.html',
  styleUrls: ['./form-movie.component.css']
})
export class FormMovieComponent implements OnInit {

  constructor(private formBuilder: FormBuilder) { }

  form!: FormGroup

  @Input()
  model!: movieDTO

  @Output()
  onSaveChanges = new EventEmitter<movieCreationDTO>();

  @Input()
  nonSelectedGenres: multipleSelectorModel[] = [];
  @Input()
  slectedGenres: multipleSelectorModel[] = [];

  @Input()
  nonSelectedMovieTheaters: multipleSelectorModel[] = [];
  @Input()
  slectedMovieTheaters: multipleSelectorModel[] = []

  @Input()
  selectedActors:actorsMovieDTO[] = []

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      title: ['',{validators:[Validators.required]}],
      summary: '',
      inTheaters: false,
      trailer: '',
      releaseDate:'',
      poster: '',
      genresIds:'',
      movieTheatersIds:'',
      actors: ''
    });

    if(this.model !== undefined){
      this.form.patchValue(this.model)
    }

  }
 onImageSelected(file: File){
    this.form.get('poster')?.setValue(file);
  }

  changeMarkdown(content: string){
    this.form.get('summary')?.setValue(content);
  }

  saveChanges(){
    const genresIds = this.slectedGenres.map(value => value.key);
    this.form.get('genresIds')?.setValue(genresIds);

    const movieTheatersIds = this.slectedMovieTheaters.map(value => value.key);
    this.form.get('movieTheatersIds')?.setValue(movieTheatersIds);

    const actors = this.selectedActors.map(value=>{
      return {id: value.id, character: value.character};
    });
    this.form.get('actors')?.setValue(actors);

    this.onSaveChanges.emit(this.form.value);
  }


}
