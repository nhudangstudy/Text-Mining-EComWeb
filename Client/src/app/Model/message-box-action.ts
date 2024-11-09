export default interface MessageBoxAction {
    label: string;
    class?: string;
    action: () => void;
  }