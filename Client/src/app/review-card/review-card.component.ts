import { Component, Input, OnInit } from '@angular/core';
import { Api, ReviewResponseModel} from 'src/myApi';

@Component({
  selector: 'app-review-card',
  templateUrl: './review-card.component.html',
  styleUrls: ['./review-card.component.css']
})
export class ReviewCardComponent {
  @Input() review!: ReviewResponseModel;
  isExpanded = false;
  private api = new Api();

  toggleText() {
    this.isExpanded = !this.isExpanded;
  }


}