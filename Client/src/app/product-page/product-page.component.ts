import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Api, ProductResponseModel, ReviewResponseModel } from 'src/myApi';
import { ChangeDetectorRef } from '@angular/core';


@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.css']
})
export class ProductPageComponent implements OnInit {
  private api = new Api();
  product: any;
  quantity: number = 1; // Default quantity
  reviews: any;
  similarProducts: any;
  selectedColor: string | null = null;

  selectColor(color: string): void {
    this.selectedColor = color;
    console.log('Selected color:', color);
  }

  constructor(
    private route: ActivatedRoute,
    private router: Router,  // Inject Router service,
    private cdr: ChangeDetectorRef // Inject ChangeDetectorRef
  ) {}




  async ngOnInit(): Promise<void> {
    await this.api.api.productsList({ size: 4, page: 1 })
      .then((res) => res.json())
      .then((fulfilled: ProductResponseModel[]) => {
        this.similarProducts = fulfilled;
        console.log(this.similarProducts);
      })
      .catch((error) => {
        console.error('Error fetching product', error);
      });
  
    await this.route.paramMap.subscribe(async params => {
      const selectedProduct = params.get('selected');
      this.api.api.productsDetail(selectedProduct!)
        .then((res) => res.json())
        .then((fulfilled) => {
          this.product = fulfilled;
          console.log(this.product);
        })
        .catch((error) => {
          console.error('Error fetching product', error);
        });
  
      await this.api.api.reviewsProductDetail(selectedProduct!)
        .then((res) => res.json())
        .then((fulfilled: ReviewResponseModel[]) => {
          this.reviews = fulfilled;
          this.cdr.detectChanges(); // Trigger change detection here
        })
        .catch((error) => {
          console.error('Error fetching product', error);
        });
    });
  }
}
