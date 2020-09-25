import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DefaultComponent } from './layouts/default/default.component';
import { AdministrationComponent } from './modules/administration/administration.component';
import { DashboardComponent } from './modules/dashboard/dashboard.component';
import { HomeComponent } from './modules/home/home.component';
import { InitiativesComponent } from './modules/initiatives/initiatives.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'dashboard',
    component: DefaultComponent,
    children: [
      {
        path: '',
        component: DashboardComponent,
        canActivate: [AuthGuard],
        data: {roles: ['User', 'Super Admin']}
      },
      {
        path: 'initiatives',
        component: InitiativesComponent
      },
      {
        path: 'administration',
        component: AdministrationComponent,
        canActivate: [AuthGuard],
        data: {roles: ['Super Admin']}
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
