import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { Api } from 'src/myApi';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Client';
  showHeader: boolean = true;

  constructor(private router: Router) {
    // Listen for route changes and set showHeader based on the current route
    this.router.events.subscribe(() => {
      // You can customize this check based on the exact routes you want to exclude
      const currentRoute = this.router.url;
      this.showHeader = currentRoute === '/' || currentRoute === '/landing';
    });
  }
}

