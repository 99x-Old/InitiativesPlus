import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DefaultComponent } from './default.component';
import { DashboardComponent } from '../../modules/dashboard/dashboard.component';
import { HomeComponent } from '../../modules/home/home.component';
import { InitiativesComponent } from '../../modules/initiatives/initiatives.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from "../../shared/shared.module";
import { MatSidenavModule } from '@angular/material/sidenav';


@NgModule({
  declarations: [
    DefaultComponent,
    DashboardComponent,
    HomeComponent,
    InitiativesComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    MatSidenavModule
  ]
})
export class DefaultModule { }
