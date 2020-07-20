import { Injectable } from '@angular/core';
import {User} from '../sign/user';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  private url : string = 'https://localhost:44302/api/login/userregister';

  constructor(private _http : HttpClient) {
   }

 UserRegister(user : User) : Observable<User>{
  const headers = new HttpHeaders({'Content-Type':'application/json'});
  const body = {
      username : user.username,
      email : user.email,
      phonenumber : user.phonenumber,
      streetnumberandname : user.streetnumberandname,
      nameoftown : user.nameoftown,
      postcode : user.postcode,
      password : user.password
    }
    return this._http.post<User>(this.url,body,{headers})
  }
}
