import { Component, Input, OnInit } from '@angular/core';

import { AuthService } from 'src/app/public/services/auth-service/auth.service';
import { MessageI } from 'src/app/model/message.interface';

@Component({
  selector: 'app-chat-message',
  templateUrl: './chat-message.component.html',
  styleUrls: ['./chat-message.component.scss'],
})
export class ChatMessageComponent {
  userId = localStorage.getItem('userId');

  @Input() message: MessageI;

  constructor() {}
}
