import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { inject } from '@angular/core';
import { alert } from 'devextreme/ui/dialog';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { Location } from '@angular/common';

export class HttpService {
  constructor(protected http: HttpClient) {}
  private router = inject(Router);

  protected sendRequest<T>(
    url: string,
    method: string = 'GET',
    data: any = {}
  ): Observable<T> {
    let httpParams = data;
    //let httpOptions = { withCredentials: true, body: httpParams };
    let result: Observable<T> = undefined!;
    switch (method) {
      case 'GET':
        result = this.http
          .get<T>(url, {params: data})
          .pipe(retry(1), catchError(this.handleError));
        break;
      case 'PUT':
        result = this.http
          .put<T>(url, httpParams)
          .pipe( catchError(this.handleError));
        break;
      case 'POST':
        result = this.http
          .post<T>(url, httpParams)
          .pipe( catchError(this.handleError));
        break;
      case 'PATCH':
        result = this.http
          .patch<T>(url, httpParams)
          .pipe( catchError(this.handleError));
        break;
      case 'DELETE':
        result = this.http
          .delete<T>(url)
          .pipe( catchError(this.handleError));
        break;
    }

    return result;
  }

  handleError(e: any) {
    let message: string = '';
    if (e) {
      let excMessage: string = '';
      let additionExcMsg: string = '';
      console.error(e);

      if (e.error) {
        message = e.error ? e.error : '';
        excMessage = e.error.ExceptionMessage ? e.error.ExceptionMessage : '';
      }

      if (!e.error && e.message) {
        additionExcMsg = e.message ? e.message : '';
      }
      if (e.status === 404) {
        alert(
          `<p style="color:red;">Запрашиваемый ресурс не найден</p>`,
          'Ошибка'
        );
      } else if (e.status === 401) {
        alert(
          `<p style="color:red;">Время сессии истекло</p>`,
          'Ошибка'
        );
      } else {
        alert(
          `<p style="color:red;">${message} ${excMessage} ${additionExcMsg}</p>`,
          'Ошибка'
        );
      }
    } else {
      alert(`<p style="color:red;">Неизвестная ошибка</p>`, 'Ошибка');
    }

    return throwError(() => {
      return message;
    });
  }
}
