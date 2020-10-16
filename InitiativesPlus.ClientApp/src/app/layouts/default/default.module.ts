import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DefaultComponent } from './default.component';
import { DashboardComponent } from '../../modules/dashboard/dashboard.component';
import { HomeComponent } from '../../modules/home/home.component';
import { InitiativesComponent } from '../../modules/initiatives/initiatives.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from "../../shared/shared.module";
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatDividerModule } from '@angular/material/divider';
import { FlexLayoutModule } from '@angular/flex-layout';
import { MatCardModule } from '@angular/material/card';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { DashboardService } from '../../_services/dashboard.service';
import { InitiativesService } from '../../_services/initiatives.service';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../_services/auth.service';
import { AdministrationComponent } from '../../modules/administration/administration.component';
import {MatGridListModule} from '@angular/material/grid-list'
import { MatFormField, MatFormFieldModule, MatHint, MatLabel } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from "@angular/material/input";
import { UserService } from 'src/app/_services/user.service';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { DataTablesModule } from 'angular-datatables';
import { ActionsDialog, InitiativeComponent } from '../../modules/initiative/initiative.component';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MentorsComponent } from 'src/app/modules/tools/mentors/mentors.component';
import { StarterkitComponent } from 'src/app/modules/tools/starterkit/starterkit.component';
import { EvaluationComponent } from "../../modules/evaluation/evaluation.component";
import { MatDialogModule } from '@angular/material/dialog';

@NgModule({
  declarations: [
    DefaultComponent,
    DashboardComponent,
    HomeComponent,
    InitiativesComponent,
    AdministrationComponent,
    InitiativeComponent,
    StarterkitComponent,
    MentorsComponent,
    EvaluationComponent,
    ActionsDialog
  ],
  imports: [
    CommonModule,
    RouterModule,
    SharedModule,
    MatSidenavModule,
    MatDividerModule,
    FlexLayoutModule,
    MatCardModule,
    MatPaginatorModule,
    MatTableModule,
    FormsModule,
    ReactiveFormsModule,
    MatGridListModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatIconModule,
    MatButtonModule,
    DataTablesModule,
    MatTooltipModule,
    MatDialogModule
  ],
  entryComponents: [
    ActionsDialog
  ],
  providers: [
    DashboardService,
    AuthService,
    UserService,
    InitiativesService
  ]
})
export class DefaultModule { }
