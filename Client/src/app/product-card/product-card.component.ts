import { Component, Input } from '@angular/core';
import { Product } from '../Model/product-model';

import { ProductResponseModel } from 'src/myApi';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.css']
})
export class ProductCardComponent {
  @Input() product!: ProductResponseModel;

  constructor(
    private route: ActivatedRoute,
    private router: Router  // Inject Router service
  ) {}
  
  navigateToProduct(productAsin: string): void {
    this.router.navigate(['/product', productAsin]); // Ensure `productId` is defined and not null.

  }
}
