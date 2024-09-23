import { Component, HostListener } from '@angular/core';

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent {
  
  prevScrollpos: number = window.pageYOffset;
  isNavbarVisible: boolean = true;

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
