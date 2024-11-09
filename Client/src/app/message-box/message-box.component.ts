import { Component, Inject, OnInit } from '@angular/core';
import { trigger, style, transition, animate, keyframes } from '@angular/animations';
import { Dialog, DIALOG_DATA } from '@angular/cdk/dialog';
import MessageBoxData from '../Model/message-box-data';
import { MessageLevel } from '../Model/message-box-level';

@Component({
  selector: 'app-message-box',
  templateUrl: './message-box.component.html',
  styleUrls: ['./message-box.component.css'],
  animations: [
    trigger('slideIn', [
      transition(':enter', [
        style({ transform: 'translateX(-100%)', opacity: 0 }),
        animate('0.5s ease-out', style({ transform: 'translateX(0)', opacity: 1 }))
      ]),
      transition(':leave', [
        animate('0.5s ease-in', style({ transform: 'translateX(-100%)', opacity: 0 }))
      ])
    ])
  ]
})
export class MessageBoxComponent implements OnInit {

  iconClass?: string;
  boxClass?: string;

  constructor(@Inject(DIALOG_DATA) public data: MessageBoxData, private dialog: Dialog) {
    switch (data.level) {
      case MessageLevel.Success:
        this.iconClass = 'bi bi-check-circle-fill';
        this.boxClass = 'success';
        break;
      case MessageLevel.Error:
        this.iconClass = 'bi bi-exclamation-triangle-fill';
        this.boxClass = 'error';
        break;
      case MessageLevel.Warning:
        this.iconClass = 'bi bi-exclamation-diamond-fill';
        this.boxClass = 'warning';
        break;
      case MessageLevel.Info:
        this.iconClass = 'bi bi-info-circle-fill';
        this.boxClass = 'info';
        break;
    }

    if (!data.actions || data.actions.length === 0) {
      data.actions = [{
        label: 'Đóng',
        class: 'btn btn-light',
        action: () => {
          this.dialog.closeAll();
        }
      }];
    } else {
      for (let action of data.actions) {
        if (!action.class) {
          action.class = 'btn ' + this.boxClass;
        }
      }
    }
  }

  ngOnInit(): void { }

  closeDialog(): void {
    this.dialog.closeAll();
  }
}
