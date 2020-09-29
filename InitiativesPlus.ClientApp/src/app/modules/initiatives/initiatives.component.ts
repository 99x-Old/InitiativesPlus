import { AfterViewInit, Component, OnInit, Renderer2  } from '@angular/core';
import { Router } from '@angular/router';
import { DataTableDirective } from 'angular-datatables';
import { ToastrService } from 'ngx-toastr';
import { Subject } from 'rxjs';
import { subscribeOn } from 'rxjs/operators';
import { InitiativesForList } from 'src/app/_models/InitiativesForList';
import { InitiativesService } from 'src/app/_services/initiatives.service';

@Component({
  selector: 'app-initiatives',
  templateUrl: './initiatives.component.html',
  styleUrls: ['./initiatives.component.scss']
})
export class InitiativesComponent implements OnInit {
  listOfInitiatives: InitiativesForList[] = [];
  dtElement: DataTableDirective;
  dtOptions: DataTables.Settings = {};
  dtTrigger: Subject<any> = new Subject();
  constructor(private initiativeService: InitiativesService, private toastr: ToastrService, private renderer: Renderer2, private router: Router) { }

  ngOnInit(): void {
    this.listOfInitiatives = [];
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      processing: true
    };
    this.getListOfInitiatives();
  }

  getListOfInitiatives() {
    this.initiativeService.getListOfInitiatives()
    .subscribe(data => {
      this.listOfInitiatives = data;
      this.dtTrigger.next();
      console.log(this.listOfInitiatives)
      //this.toastr.success("User role changed successfully.")
    }, error => {
      this.toastr.error(error)
    });
  }

  navigate(id: number){
    console.log(id)
    this.router.navigate(['dashboard/initiative',id]);

  }
}
