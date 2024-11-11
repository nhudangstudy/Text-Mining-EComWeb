import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  Api,
  ProductResponseModel,
  ReviewResponseModel,
  CreateReviewRequest,
} from 'src/myApi';
import { ChangeDetectorRef } from '@angular/core';
import { AuthService } from '../service/auth.service';
import { LoadingService } from '../service/loading.service';
import { MessageBoxService } from '../service/message-box-service.service';

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.css'],
})
export class ProductPageComponent implements OnInit {
  private api = new Api();
  product: any;
  quantity: number = 1;
  reviews: any;
  similarProducts: any;
  selectedColor: string | null = null;
  isLoggedIn: boolean = false;

  // Review form properties
  reviewTitle: string = '';
  starRating: number = 0;
  reviewContent: string = '';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private cdr: ChangeDetectorRef,
    private authService: AuthService,
    private loadingService: LoadingService,
    private messageBoxService: MessageBoxService
  ) {}

  ngOnInit(): void {
    this.loadingService.startLoading();
    this.isLoggedIn = this.authService.isAuthenticated();
    this.route.paramMap.subscribe(async (params) => {
      const selectedProduct = params.get('selected');
      await this.api.api
        .productsDetail(selectedProduct!)
        .then((res) => res.json())
        .then((fulfilled) => {
          this.product = fulfilled;
        })
        .catch((error) => {
          console.error('Error fetching product', error);
        });

      await this.api.api
        .productsList({ size: 4, page: 1 })
        .then((res) => res.json())
        .then((fulfilled: ProductResponseModel[]) => {
          this.similarProducts = fulfilled;
          this.cdr.detectChanges();
        })
        .catch((error) => {
          console.error('Error fetching product', error);
        });

      await this.api.api
        .reviewsProductDetail(selectedProduct!)
        .then((res) => res.json())
        .then((fulfilled: ReviewResponseModel[]) => {
          this.reviews = fulfilled;
          this.loadingService.stopLoading();

          this.cdr.detectChanges();
          
        })
        .catch((error) => {
          this.reviews = [];
          console.error('Error fetching product', error);
        });
    });
    this.cdr.detectChanges();
  }

  // Method to submit a new review
  async submitReview(): Promise<void> {
    if (this.isLoggedIn) {
      const reviewData: CreateReviewRequest = {
        title: this.reviewTitle,
        imageUrl: 'https://picsum.photos/200/300',
        starRating: this.starRating,
        reviewContent: this.reviewContent,
      };

      // Retrieve the JWT token from your AuthService
      const token = this.authService.getToken();

      this.loadingService.startLoading();
      // Ensure the request is made with the Authorization header
      await this.api.api
        .reviewsCreate(this.product.asin, reviewData, {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        })
        .then(() => {
          this.messageBoxService.success('Review submitted successfully');
          // Optionally refresh the reviews list
        })
        .catch((error) => {
          this.messageBoxService.error('Error submitting review: ' + error);
        });
      this.ngOnInit();
      this.loadingService.stopLoading();
    }
  }

  selectColor(color: string): void {
    this.selectedColor = color;
    console.log('Selected color:', color);
  }
}
