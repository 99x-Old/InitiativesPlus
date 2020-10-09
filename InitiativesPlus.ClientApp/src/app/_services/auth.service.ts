import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { UserForLogin } from '../_models/UserForLogin';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';
import { UserForRegister } from '../_models/UserForRegister';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl;
  decodedToken: any;
  helper = new JwtHelperService();
  token: string;
  constructor(private http: HttpClient, private router: Router) { }

  login(model: UserForLogin) {
    const headers = new HttpHeaders({'Content-type': 'application/json'});
    const options = { headers };
    return this.http.post<any>(this.baseUrl + 'user/login', model, options)
    .pipe(map(response => {
      const user = response;

      localStorage.setItem('token', user.tokenString);
      this.decodedToken  = this.helper.decodeToken(user.tokenString);
      localStorage.setItem('role', this.decodedToken.role);
      localStorage.setItem('user', this.decodedToken.unique_name);
    }))
    .pipe(
      catchError(this.handleError)
    );
  }

  register(model: UserForRegister) {
    const headers = new HttpHeaders({'Content-type': 'application/json'});
    const options = { headers };
    return this.http.post<any>(this.baseUrl + 'user/register', model, options)
    .pipe(map(response => {
      this.router.navigate(['/']);
    }))
    .pipe(
      catchError(this.handleError)
    );
  }

  loggedIn() {
    return !this.helper.isTokenExpired(this.getToken());
  }

  logOut(){
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('user');
    this.router.navigate(['/']);
  }

  getUserRole(){
    return localStorage.getItem('role');
  }

  getToken(){
    return localStorage.getItem('token');
  }

  getUserName(){
    return `${localStorage.getItem('user')[0].toUpperCase()}${localStorage.getItem('user').substring(1)}`;
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
