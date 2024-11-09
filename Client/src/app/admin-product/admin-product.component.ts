import { Component } from '@angular/core';


interface Product {
  id: number;
  name: string;
  category: string;
  price: string;
  stock: 'In Stock' | 'Out of Stock';
}

@Component({
  selector: 'app-admin-product',
  templateUrl: './admin-product.component.html',
  styleUrls: ['./admin-product.component.css']
})

export class AdminProductComponent {
  products: Product[] = [
    { id: 1, name: 'Product 1', category: 'Category A', price: '$50.00', stock: 'In Stock' },
    { id: 2, name: 'Product 2', category: 'Category B', price: '$75.00', stock: 'Out of Stock' },
    { id: 3, name: 'Product 3', category: 'Category A', price: '$100.00', stock: 'In Stock' },
    { id: 4, name: 'Product 4', category: 'Category C', price: '$120.00', stock: 'In Stock' },
    { id: 5, name: 'Product 5', category: 'Category B', price: '$80.00', stock: 'Out of Stock' }
  ];
}
