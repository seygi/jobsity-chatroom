import * as signalR from '@microsoft/signalr';

import { BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { tokenGetter } from 'src/app/app.module';

@Injectable({
  providedIn: 'root',
})
export class SignalrService {
  private hubConnection: signalR.HubConnection;
  newMessage: BehaviorSubject<string> = new BehaviorSubject('');

  constructor() {
    this.startConnection();
  }
  public startConnection = () => {
    const options: signalR.IHttpConnectionOptions = {
      accessTokenFactory: () => {
        return tokenGetter();
      },
    };
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5111/chat-room-hub', options)
      .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch((err) => console.log('Error while starting connection: ' + err));

    this.hubConnection.on('ReceiveMessage', (e) => {
      this.newMessage.next(e.chatRoomId);
    });
  };

  public sendMessage(chatRoomId: string, text: string) {
    this.hubConnection.invoke('SendMessage', chatRoomId, text);
  }
}
