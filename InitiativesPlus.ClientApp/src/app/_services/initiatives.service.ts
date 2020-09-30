import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { InitiativesForList } from "../_models/InitiativesForList";

@Injectable({
  providedIn: 'root'
})
export class InitiativesService {
  baseUrl = environment.apiUrl;
  token: string = localStorage.getItem('token');
  constructor(private http: HttpClient) { }

  getListOfInitiatives(filter?: string) {
    const headers = new HttpHeaders(
      {
        'Content-type': 'application/json',
        'Authorization': 'Bearer '+this.token
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
        'Authorization': 'Bearer '+this.token
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
        'Authorization': 'Bearer '+this.token
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
        'Authorization': 'Bearer '+this.token
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
        'Authorization': 'Bearer '+this.token
      }
    );
    const options = { headers };
    return this.http.get<any>(this.baseUrl + 'Initiative/Join/' + id, options)
    .pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: any) {
    console.log(error)
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
