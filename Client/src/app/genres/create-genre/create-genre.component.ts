import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { parseWebAPIErrors } from 'src/app/utilities/utils';
//import { error } from 'console';
import { genreCreationDTO } from '../genre.model';
import { GenresService } from '../genres.service';

@Component({
  selector: 'app-create-genre',
  templateUrl: './create-genre.component.html',
  styleUrls: ['./create-genre.component.css']
})
export class CreateGenreComponent implements OnInit {

  errors: string[] = [];

  constructor(private router: Router, private genresService: GenresService) { }

  form!: FormGroup;

  ngOnInit(): void {
  }

  saveChanges(genreCrationDTO : genreCreationDTO){
    this.genresService.create(genreCrationDTO).subscribe(()=>{
      this.router.navigate(['/genres']);
    },error => this.errors = parseWebAPIErrors(error));
  }
}
