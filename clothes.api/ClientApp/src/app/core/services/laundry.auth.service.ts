import { Injectable } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { LoginViewModel } from '../view-models/login.view.model.cs';

@Injectable()
export class LaundryAuthService {

  private loggedIn: boolean;

  constructor(private httpClient: HttpClient) {
    this.loggedIn = !!localStorage.getItem("auth_token");
  }

  login(loginViewModel: LoginViewModel) {

  }

  isLoggedIn(){
    return this.loggedIn;
  }

}
