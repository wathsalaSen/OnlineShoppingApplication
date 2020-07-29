import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from './user';
import { Router, ActivatedRoute } from '@angular/router';
import { first } from 'rxjs/operators';
import { SignService } from './sign.service';

@Component({
  selector: 'app-sign',
  templateUrl: './sign.component.html',
  styleUrls: ['./sign.component.css']
})
export class SignComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  error = '';

  public UserModel = new User;

  constructor(
    private signService: SignService,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,

  ) {
    // Temporary:
    this.signService.logout();
    console.log('use is logged out');

    // redirect to home if already logged in
    if (this.signService.currentUserValue) {
      console.log('use is already logged in');
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

    this.signService.autenticate(this.f.username.value, this.f.password.value)
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
