import { Component, HostListener } from '@angular/core';
import { Product

 } from '../Model/product-model';
import { Review } from '../Model/review-model';
@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent {

  prevScrollpos: number = window.pageYOffset;
  isNavbarVisible: boolean = true;

  productNewArrivals: Product[] = [
    {
      
      productASIN: null,
      name: 'Xiaomi Redmi Note 13',
      price: 120,
      rating: 4.5,
      imageUrl: 'path-to-image-1',
      oldPrice: null,
      discount: null
    },
    {
      productASIN: null,
      name: 'Samsung Galaxy S23 FE 5G',
      price: 240,
      rating: 3.5,
      imageUrl: 'path-to-image-2',
      oldPrice: 260,
      discount: 20
    },
    {
      productASIN: null,
      name: 'iPhone 15 Pro Max',
      price: 180,
      rating: 4.5,
      imageUrl: 'path-to-image-3',
      oldPrice: null,
      discount: null
    },
    {
      productASIN: null,
      name: 'OPPO Reno12 5G',
      price: 130,
      rating: 4.5,
      imageUrl: 'path-to-image-4',
      oldPrice: 160,
      discount: 30
    }
  ];

  productSpecialOffers: Product[] = [
    {
      
      productASIN: null,
      name: 'Xiaomi Redmi Note 13',
      price: 120,
      rating: 4.5,
      imageUrl: 'path-to-image-1',
      oldPrice: null,
      discount: null
    },
    {
      productASIN: null,
      name: 'Samsung Galaxy S23 FE 5G',
      price: 240,
      rating: 3.5,
      imageUrl: 'path-to-image-2',
      oldPrice: 260,
      discount: 20
    },
    {
      productASIN: null,
      name: 'iPhone 15 Pro Max',
      price: 180,
      rating: 4.5,
      imageUrl: 'path-to-image-3',
      oldPrice: null,
      discount: null
    },
    {
      productASIN: null,
      name: 'OPPO Reno12 5G',
      price: 130,
      rating: 4.5,
      imageUrl: 'path-to-image-4',
      oldPrice: 160,
      discount: 30
    },
    {
      
      productASIN: null,
      name: 'Xiaomi Redmi Note 13',
      price: 120,
      rating: 4.5,
      imageUrl: 'path-to-image-1',
      oldPrice: null,
      discount: null
    },
    {
      productASIN: null,
      name: 'Samsung Galaxy S23 FE 5G',
      price: 240,
      rating: 3.5,
      imageUrl: 'path-to-image-2',
      oldPrice: 260,
      discount: 20
    },
    {
      productASIN: null,
      name: 'iPhone 15 Pro Max',
      price: 180,
      rating: 4.5,
      imageUrl: 'path-to-image-3',
      oldPrice: null,
      discount: null
    },
    {
      productASIN: null,
      name: 'OPPO Reno12 5G',
      price: 130,
      rating: 4.5,
      imageUrl: 'path-to-image-4',
      oldPrice: 160,
      discount: 30
    }
  ];


  reviews: Review[] = [
    {
      reviewerName: 'Sarah M.',
      reviewText: 'Amazing service and products!',
      starRating: 5,
      isVerified: true
    },
    {
      reviewerName: 'John D.',
      reviewText: 'The quality is top-notch, will definitely shop againThe quality is top-notch, will definitely shop againThe quality is top-notch, will definitely shop againThe quality is top-notch, will definitely shop againThe quality is top-notch, will definitely shop againThe quality is top-notch, will definitely shop againThe quality is top-notch, will definitely shop againThe quality is top-notch, will definitely shop again!',
      starRating: 4,
      isVerified: true
    },
    {
      reviewerName: 'Emily T.',
      reviewText: 'Fast shipping and excellent customer support.',
      starRating: 5,
      isVerified: false
    },
    {
      reviewerName: 'Michael R.',
      reviewText: 'Great range of products and reasonable prices.',
      starRating: 4,
      isVerified: true
    }
    // Add more reviews if needed
  ];

  @HostListener('window:scroll', ['$event'])
  onWindowScroll() {
    const currentScrollPos = window.pageYOffset;

    if (this.prevScrollpos > currentScrollPos) {
      this.isNavbarVisible = true;
    } else {
      this.isNavbarVisible = false;
    }

    this.prevScrollpos = currentScrollPos;
  }
}
