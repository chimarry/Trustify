import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponseBase } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, catchError, map, Observable, ObservableInput } from 'rxjs';
import { Messages } from '../models/messages';
import { DisplayMessageService } from '../services/display-message.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
/**
 * Handles errors that happened during sending/receving HTTP request.
 */
export class ErrorHandlingInterceptorService implements HttpInterceptor {

  private router: Router = inject(Router);
  constructor(
    private displayMessageService: DisplayMessageService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<Object>> {
    let authReq = req;

    return next.handle(req).pipe(
      catchError(error => {
        if (error instanceof HttpErrorResponse) {
          if (error.message.includes('Http failure during parsing')) {
            this.router.navigate([''])
          }
        }
        return this.handleError(error, authReq, next);
      }));
  }

  private handleError(error: any, request: HttpRequest<any>, next: HttpHandler): ObservableInput<any> {
    if (error instanceof ErrorEvent) {
      this.displayMessageService.displayMessage(Messages.CLIENT_ERROR);
    }
    else if (error instanceof HttpErrorResponse) {
      if (error.error && error.error.status) {
        this.displayMessageService.displayStatus(error.error.status);
      }
      else if (error.status && error.status != 401) {
        console.log(error)
        this.displayMessageService.displayHttpStatusCode(error.status);
      } else if (error.status && error.status == 401) {
        this.displayMessageService.displayHttpStatusCode(error.status);
      }
    }
    throw error;
  }

}