import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { OperationStatus } from '../models/operation-status';
import { Messages } from '../models/messages';

@Injectable({
  providedIn: 'root',
})
export class DisplayMessageService {

  constructor(private snackBar: MatSnackBar) { }

  public displayString(message: string) {
    this.snackBar.open(message, undefined, {
      duration: 2000, panelClass: ['mb-snackbar']
    })
  }

  public displayStatus(status: OperationStatus) {
    let message = Messages.UNKNOWN_ERROR;
    switch (status) {
      case OperationStatus.Success: message = Messages.SUCCESS; break;
      case OperationStatus.InvalidData: message = Messages.INVALID_DATA; break;
      case OperationStatus.DatabaseError: message = Messages.DATABASE_ERROR; break;
      case OperationStatus.Exists: message = Messages.EXISTS; break;
      case OperationStatus.FileSystemError: message = Messages.FILE_SYSTEM_ERROR; break;
      case OperationStatus.UnknownError: message = Messages.UNKNOWN_ERROR; break;
      case OperationStatus.NotCompatible: message = Messages.NOT_COMPATIBLE; break;
      case OperationStatus.NotSupported: message = Messages.NOT_SUPPORTED; break;
      case OperationStatus.NotFound: message = Messages.NOT_FOUND; break;
      default: message = Messages.ERROR;
    }
    this.displayMessage(message);
  }

  public displayMessage(message: Messages) {
    this.snackBar.open(Messages[message], undefined, {
      duration: 2000, panelClass: ['mb-snackbar']
    })
  }

  public displayHttpStatusCode(status: number) {
    let message = Messages.UNKNOWN_ERROR;
    switch (status) {
      case 204: message = Messages.NOT_FOUND; break;
      case 403: message = Messages.FORBIDDEN_ACCESS; break;
      case 401: message = Messages.UNAUTHROIZED; break;
      case 424: message = Messages.MACHINE_DISCONNECTED; break;
      case 422: message = Messages.INVALID_DATA; break;
      case 429: message = Messages.TOO_MANY_REQUEST; break;
      case 404: message = Messages.CLIENT_ERROR; break;
      case 500:
      case 501:
      case 502:
      case 503:
      case 504: message = Messages.INTERNAL_SERVER_ERROR; break;
    }
    this.displayMessage(message);
  }
}
