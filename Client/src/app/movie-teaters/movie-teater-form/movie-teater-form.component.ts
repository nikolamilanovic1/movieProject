import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { movieTeatersCreationDTO } from '../movie-teaters.model';

@Component({
  selector: 'app-movie-teater-form',
  templateUrl: './movie-teater-form.component.html',
  styleUrls: ['./movie-teater-form.component.css']
})
export class MovieTeaterFormComponent implements OnInit {

  constructor(private formBuilder:FormBuilder) { }

  form!: FormGroup

  @Output()
  onSaveChanges = new EventEmitter<movieTeatersCreationDTO>();

  @Input()
  model!: movieTeatersCreationDTO

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      name: ['',{validators:[Validators.required]}]
    })
    if(this.model !== undefined){
      this.form.patchValue(this.model) //ovo ovde ti govori da je ovo edit
    }
  }


  saveChanges(){
    this.onSaveChanges.emit(this.form.value)
  }
}
