import { Dialog } from '@angular/cdk/dialog';
import { Injectable } from '@angular/core';
import { MessageBoxComponent } from 'src/app/message-box/message-box.component';
import MessageBoxData from '../Model/message-box-data';
import { MessageLevel } from '../Model/message-box-level';

@Injectable({
  providedIn: 'root'
})
export class MessageBoxService {

  constructor(private dialog: Dialog) { }

  show(messageData:MessageBoxData){
    this.dialog.open(MessageBoxComponent, { data: messageData });
  }

  showByCode(messageCode:string){
    let data: MessageBoxData = new MessageBoxData();
    data.title = 'Mã thông báo';
    data.message = messageCode;
    data.level = MessageLevel.Info;
    this.dialog.open(MessageBoxComponent, { data: data });
  }

  error(message:string){
    let data: MessageBoxData = new MessageBoxData();
    data.title = 'Lỗi';
    data.message = message;
    data.level = MessageLevel.Error;
    this.dialog.open(MessageBoxComponent, { data: data });
  }

  success(message:string){
    let data: MessageBoxData = new MessageBoxData();
    data.title = 'Thành công';
    data.message = message;
    data.level = MessageLevel.Success;
    this.dialog.open(MessageBoxComponent, { data: data });
  }

  warning(message:string){
    let data: MessageBoxData = new MessageBoxData();
    data.title = 'Cảnh báo';
    data.message = message;
    data.level = MessageLevel.Warning;
    this.dialog.open(MessageBoxComponent, { data: data });
  }

  info(message:string){
    let data: MessageBoxData = new MessageBoxData();
    data.title = 'Thông tin';
    data.message = message;
    data.level = MessageLevel.Info;
    this.dialog.open(MessageBoxComponent, { data: data });
  }
}