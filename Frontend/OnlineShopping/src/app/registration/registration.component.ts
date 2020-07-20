import { Component, OnInit } from '@angular/core';
import {User} from '../sign/user';
import {RegistrationService} from './registration.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {

  public UserModel = new User;
  constructor(private _registrationService : RegistrationService) { }

  ngOnInit(): void {
  }

  onSubmit(){
    this._registrationService.UserRegister(this.UserModel).subscribe(Reg => console.log('Success',Reg));
  }
}
