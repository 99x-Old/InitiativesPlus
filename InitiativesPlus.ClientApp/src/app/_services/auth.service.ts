import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, catchError } from 'rxjs/operators';
import { Observable, throwError } from 'rxjs';
import { UserForLogin } from '../_models/UserForLogin';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = environment.apiUrl;
  decodedToken: any;
  helper = new JwtHelperService();
  token: string;
  constructor(private http: HttpClient) { }

  login(model: UserForLogin) {
    const headers = new HttpHeaders({'Content-type': 'application/json'});
    const options = { headers };
    return this.http.post<any>(this.baseUrl + 'user/login', model, options)
    .pipe(map(response => {
      const user = response;

      localStorage.setItem('token', user.tokenString);
      this.decodedToken  = this.helper.decodeToken(user.tokenString);
      localStorage.setItem('role', user.tokenString.role);
    }))
    .pipe(
      catchError(this.handleError)
    );
  }

  loggedIn() {
    return !this.helper.isTokenExpired(this.getToken());
  }

  getToken(){
    return localStorage.getItem('token');
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
