import { Component, OnInit } from '@angular/core';
import { SignInService } from '../signIn/signIn.service';

@Component({
  selector: 'app-top-navigation',
  templateUrl: './top-navigation.component.html',
  styleUrls: ['./top-navigation.component.css']
})
export class TopNavigationComponent implements OnInit {
  public hasSignedIn: boolean = false;

  constructor(private signService: SignInService) { }

  ngOnInit(): void {
    if (this.signService.currentUserValue) {
      this.hasSignedIn = true;
    }
  }

  signOut() {
    this.hasSignedIn = false;
    this.signService.logout();
  }

}
