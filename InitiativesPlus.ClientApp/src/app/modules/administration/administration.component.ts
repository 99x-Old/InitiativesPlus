import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ChangeRole } from 'src/app/_models/UserChangeRole';
import { UserService } from "../../_services/user.service";
declare let alertify: any;
import { UserChangeStatus } from "../../_models/UserChangeStatus";

@Component({
  selector: 'app-administration',
  templateUrl: './administration.component.html',
  styleUrls: ['./administration.component.scss']
})
export class AdministrationComponent implements OnInit {
  userRoles: string [];
  userStatuses: string [];
  selectedRole: string;
  selectedStatus: string;
  selectedUser: string;
  selectedUserForStatus: string;
  changedRole: ChangeRole = {
    username: '',
    newRole: 0
  };
  changedStatus: UserChangeStatus = {
    username: '',
    newStatus: 0
  };
  constructor(private userService: UserService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.userService.gerListOfRoles().subscribe(data => {
      this.userRoles = data;
    }, error => {
      this.toastr.error(error)
    });

    this.userService.gerListOfStatuses().subscribe(data => {
      this.userStatuses = data;
    }, error => {
      this.toastr.error(error)
    });
  }

  changeRole(){
    this.changedRole.username = this.selectedUser;
    this.changedRole.newRole = this.userRoles.indexOf(this.selectedRole) + 1;
    if(this.changedRole.username === undefined ||this.changedRole.newRole === 0){
      this.toastr.error("Either username or role was not selected.")
      return;
    }
    var _self = this;
    alertify.confirm('Heads Up!', 'Changing the user role will remove all the permissins that this user is having.', 
    function(){ 
      _self.userService.changeUserRole(_self.changedRole).subscribe(data => {
        _self.toastr.success("User role changed successfully.")
      }, error => {
        _self.toastr.error(error)
      });
    }, function(){ 
      _self.toastr.info('You cancelled action.')
    });
  }

  changeStatus(){
    this.changedStatus.username = this.selectedUserForStatus;
    this.changedStatus.newStatus = this.userStatuses.indexOf(this.selectedStatus) + 1;
    if(this.changedStatus.username === undefined ||this.changedStatus.newStatus === 0){
      this.toastr.error("Either username or status was not selected.")
      return;
    }
    var _self = this;
    alertify.confirm('Heads Up!', 'Changing the status of the user status to '+ this.selectedStatus + '!, continue?', 
    function(){ 
      _self.userService.changeUserStatus(_self.changedStatus).subscribe(data => {
        _self.toastr.success("User status changed successfully.");
      }, error => {
        _self.toastr.error(error);
      });
    }, function(){ 
      _self.toastr.info('You cancelled action.')
    });
  }
}
