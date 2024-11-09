import { Component, Input } from '@angular/core';
import { ReviewResponseModel } from 'src/myApi';

@Component({
  selector: 'app-comments-list',
  templateUrl: './comments-list.component.html',
  styleUrls: ['./comments-list.component.css']
})
export class CommentsListComponent {
  @Input() reviews: ReviewResponseModel[] = [];
  displayedReviews: ReviewResponseModel[] = []; // Initialized properly
  reviewsPerPage: number = 3; // Number of reviews to display per page
  currentPage: number = 1;

  ngOnInit() {
    // Preload the first review
    this.loadMoreReviews();
  }

  loadMoreReviews() {
    const startIndex = (this.currentPage - 1) * this.reviewsPerPage;
    const endIndex = startIndex + this.reviewsPerPage;
    this.displayedReviews = [
      ...this.displayedReviews,
      ...this.reviews.slice(startIndex, endIndex)
    ];
    this.currentPage++;
  }

  hasMoreReviews(): boolean {
    return (this.displayedReviews?.length || 0) < (this.reviews?.length || 0);
  }
}
