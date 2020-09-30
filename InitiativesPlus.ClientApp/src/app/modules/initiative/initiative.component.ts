import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { InitiativesService } from 'src/app/_services/initiatives.service';
import { InitiativeForDisplay } from "../../_models/InitiativeForDisplay";
@Component({
  selector: 'app-initiative',
  templateUrl: './initiative.component.html',
  styleUrls: ['./initiative.component.scss']
})
export class InitiativeComponent implements OnInit {
  id: number;
  private sub: any;
  public initiative: InitiativeForDisplay
  constructor(private route: ActivatedRoute, private initiativesService: InitiativesService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.sub = this.route.params.subscribe(params => {
      this.id = +params['id'];
      //console.log(this.id)
      this.initiativesService.getInitiative(this.id)
      .subscribe(data => {
        this.initiative = data;
      }, error => {
        this.toastr.error(error)
      });
   });
  }
  ngOnDestroy() {
    this.sub.unsubscribe();
  }

  joinInitiative(){
    this.initiativesService.joinInitiative(this.id)
    .subscribe(data => {
      this.toastr.success("You're now a member of "+this.initiative.name)
    }, error => {
      this.toastr.error(error)
    });
  }

  leaveInitiative(){
    this.initiativesService.removeCurruntUser(this.id)
    .subscribe(data => {
      this.toastr.success("You have left "+this.initiative.name)
    }, error => {
      this.toastr.error(error)
    });
  }
}
