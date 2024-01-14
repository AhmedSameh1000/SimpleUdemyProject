import { Injectable, Injector } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, catchError, switchMap, throwError } from 'rxjs';
import { AuthService } from '../Services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private inject: Injector, private authservice: AuthService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    if (!this.authservice.isLoggedIn()) {
      return next.handle(request);
    }
    let authreq = request;
    authreq = this.AddTokenheader(request, this.authservice.GetToken());
    return next.handle(authreq).pipe(
      catchError((errordata: any) => {
        if (errordata.status === 401) {
          return this.handleRefrehToken(request, next);
        }
        return throwError(errordata);
      })
    );
  }
  handleRefrehToken(request: HttpRequest<any>, next: HttpHandler) {
    let authservice = this.inject.get(AuthService);
    return authservice.RefreshToken(this.authservice.GetUserId()).pipe(
      switchMap((Response: any) => {
        authservice.SaveTokens(Response);
        return next.handle(this.AddTokenheader(request, Response.data.token));
      }),
      catchError((errodata) => {
        return throwError(errodata);
      })
    );
  }

  AddTokenheader(request: HttpRequest<any>, token: any) {
    return request.clone({
      headers: request.headers.set('Authorization', 'bearer ' + token),
    });
  }
}
