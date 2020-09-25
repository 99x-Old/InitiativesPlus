import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { throwError } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ChangeRole } from "../_models/ChangeRole";

@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl;
  token: string = localStorage.getItem('token');
constructor(private http: HttpClient, private router: Router) { }
  gerListOfRoles(){
    const headers = new HttpHeaders(
      {
        'Content-type': 'application/json',
        'Authorization': 'Bearer '+this.token
      }
    );
    const options = { headers };
    return this.http.get<any>(this.baseUrl + 'user/list-roles', options)
    .pipe(
      catchError(this.handleError)
    );
  }

  changeUserRole(model: ChangeRole){
    console.log("model", model);
    const headers = new HttpHeaders(
      {
        'Content-type': 'application/json',
        'Authorization': 'Bearer '+this.token
      }
    );
    const options = { headers };
    return this.http.put<any>(this.baseUrl + 'user/assign-role', model, options)
    .pipe(map(response => {
    }))
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
