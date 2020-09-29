import { AfterViewInit, Component, OnInit, Renderer2  } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { subscribeOn } from 'rxjs/operators';
import { InitiativesForList } from 'src/app/_models/InitiativesForList';
import { InitiativesService } from 'src/app/_services/initiatives.service';

@Component({
  selector: 'app-initiatives',
  templateUrl: './initiatives.component.html',
  styleUrls: ['./initiatives.component.scss']
})
export class InitiativesComponent implements OnInit {
  listOfInitiatives: InitiativesForList[];
  dtOptions: DataTables.Settings = {};
  constructor(private initiativeService: InitiativesService, private toastr: ToastrService, private renderer: Renderer2, private router: Router) { }

  ngOnInit(): void {
    this.dtOptions = {
      pagingType: 'full_numbers',
      pageLength: 5,
      processing: true
    };
    this.listOfInitiatives = [];
    this.getListOfInitiatives();
  }

  getListOfInitiatives() {
    this.initiativeService.getListOfInitiatives()
    .subscribe(data => {
      this.listOfInitiatives = data;
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
