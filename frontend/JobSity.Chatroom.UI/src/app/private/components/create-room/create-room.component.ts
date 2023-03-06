import { ActivatedRoute, Router } from '@angular/router';
import { FormArray, FormControl, FormGroup, Validators } from '@angular/forms';

import { ChatService } from '../../services/chat-service/chat.service';
import { Component } from '@angular/core';

@Component({
  selector: 'app-create-room',
  templateUrl: './create-room.component.html',
  styleUrls: ['./create-room.component.scss'],
})
export class CreateRoomComponent {
  form: FormGroup = new FormGroup({
    name: new FormControl(null, [Validators.required]),
  });

  constructor(
    private chatService: ChatService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {}

  async create() {
    if (this.form.valid) {
      let x = await this.chatService.createRoom(this.form.getRawValue());
      if (x) {
        this.router.navigate(['../dashboard'], {
          relativeTo: this.activatedRoute,
        });
      }
    }
  }

  get name(): FormControl {
    return this.form.get('name') as FormControl;
  }

  get description(): FormControl {
    return this.form.get('description') as FormControl;
  }

  get users(): FormArray {
    return this.form.get('users') as FormArray;
  }
}
