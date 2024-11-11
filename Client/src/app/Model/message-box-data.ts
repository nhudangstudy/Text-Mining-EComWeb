import MessageBoxAction from "./message-box-action";
import { MessageLevel } from "./message-box-level";

export default class MessageBoxData {
  title!: string;
  message!: string;
  level: MessageLevel = MessageLevel.Show;
  actions?: Array<MessageBoxAction>;
}