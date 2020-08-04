import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from './user';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { SignInService } from './signIn.service';

@Component({
  selector: 'app-signIn',
  templateUrl: './signIn.component.html',
  styleUrls: ['./signIn.component.css']
})
export class SignInComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  error = '';

  public UserModel = new User;

  constructor(
    private signInService: SignInService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,

  ) {
    // Temporary:
    this.signInService.logout();
    console.log('user is logged out');

    // redirect to home if already logged in
    if (this.signInService.currentUserValue) {
      console.log('user is already logged in');
      this.router.navigate(['/']);
    }
  }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  // convenience getter for easy access to form fields
  get f() { return this.loginForm.controls; }

  onSubmit() {
    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }

    this.submitted = true;
    this.loading = true;

    this.signInService.autenticate(this.f.username.value, this.f.password.value)
      .pipe(first())
      .subscribe(
        data => {
          this.router.navigate([this.returnUrl]);
        },
        error => {
          this.error = error;
          this.loading = false;
        });

    //this.signService.autenticate(this.UserModel).subscribe(loginDetails => console.log('Success', loginDetails))
  }
}
