import { Component, HostListener, OnInit } from '@angular/core';
import { Api, ProductResponseModel, ReviewResponseModel } from 'src/myApi';

import { Product } from '../Model/product-model';
import { Review } from '../Model/review-model';
@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent implements OnInit {

  private api = new Api();
  prevScrollpos: number = window.pageYOffset;
  isNavbarVisible: boolean = true;

  productNewArrivals: ProductResponseModel[] = []


  reviews: ReviewResponseModel[] = []

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

  ngOnInit(): void {
    this.api.api.productsList({ size: 4, page: 2 })
      .then((res) => res.json()) // Parse the response as JSON
      .then((parsedResponse: ProductResponseModel[]) => {
        this.productNewArrivals = parsedResponse;
      })
      .catch(err => {
        console.error('Error fetching product list:', err);
      });
    
    this.api.api.reviewsFeaturedList({ count: 6, minimumRating: 4 })
    .then((res)  => res.json())
    .then((parsedResponse: ReviewResponseModel[]) => {
      this.reviews = parsedResponse;
    })
    .catch(err => {
      console.error('Error fetching reviews list:', err);
    });
    
  }
}
