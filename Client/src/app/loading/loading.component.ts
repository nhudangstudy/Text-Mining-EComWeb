import { Component } from '@angular/core';
import { LoadingService } from '../service/loading.service';

@Component({
  selector: 'app-loading',
  template: `
    <div *ngIf="loadingService.loading$ | async" class="loading-overlay">
      <div class="loading-animation">
        <!-- Custom loading animation -->
        <div class="spinner"></div>
      </div>
    </div>
  `,
  styleUrls: ['./loading.component.css'] // Adjust path if needed
})
export class LoadingComponent {
  constructor(public loadingService: LoadingService) {}
}
