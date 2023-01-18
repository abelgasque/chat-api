import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class MessagesService {

  constructor(private messageService: MessageService) { }

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
}
