// admin-report.component.ts
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Api, HttpResponse, ReviewResponseModel } from 'src/myApi';
import { SentimentService


 } from '../service/sentiment.service';
interface ReviewWithSentiment extends ReviewResponseModel {
  sentiment?: string;
}

@Component({
  selector: 'app-admin-report',
  templateUrl: './admin-report.component.html',
  styleUrls: ['./admin-report.component.css']
})
export class AdminReportComponent implements OnInit {

  private api = new Api();
  reviews: ReviewWithSentiment[] = [];

  totalComments = 0;
  averageRating = '';
  productsSold = 150;
  totalPositiveComments = 0;
  totalNegativeComments = 0;
  totalNeutralComments = 0;

  positivePercentage: number = 0;
  neutralPercentage: number = 0;
  negativePercentage: number = 0;

  // Chart data
  sentimentChartData: any[] = [];
  reviewSummaryChartData: any[] = [];

  // Color scheme for charts
  colorScheme = {
    domain: ['#28a745', '#ffc107', '#dc3545'] // Green, Yellow, Red
  };

  constructor(private sentimentService: SentimentService, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.fetchReviews();
  }

  async fetchReviews(): Promise<void> {
    try {
      const response: HttpResponse<ReviewResponseModel[]> = await this.api.api.reviewsFeaturedList({ count: 10, minimumRating: 1 });
      console.log('API response:', response);
      const responseBody = response.data;
      if (responseBody && Array.isArray(responseBody)) {
        this.reviews = responseBody.map(review => review as ReviewWithSentiment);
        await this.analyzeSentiments();
        this.cdr.detectChanges();
      } else {
        console.error('Unexpected response format or empty body.');
      }
    } catch (error) {
      console.error('Error fetching reviews:', error);
    }
  }

  async analyzeSentiments(): Promise<void> {
    const sentimentPromises = this.reviews.map(async review => {
      try {
        const sentimentResponse = await this.sentimentService.getSentiment(review.reviewContent!).toPromise();
        review.sentiment = sentimentResponse.sentiment; // Adjust according to actual response structure
      } catch (error) {
        console.error(`Error analyzing sentiment for review ${review.id}:`, error);
        review.sentiment = 'Unknown';
      }
    });

    await Promise.all(sentimentPromises);
    this.calculateKPIs();
    this.cdr.detectChanges();
  }

  calculateKPIs(): void {
    this.totalComments = this.reviews.length;

    const totalRating = this.reviews.reduce((sum, review) => sum + (review.starRating || 0), 0);
    const avgRating = totalRating / this.totalComments;

    this.averageRating = avgRating ? avgRating.toFixed(1) : '0.0';

    // Reset counts
    this.totalPositiveComments = 0;
    this.totalNegativeComments = 0;
    this.totalNeutralComments = 0;

    // Count sentiments
    this.reviews.forEach(review => {
      switch (review.sentiment) {
        case 'positive':
          this.totalPositiveComments++;
          break;
        case 'neutral':
          this.totalNeutralComments++;
          break;
        case 'negative':
          this.totalNegativeComments++;
          break;
        default:
          break;
      }
    });

    // Calculate percentages
    if (this.totalComments > 0) {
      this.positivePercentage = (this.totalPositiveComments / this.totalComments) * 100;
      this.neutralPercentage = (this.totalNeutralComments / this.totalComments) * 100;
      this.negativePercentage = (this.totalNegativeComments / this.totalComments) * 100;
    } else {
      this.positivePercentage = 0;
      this.neutralPercentage = 0;
      this.negativePercentage = 0;
    }

    // Prepare chart data
    this.sentimentChartData = [
      { name: 'Positive', value: this.totalPositiveComments },
      { name: 'Neutral', value: this.totalNeutralComments },
      { name: 'Negative', value: this.totalNegativeComments }
    ];

    this.reviewSummaryChartData = [...this.sentimentChartData];

    // Log report
    console.log('Detailed Analysis Report:');
    console.log(`Total Comments: ${this.totalComments}`);
    console.log(`Average Rating: ${this.averageRating}`);
    console.log(`Positive Comments: ${this.totalPositiveComments}`);
    console.log(`Neutral Comments: ${this.totalNeutralComments}`);
    console.log(`Negative Comments: ${this.totalNegativeComments}`);
  }
}
