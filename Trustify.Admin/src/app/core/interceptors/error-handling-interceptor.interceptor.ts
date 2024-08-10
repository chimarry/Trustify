import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, catchError, Observable, ObservableInput } from 'rxjs';
import { HttpUtilServiceService } from '../services/http-util-service.service';
import { Messages } from '../models/messages';
import { DisplayMessageService } from '../services/display-message.service';

@Injectable({
  providedIn: 'root'
})
/**
 * Handles errors that happened during sending/receving HTTP request.
 */
export class ErrorHandlingInterceptorService implements HttpInterceptor {

  constructor(
    private displayMessageService: DisplayMessageService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<Object>> {
    console.log("intercepter")
    let authReq = req;

    return next.handle(req).pipe(
      catchError(error => {
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
        this.displayMessageService.displayHttpStatusCode(error.status);
      }
    }
    throw error;
  }

}