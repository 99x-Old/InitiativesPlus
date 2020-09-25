import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../_services/auth.service';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss']
})
export class SidebarComponent implements OnInit {
  userName: string;
  curruntRole: string
  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.userName = this.authService.getUserName();
    this.curruntRole = this.authService.getUserRole();
  }

  canNavigate(roles: string []): boolean{
    if(roles.includes(this.curruntRole)){
      return true;
    }
  }
}
