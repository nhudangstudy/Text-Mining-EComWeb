import { Component } from '@angular/core';

interface Transaction {
  id: string;
  date: string;
  email: string;
  status: 'Pending' | 'Completed';
  document: string;
  amount: string;
}

@Component({
  selector: 'app-admin-transaction',
  templateUrl: './admin-transaction.component.html',
  styleUrls: ['./admin-transaction.component.css']
})
export class AdminTransactionComponent {
  transactions: Transaction[] = [
    { id: '202410926', date: '12 June, 2021', email: 'samantha@email.com', status: 'Pending', document: 'ticket001.pdf', amount: '- $60.00' },
    { id: '528239322', date: '12 June, 2021', email: 'soap@email.com', status: 'Completed', document: 'ticket002.pdf', amount: '- $750.00' },
    { id: '5173936204', date: '12 June, 2021', email: 'achmad@email.com', status: 'Pending', document: 'ticket003.pdf', amount: '- $150.00' },
    { id: '1842947593', date: '12 June, 2021', email: 'hope@email.com', status: 'Completed', document: 'ticket004.pdf', amount: '- $50.00' },
    { id: '6284959473', date: '12 June, 2021', email: 'cole@email.com', status: 'Completed', document: 'ticket005.pdf', amount: '- $10.00' },
    { id: '7365274059', date: '12 June, 2021', email: 'john@email.com', status: 'Pending', document: 'ticket006.pdf', amount: '- $650.00' }
  ];
}
