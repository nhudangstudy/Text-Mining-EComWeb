import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoadingService {
  private loadingSubject = new BehaviorSubject<boolean>(false);

  // Observable for the loading component to listen to
  loading$ = this.loadingSubject.asObservable();

  startLoading(): void {
    this.loadingSubject.next(true);
  }

  stopLoading(): void {
    this.loadingSubject.next(false);
  }
}
