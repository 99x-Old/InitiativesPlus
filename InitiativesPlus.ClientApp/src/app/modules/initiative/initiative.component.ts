import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/_services/auth.service';
import { InitiativesService } from 'src/app/_services/initiatives.service';
import { InitiativeForDisplay } from "../../_models/InitiativeForDisplay";
import { UserForDisplay } from "../../_models/UserForDisplay";
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { UserService } from 'src/app/_services/user.service';
import { ActionDialogData } from "../../_models/ActionDialogData";
import { InitiativeChangeLead } from 'src/app/_models/InitiativeChangeLead';

@Component({
  selector: 'app-initiative',
  templateUrl: './initiative.component.html',
  styleUrls: ['./initiative.component.scss']
})
export class InitiativeComponent implements OnInit {
  id: number;
  private sub: any;
  public initiative: InitiativeForDisplay;
  evaluator: string = "Initiative Evaluator";
  users: UserForDisplay[] = [];
  dialogData: ActionDialogData;

  constructor(
    private route: ActivatedRoute, 
    private initiativesService: InitiativesService, 
    private toastr: ToastrService,
    private authService: AuthService,
    private router: Router,
    public dialog: MatDialog,
    ) { }

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

   this.getUsers();
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

  deleteInitiative(){
    this.initiativesService.deleteInitiative(this.id)
    .subscribe(data => {
      this.toastr.success("You have deleted "+this.initiative.name)
      this.router.navigate(['/dashboard/initiatives'])
    }, error => {
      this.toastr.error(error)
    });
  }

  initiativeActions(userFromPage: UserForDisplay){
    const dialogData : ActionDialogData = {
      initiativeId : this.id,
      user : userFromPage
    }
    this.openDialog(dialogData);
  }
  getUsers(){
    this.initiativesService.getUsers(this.id)
    .subscribe(data => {
      this.users = data as UserForDisplay[];
      console.log(this.users)
    }, error => {
      this.toastr.error(error)
    });
  }

  isAuthorized(role: string){
    let user = this.authService.getUserRole();
    return (role === user);
  }

  openDialog(dataFromPage: ActionDialogData): void {
    const dialogRef = this.dialog.open(ActionsDialog, {
      data: dataFromPage
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
      // this.animal = result;
    });
  }
}

@Component({
  selector: 'actions-dialog',
  templateUrl: 'actions-dialog.html',
})
export class ActionsDialog {

  constructor(
    public dialogRef: MatDialogRef<ActionsDialog>,
    @Inject(MAT_DIALOG_DATA) public data: ActionDialogData,
    private toastr: ToastrService,
    private userService: UserService
    ) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

  changeLead(){
    console.log(this.data.user.id)
    const model: InitiativeChangeLead ={
      initiativeId : this.data.initiativeId,
      userId: this.data.user.id
    }
    this.userService.changeInitiativeLead(model)
    .subscribe(data => {
      this.toastr.success("Initiative lead has been changed.")
    }, error => {
      this.toastr.error(error)
    });
  }

}