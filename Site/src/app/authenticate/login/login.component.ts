import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';
import { ICredential } from '../models/credential';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { IAuthenticatedUser } from '../models/autenticatedUser';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form: FormGroup;

  constructor(
    private readonly loginService: LoginService,
    private readonly formBuilder: FormBuilder
  ) {
    this.form = this.formBuilder.group({
      username: ['lucas', Validators.required],
      password: ['adiq', Validators.required]
    });
  }

  ngOnInit() {
  }

  authenticateCredential(): void {
    console.log(this.form.value);
    const credential = this.form.value as ICredential;
    this.loginService.authenticateCredential(credential)
      .subscribe(response => {
        const authenticatedUser = response as IAuthenticatedUser;
        console.log(`Usuário autenticado com token ${authenticatedUser.token} com validade até ${authenticatedUser.expiresOn}.`);
      });
  }
}
