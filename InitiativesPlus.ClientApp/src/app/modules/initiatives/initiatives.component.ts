import { Component, OnInit  } from '@angular/core';
import { Router } from '@angular/router';
import { DataTableDirective } from 'angular-datatables';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { InitiativesForList } from 'src/app/_models/InitiativesForList';
import { InitiativesService } from 'src/app/_services/initiatives.service';

@Component({
  selector: 'app-initiatives',
  templateUrl: './initiatives.component.html',
  styleUrls: ['./initiatives.component.scss']
})
export class InitiativesComponent implements OnInit {
  listOfInitiatives: InitiativesForList[] = [];
  listOfMyInitiatives: InitiativesForList[] = [];
  dtElement: DataTableDirective;
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject();
  dtTrigger2: Subject<any> = new Subject();
  constructor(private initiativeService: InitiativesService, private toastr: ToastrService, private router: Router) { }

  ngOnInit(): void {
    this.listOfInitiatives = [];
    this.listOfMyInitiatives = [];
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      processing: true
    };
    this.getListOfInitiatives();
    this.getMyListOfInitiatives();
  }

  getListOfInitiatives() {
    this.initiativeService.getListOfInitiatives()
    .subscribe(data => {
      this.listOfInitiatives = data;
      this.dtTrigger.next();
    }, error => {
      this.toastr.error(error)
    });
  }
  getMyListOfInitiatives() {
    this.initiativeService.getListOfInitiatives('my')
    .subscribe(data => {
      this.listOfMyInitiatives = data;
      this.dtTrigger2.next();
    }, error => {
      this.toastr.error(error)
    });
  }

  navigate(id: number){
    //console.log(id)
    this.router.navigate(['dashboard/initiative',id]);
  }
}
