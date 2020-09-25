import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ChangeRole } from 'src/app/_models/ChangeRole';
import { UserService } from "../../_services/user.service";
declare let alertify: any;
@Component({
  selector: 'app-administration',
  templateUrl: './administration.component.html',
  styleUrls: ['./administration.component.scss']
})
export class AdministrationComponent implements OnInit {
  userRoles: string [];
  selectedRole: string;
  selectedUser: string;
  changedRole: ChangeRole = {
    username: '',
    newRole: 0
  };
  constructor(private userService: UserService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.userService.gerListOfRoles().subscribe(data => {
      this.userRoles = data;
    }, error => {
      this.toastr.error(error)
    });
  }

  changeRole(){
    this.changedRole.username = this.selectedUser;
    this.changedRole.newRole = this.userRoles.indexOf(this.selectedRole) + 1;
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
}
