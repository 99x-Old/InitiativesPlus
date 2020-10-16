import { Component, OnInit, Inject, ViewChild } from '@angular/core';
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
import { InitiativeAction } from "../../_models/InitiativeAction";
import { InitiativeActionToDisplay } from "../../_models/InitiativeActionToDisplay";
import { MatAccordion } from '@angular/material/expansion';
import { InitiativeActionForUpdate } from 'src/app/_models/InitiativeActionForCreate';

@Component({
  selector: 'app-initiative',
  templateUrl: './initiative.component.html',
  styleUrls: ['./initiative.component.scss']
})

export class InitiativeComponent implements OnInit {
  @ViewChild(MatAccordion) accordion: MatAccordion;
  id: number;
  private sub: any;
  initiative: InitiativeForDisplay;
  userList: string [] = ["Initiative Evaluator", "Initiative Lead"];
  users: UserForDisplay[] = [];
  actions: InitiativeActionToDisplay [] = [];
  dialogData: ActionDialogData;
  evaluator: string = "Initiative Evaluator"
  lead: string = "Initiative Lead"

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
   this.loadActions();
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
      // console.log(this.users)
    }, error => {
      this.toastr.error(error)
    });
  }

  isAuthorized(roles: string []){
    let user = this.authService.getUserRole();
    if(roles.includes(user)){
      return true;
    }
    return false;
  }
  
  isAuthorizedUser(role: string){
    let user = this.authService.getUserRole();
    return role === user;
  }

  openDialog(dataFromPage: ActionDialogData): void {
    const dialogRef = this.dialog.open(ActionsDialog, {
      data: dataFromPage
    });

    dialogRef.afterClosed().subscribe(result => {
      this.loadActions();
    });
  }

  loadActions(){
    if(this.isAuthorizedUser(this.lead)){
      this.initiativesService.getActions(this.id)
      .subscribe(data => {
        this.actions = data as InitiativeActionToDisplay[];
        // console.log(this.users)
      }, error => {
        this.toastr.error(error)
      });
    }
  }

  updateAction(action: InitiativeActionForUpdate){
    this.initiativesService.updateAction(action)
      .subscribe(data => {
        this.toastr.success("Action updated")
      }, error => {
        this.toastr.error(error)
      });
  }
}

@Component({
  selector: 'actions-dialog',
  templateUrl: 'actions-dialog.html',
})
export class ActionsDialog {
  action: string;
  evaluator: string = "Initiative Evaluator"
  lead: string = "Initiative Lead"

  constructor(
    public dialogRef: MatDialogRef<ActionsDialog>,
    @Inject(MAT_DIALOG_DATA) public data: ActionDialogData,
    private toastr: ToastrService,
    private userService: UserService,
    private authService: AuthService,
    private initiativesService: InitiativesService
    ) {}

  onNoClick(): void {
    this.dialogRef.close();
  }

  changeLead(){
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

  isAuthorizedUser(role: string){
    let user = this.authService.getUserRole();
    return role === user;
  }

  createAction(){
    const actionToCreate: InitiativeAction = {
      initiativeId: this.data.initiativeId,
      userId: this.data.user.id,
      action: this.action,
      progress: 0
    }

    this.initiativesService.createAction(actionToCreate)
    .subscribe(data => {
      this.toastr.success("Action created and assigned")
    }, error => {
      this.toastr.error(error)
    });
  }
}