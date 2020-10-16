import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { InitiativesForList } from "../_models/InitiativesForList";
import { InitiativeForCreate } from "../_models/InitiativeForCreate";
import { InitiativeActionForUpdate } from "../_models/InitiativeActionForCreate";
import { Router } from '@angular/router';
import { InitiativeAction } from '../_models/InitiativeAction';

@Injectable({
  providedIn: 'root'
})
export class InitiativesService {
  baseUrl = environment.apiUrl;
  // token: string = localStorage.getItem('token');
  constructor(private http: HttpClient, private router: Router) { }

  getListOfInitiatives(filter?: string) {
    const headers = new HttpHeaders(
      {
        'Content-type': 'application/json',
        'Authorization': 'Bearer '+localStorage.getItem('token')
      }
    );
    const options = { headers };
    return this.http.get<InitiativesForList[]>(this.baseUrl + 'Initiative/GetInitiatives?filter=' + filter, options)
    .pipe(
      catchError(this.handleError)
    );
  }

  getInitiative(id: number) {
    const headers = new HttpHeaders(
      {
        'Content-type': 'application/json',
        'Authorization': 'Bearer '+localStorage.getItem('token')
      }
    );
    const options = { headers };
    return this.http.get<InitiativesForList>(this.baseUrl + 'Initiative/GetInitiative/' + id, options)
    .pipe(
      catchError(this.handleError)
    );
  }

  removeUser(id: number, userId: number) {
    const headers = new HttpHeaders(
      {
        'Content-type': 'application/json',
        'Authorization': 'Bearer '+localStorage.getItem('token')
      }
    );
    const options = { headers };
    return this.http.delete(this.baseUrl + 'initiative/remove-user/' + id + '/' + userId, options)
    .pipe(
      catchError(this.handleError)
    );
  }

  removeCurruntUser(id: number) {
    const headers = new HttpHeaders(
      {
        'Content-type': 'application/json',
        'Authorization': 'Bearer '+localStorage.getItem('token')
      }
    );
    const options = { headers };
    return this.http.delete(this.baseUrl + 'initiative/remove-me/' + id, options)
    .pipe(
      catchError(this.handleError)
    );
  }

  joinInitiative(id: number){
    const headers = new HttpHeaders(
      {
        'Content-type': 'application/json',
        'Authorization': 'Bearer '+localStorage.getItem('token')
      }
    );
    const options = { headers };
    return this.http.get<any>(this.baseUrl + 'Initiative/Join/' + id, options)
    .pipe(
      catchError(this.handleError)
    );
  }

  createInitiative(model: InitiativeForCreate){
    const headers = new HttpHeaders(
      {
        'Content-type': 'application/json',
        'Authorization': 'Bearer '+localStorage.getItem('token')
      }
    );
    const options = { headers };
    return this.http.post<any>(this.baseUrl + 'Initiative/create/' , model, options)
    .pipe(
      catchError(this.handleError)
    );
  }

  deleteInitiative(id: number) {
    const headers = new HttpHeaders(
      {
        'Content-type': 'application/json',
        'Authorization': 'Bearer '+localStorage.getItem('token')
      }
    );
    const options = { headers };
    return this.http.delete(this.baseUrl + 'initiative/remove/' + id, options)
    .pipe(
      catchError(this.handleError)
    );
  }

  getUsers(id: number) {
    const headers = new HttpHeaders(
      {
        'Content-type': 'application/json',
        'Authorization': 'Bearer '+localStorage.getItem('token')
      }
    );
    const options = { headers };
    return this.http.get(this.baseUrl + 'initiative/users/' + id, options)
    .pipe(
      catchError(this.handleError)
    );
  }

  createAction(model: InitiativeAction){
    const headers = new HttpHeaders(
      {
        'Content-type': 'application/json',
        'Authorization': 'Bearer '+localStorage.getItem('token')
      }
    );
    const options = { headers };
    return this.http.post<any>(this.baseUrl + 'Actions/create/' , model, options)
    .pipe(
      catchError(this.handleError)
    );
  }

  getActions(id: number){
    const headers = new HttpHeaders(
      {
        'Content-type': 'application/json',
        'Authorization': 'Bearer '+localStorage.getItem('token')
      }
    );
    const options = { headers };
    return this.http.get<any>(this.baseUrl + 'Actions/' + id, options)
    .pipe(
      catchError(this.handleError)
    );
  }

  updateAction(model: InitiativeActionForUpdate){
    const headers = new HttpHeaders(
      {
        'Content-type': 'application/json',
        'Authorization': 'Bearer '+localStorage.getItem('token')
      }
    );
    const options = { headers };
    return this.http.put<any>(this.baseUrl + 'Actions' ,model, options)
    .pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: any) {
    if(error.status === 403){
      return throwError("You're not authorized to view this!");
    }
    const applicationError = error.error.error_description;
    if (applicationError) {
      return throwError(applicationError);
    }
    const serverError = error.error.modelState;
    let modelStateErrors = '';
    if (serverError) {
      for (const key in serverError) {
        if (serverError[key]) {
          modelStateErrors += serverError[key] + '\n';
        }
      }
    }
    return throwError(modelStateErrors || error.error );
  }
}
