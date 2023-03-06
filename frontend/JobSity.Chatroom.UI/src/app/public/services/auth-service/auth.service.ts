import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { UserI } from 'src/app/model/user.interface';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient, private snackbar: MatSnackBar) {}

  login(user: UserI): Observable<string> {
    return this.http
      .post('http://localhost:5111/api/User/login', user, { responseType: 'text' })
      .pipe(
        tap((e) => {
          var userLogin = JSON.parse(e);
          localStorage.setItem('nestjs_chat_app', userLogin.userJwt);
          this.http
            .get('http://localhost:5111/api/User', {
              responseType: 'text',
              headers: {
                'Content-Type': 'application/json',
                Authorization: `Bearer ${localStorage.getItem(
                  'nestjs_chat_app'
                )}`,
              },
            })
            .toPromise()
            .then((p: string) => {
              var user = JSON.parse(p);
              localStorage.setItem('userId', user.id);
            });
        }),
        tap(() =>
          this.snackbar.open('Login Successfull', 'Close', {
            duration: 2000,
            horizontalPosition: 'right',
            verticalPosition: 'top',
          })
        ),
        catchError((e) => {
          console.log(e);
          var error = JSON.parse(e.error);
          var errorMsg = '';
          Object.keys(error.errors).forEach(key => {
            errorMsg = key +" - "+ error.errors[key] + ' ';
          });

          this.snackbar.open(
            `User could not be created, due to: ${errorMsg}`,
            'Close',
            {
              duration: 5000,
              horizontalPosition: 'right',
              verticalPosition: 'top',
            }
          );
          return throwError(e);
        })
      );
  }

  create(user: UserI): Observable<string> {
    user.confirmPassword = user.password;
    return this.http
      .post('http://localhost:5111/api/User', user, {
        responseType: 'text',
      })
      .pipe(
        tap(() =>
          this.snackbar.open(`User created successfully`, 'Close', {
            duration: 2000,
            horizontalPosition: 'right',
            verticalPosition: 'top',
          })
        ),
        catchError((e) => {
          var error = JSON.parse(e.error);
          var errorMsg = '';
          Object.keys(error.errors).forEach(key => {
            errorMsg = key +" - "+ error.errors[key] + ' ';
          });

          this.snackbar.open(
            `User could not be created, due to: ${errorMsg}`,
            'Close',
            {
              duration: 5000,
              horizontalPosition: 'right',
              verticalPosition: 'top',
            }
          );
          return throwError(e);
        })
      );
  }
}
