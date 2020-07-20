import { Component, OnInit } from '@angular/core';
import {SignService} from './sign.service';
import {FormBuilder, NgForm} from '@angular/forms';
import {User} from './user';

@Component({
  selector: 'app-sign',
  templateUrl: './sign.component.html',
  styleUrls: ['./sign.component.css']
})
export class SignComponent implements OnInit {

  public UserModel = new User;

  constructor(private signService :SignService )
  {

  } 
  
  ngOnInit(): void {
  
  }
  onSubmit(){
    this.signService.enroll(this.UserModel).subscribe(loginDetails => console.log('Success', loginDetails))
  }
}
