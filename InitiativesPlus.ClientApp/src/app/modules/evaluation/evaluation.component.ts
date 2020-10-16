import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { InitiativeForCreate } from 'src/app/_models/InitiativeForCreate';
import { InitiativesService } from 'src/app/_services/initiatives.service';

@Component({
  selector: 'app-evaluation',
  templateUrl: './evaluation.component.html',
  styleUrls: ['./evaluation.component.scss']
})
export class EvaluationComponent implements OnInit {
  initiativeName: string;
  year: string;
  years: string[] = [];
  constructor(private initiativeService: InitiativesService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.years.push(new Date().getFullYear().toString());
    this.years.push(new Date(new Date().setFullYear(new Date().getFullYear() + 1)).getFullYear().toString());
  }

  createInitiative(){
    let initiative: InitiativeForCreate = {
      name : this.initiativeName,
      year : this.year
    };
    console.log(initiative)
    this.initiativeService.createInitiative(initiative)
    .subscribe(data => {
      this.toastr.success("Initiative added successfully");
    }, error => {
      this.toastr.error(error);
    });
  }

}
