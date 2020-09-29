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

  getListOfInitiatives() {
    const headers = new HttpHeaders(
      {
        'Content-type': 'application/json',
        'Authorization': 'Bearer '+this.token
      }
    );
    const options = { headers };
    return this.http.get<InitiativesForList[]>(this.baseUrl + 'Initiative/GetInitiatives', options)
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
    return this.http.get<InitiativesForList>(this.baseUrl + 'Initiative/GetInitiatives/' + id, options)
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
