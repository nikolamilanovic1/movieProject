import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { actorCreationDTO } from '../actor.model';

@Component({
  selector: 'app-form-actor',
  templateUrl: './form-actor.component.html',
  styleUrls: ['./form-actor.component.css']
})
export class FormActorComponent implements OnInit {

  constructor(private formBuilder: FormBuilder) { }

  form!: FormGroup;

   @Input()
   model!: actorCreationDTO;

  @Output()
  onSaveChanges = new EventEmitter<actorCreationDTO>();

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      name: ['',{validators:[Validators.required]}],
      dateOfBirth: '',
      picture : '',
      biography : ''
    });

    if(this.model !== undefined){
      this.form.patchValue(this.model);
    }
  }

  onImageSelected(image:any){
    this.form.get('picture')?.setValue(image);
  }

  changeMarkdown(content:any){
    this.form.get('biography')?.setValue(content);
  }

  saveChanges(){
    this.onSaveChanges.emit(this.form.value);
  }
}
