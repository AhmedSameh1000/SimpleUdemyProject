import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private httpclient: HttpClient) {}

  LogIn(logInModel: any) {
    return this.httpclient.post(
      'http://localhost:5227/api/Auth/LogIn',
      logInModel
    );
  }
  GetData() {
    return this.httpclient.get('http://localhost:5227/api/Auth/SecureData');
  }
  JWTHealper = new JwtHelperService();
  GetUserId() {
    if (this.isLoggedIn()) {
      var decodedToken = this.GetDecodedToken();
      return decodedToken.Id;
    }
  }

  Signup(SignupModel: any) {
    return this.httpclient.post(
      'http://localhost:5227/api/Auth/Register',
      SignupModel
    );
  }

  GetDecodedToken(): any {
    if (this.isLoggedIn()) {
      const token = this.GetToken();
      var decodedToken = this.JWTHealper.decodeToken(token);
      return decodedToken;
    }
  }

  IsAdmin(): any {
    if (this.isLoggedIn()) {
      var decodedToken = this.GetDecodedToken();
      var Roles: string[] = decodedToken.roles;
      if (Roles.includes('Admin')) {
        return true;
      } else {
        return false;
      }
    }
  }
  isLoggedIn() {
    if (
      localStorage.getItem('token') != null &&
      localStorage.getItem('token') != undefined
    ) {
      return true;
    }
    return false;
  }
  RefreshToken(userId: any) {
    return this.httpclient.post(
      `http://localhost:5227/api/Auth/RefreshToken?userId=${userId}`,
      {}
    );
  }
  SaveTokens(response: any) {
    localStorage.setItem('token', response.data.token);
  }

  GetToken(): any {
    if (this.isLoggedIn()) {
      return localStorage.getItem('token');
    }
  }
  logout() {
    localStorage.removeItem('token');
  }
}
