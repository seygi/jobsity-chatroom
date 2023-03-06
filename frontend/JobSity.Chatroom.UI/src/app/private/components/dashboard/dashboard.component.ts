import { AfterViewInit, Component, OnInit } from '@angular/core';

import { ChatService } from '../../services/chat-service/chat.service';
import { MatSelectionListChange } from '@angular/material/list';
import { RoomI } from 'src/app/model/room.interface';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent implements OnInit, AfterViewInit {
  rooms: RoomI[] = [];
  selectedRoom = null;

  constructor(private chatService: ChatService) {}

  async ngOnInit() {
    let x = await this.chatService.getMyRooms();
    this.rooms = x as RoomI[];
  }

  ngAfterViewInit() {}

  onSelectRoom(event: MatSelectionListChange) {
    this.selectedRoom = event.source.selectedOptions.selected[0].value;
  }
}
