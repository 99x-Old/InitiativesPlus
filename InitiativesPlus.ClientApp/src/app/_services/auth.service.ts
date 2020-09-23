import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { UserForLogin } from '../_models/UserForLogin';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'https://localhost:44380/api/user/';

  constructor(private http: HttpClient) { }

  login(model: UserForLogin) {
    console.log("model", model);
    const headers = new HttpHeaders({'Content-type': 'application/json'});
    const options = { headers };
    return this.http.post<UserForLogin>(this.baseUrl + 'login', model, options)
    .pipe(map(response => {
      const user = response;
      // if (user) {
      //   localStorage.setItem('token', user.access_token);
      //   localStorage.setItem('user', user.userName);
      //   localStorage.setItem('isAdmin', user.roles.includes(this.adminString));
      //   this.userToken = user.access_token;
      //   this.isAdmin = user.roles.includes(this.adminString);
      //   this.userName = user.userName;
      //}
      console.log(user);
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
    return throwError(modelStateErrors || 'Server error');
  }
}
