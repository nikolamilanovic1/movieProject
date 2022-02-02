import { animate } from '@angular/animations';
import { NullVisitor } from '@angular/compiler/src/render3/r3_ast';
import { Component, Input, OnInit, Output } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { SecurityService } from 'src/app/security/security.service';


@Component({
  selector: 'app-rating',
  templateUrl: './rating.component.html',
  styleUrls: ['./rating.component.css']
})
export class RatingComponent implements OnInit {

  constructor(private securityService: SecurityService) { }


  @Input()
  maxRating = 5;
  @Input()
  selectedRate = 0;
  @Output()
  onRating: EventEmitter<number> = new EventEmitter<number>();
  previousRate = 0;
  maxRatingArr : any;

  ngOnInit(): void {
    this.maxRatingArr = Array(this.maxRating).fill(0);
  }

  handleMouseEnter(index:number){
    this.selectedRate = index + 1;
  }

  handleMouseLeave(){
    if (this.previousRate !== 0){
      this.selectedRate = this.previousRate;
    } else{
      this.selectedRate = 0;
    }
    console.log(this.previousRate)
  }

  rate(index:number){
    if (this.securityService.isAuthenticated()){
      this.selectedRate = index + 1;
      this.previousRate = this.selectedRate;
      console.log(this.previousRate)
      this.onRating.emit(this.selectedRate)
    }else{
      alert("Error you need to log in before voting")
    }

  }



}
