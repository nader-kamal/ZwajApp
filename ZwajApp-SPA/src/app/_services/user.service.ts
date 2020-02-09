import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/user';



@Injectable({
  providedIn: 'root'
})
export class UserService {
  baseUrl = environment.apiUrl + 'users/'
  constructor(private http: HttpClient) { }

  GetUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl)
  }
  GetUser(id: number): Observable<User> {
    return this.http.get<User>(`${this.baseUrl}${id}`);

  }
  updateUser(id: number, user: User) {
    return this.http.put(`${this.baseUrl}${id}`, user);
  }
}
