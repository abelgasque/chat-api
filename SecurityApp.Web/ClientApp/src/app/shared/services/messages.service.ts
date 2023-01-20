import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { MessageService } from 'primeng/api';

import { SharedService } from './shared.service';

@Injectable({
  providedIn: 'root'
})
export class MessagesService {

  constructor(
    private messageService: MessageService,
    private sharedService: SharedService
  ) { }

  success(title: string, description: string) {
    this.messageService.add({
      severity: 'success',
      summary: title,
      detail: description
    });
  }

  error(title: string, description: string) {
    this.messageService.add({
      severity: 'error',
      summary: title,
      detail: description
    });
  }

  public errorHandler(error: any) {
    let msg: string = 'Error processing remote service. Try again.';

    if (error instanceof HttpErrorResponse && error.status >= 400 && error.status <= 499) {
      msg = 'There was an error processing your request';

      if (error.status === 401 || error.status === 403) {
        msg = 'You do not have permission to perform this action.';
      }
    }

    if ('error' in error.error) {
      msg = error.error.error;
    }

    console.error('An error has occurred', error);
    this.sharedService.closeSpinner();
    this.error('Error', msg);
  }
}
