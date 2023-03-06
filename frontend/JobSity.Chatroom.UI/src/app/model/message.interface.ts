import { Meta } from '@angular/platform-browser';
import { RoomI } from './room.interface';
import { UserI } from './user.interface';

export interface MessageI {
  id?: string;
  text: string;
  createdUserName?: string;
  createdUserId?: string;
  room: RoomI;
  createdOn?: Date;
  updated_at?: Date;
}

export interface MessagePaginateI {
  items: MessageI[];
  meta: Meta;
}
