import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DefaultComponent } from './layouts/default/default.component';
import { AdministrationComponent } from './modules/administration/administration.component';
import { DashboardComponent } from './modules/dashboard/dashboard.component';
import { HomeComponent } from './modules/home/home.component';
import { InitiativeComponent } from './modules/initiative/initiative.component';
import { InitiativesComponent } from './modules/initiatives/initiatives.component';
import { MentorsComponent } from './modules/tools/mentors/mentors.component';
import { StarterkitComponent } from './modules/tools/starterkit/starterkit.component';
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
        data: {
          roles: [
            'User', 'Super Admin', 'Initiative Lead', 'Initiative Evaluator'
          ]
        }
      },
      {
        path: 'initiatives',
        component: InitiativesComponent,
        canActivate: [AuthGuard],
        data: {
          roles: [
            'User', 'Super Admin', 'Initiative Lead', 'Initiative Evaluator'
          ]
        }
      },
      {
        path: 'initiative/:id',
        component: InitiativeComponent,
        canActivate: [AuthGuard],
        data: {
          roles: [
            'User', 'Super Admin', 'Initiative Lead', 'Initiative Evaluator'
          ]
        }
      },
      {
        path: 'administration',
        component: AdministrationComponent,
        canActivate: [AuthGuard],
        data: {roles: ['Super Admin']}
      },
      {
        path: 'starterkit',
        component: StarterkitComponent,
        canActivate: [AuthGuard],
        data: {
          roles: [
          'User', 'Super Admin', 'Initiative Lead', 'Initiative Evaluator'
          ]
        }
      },
      {
        path: 'mentors',
        component: MentorsComponent,
        canActivate: [AuthGuard],
        data: {
          roles: [
          'User', 'Super Admin', 'Initiative Lead', 'Initiative Evaluator'
          ]
        }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
