import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from "@auth0/angular-jwt";
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  jwtHelper = new JwtHelperService();
  decodedToken:any;
  baseUrl = environment.apiUrl+'auth/'
  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http.post(`${this.baseUrl}login`, model).pipe(
      //return this.http.post(this.baseUrl + 'login', model).pipe(
      map((response: any) => {
        const user = response;
        if (user) { 
          localStorage.setItem('token', user.token) ;
          this.decodedToken=this.jwtHelper.decodeToken(user.token);
          console.log(this.decodedToken);
        }
      })
    )

  }

  register(model: any) {
    return this.http.post(`${this.baseUrl}Register`, model);
  }

  loggedIn() {
    try {
      const token = localStorage.getItem('token');
      return !this.jwtHelper.isTokenExpired(token);
    } catch (error) {
      return false;
    }

  }
}
