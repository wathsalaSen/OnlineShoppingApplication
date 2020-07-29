import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { User } from './user';
import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { AuthRequest } from '../AuthRequest/auth-request';


@Injectable({
  providedIn: 'root'
})
export class SignService {
  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;

  //private baseUrl = 'https://localhost:44313/';
  private readonly apiPath: string = 'api/Login/authenticate';

  constructor(private _http: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser')));
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  // autenticate(username: string, password: string): Observable<User> {
  //   return this._http.post<User>('https://localhost:5001/login/authenticate', { username, password }, {
  //     //return this._http.post<User>(`${environment.apiUrl}${this.apiPath}`, { username, password },{
  //     headers: new HttpHeaders({
  //       'Content-Type': 'application/json'
  //     })
  //   }).pipe(map(user => {
  //     // store user details and jwt token in local storage to keep user logged in between page refreshes
  //     localStorage.setItem('currentUser', JSON.stringify(user));
  //     this.currentUserSubject.next(user);
  //     return user;
  //   }));
  //   console.log(environment.apiUrl);
  // }

  autenticate(username: string, password: string): Observable<User> {
    let authRequest: AuthRequest = new AuthRequest(username, password);
    return this._http.post<User>(`${environment.apiUrl}${this.apiPath}`, authRequest)
      .pipe(map(user => {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        return user;
      }));
  }

  logout() {
    // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }
}
