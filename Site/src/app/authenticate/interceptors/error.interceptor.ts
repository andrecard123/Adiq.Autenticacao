import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    constructor(private readonly router: Router) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            return this.handleError(err);
        }));
    }

    private handleError(httpErrorResponse: HttpErrorResponse) {
        if (httpErrorResponse.error != null) {
            const mensagens = httpErrorResponse.error.map((item) => {
                return item['message'];
            });
            return throwError(mensagens);
        } else {
            return throwError('Erro inesperado. Por favor, entre em contato com o suporte.');
        }
    }
}
