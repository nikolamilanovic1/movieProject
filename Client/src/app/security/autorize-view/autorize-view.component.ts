import { Component, Input, OnInit } from '@angular/core';
import { SecurityService } from '../security.service';

@Component({
  selector: 'app-autorize-view',
  templateUrl: './autorize-view.component.html',
  styleUrls: ['./autorize-view.component.css']
})
export class AutorizeViewComponent implements OnInit {

  constructor(private securityService:SecurityService) { }

  ngOnInit(): void {
  }

  @Input()
  role!: string;

  public isAuthorized(){
    if(this.role){
      return this.securityService.getRole() === this.role;
    } else{

    }
    return this.securityService.isAuthenticated();
  }

}
