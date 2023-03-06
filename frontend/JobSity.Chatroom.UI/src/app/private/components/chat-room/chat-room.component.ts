import {
  AfterViewInit,
  Component,
  ElementRef,
  Input,
  OnChanges,
  OnDestroy,
  OnInit,
  SimpleChanges,
  ViewChild,
} from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import { ChatService } from '../../services/chat-service/chat.service';
import { MessageI } from 'src/app/model/message.interface';
import { RoomI } from 'src/app/model/room.interface';
import { SignalrService } from 'src/app/public/services/signalr.service';

@Component({
  selector: 'app-chat-room',
  templateUrl: './chat-room.component.html',
  styleUrls: ['./chat-room.component.scss'],
})
export class ChatRoomComponent implements OnChanges, OnDestroy, AfterViewInit {
  @Input() chatRoom: RoomI;
  @ViewChild('messages') private messagesScroller: ElementRef;
  messagess: MessageI[] = [];

  chatMessage: FormControl = new FormControl(null, [Validators.required]);

  constructor(
    private chatService: ChatService,
    private signalrService: SignalrService
  ) {
    this.signalrService.newMessage.subscribe((value) => {
      if (value == this.chatRoom?.id) {
        this.getMessages(
          this.messagess.length > 0 ? this.messagess[0].createdOn : null
        );
      }
    });
  }

  ngOnChanges(changes: SimpleChanges) {
    this.messagess = [];
    if (this.chatRoom) {
      this.getMessages();
    }
  }

  getMessages(date?) {
    this.chatService.getMessages(this.chatRoom.id, date).then((x) => {
      this.messagess.unshift(...(x as MessageI[]));
    });
  }

  ngAfterViewInit() {
    if (this.messagesScroller) {
      this.scrollToBottom();
    }
  }

  ngOnDestroy() {}

  sendMessage() {
    this.chatService.sendMessage({
      text: this.chatMessage.value,
      room: this.chatRoom,
    });
    this.chatMessage.reset();
  }

  scrollToBottom(): void {
    try {
      setTimeout(() => {
        this.messagesScroller.nativeElement.scrollTop =
          this.messagesScroller.nativeElement.scrollHeight;
      }, 1);
    } catch {}
  }
}
