import { Injectable } from '@angular/core';
import { Api, GetByLoginTokenModel, GetAccessByRefreshRequestTokenModel } from 'src/myApi'
import { environment } from 'src/environment';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private api = new Api({ baseUrl: environment.apiUrl });
  private tokenExpirationTimer: any;
  private currentUserSubject = new BehaviorSubject<GetByLoginTokenModel | null>(null);
  public currentUser = this.currentUserSubject.asObservable();

  constructor(private router: Router) {}

  login(email: string, password: string): Observable<GetByLoginTokenModel> {
    const loginData = { clientId: email, password };
    return new Observable<GetByLoginTokenModel>((observer) => {
      this.api.token.tokenCreate(loginData, { includeRefreshToken: true }).then(
        (response) => {
          this.handleAuthentication(response.data.access, response.data.refresh);
          observer.next(response.data);
          observer.complete();
        },
        (error: any) => {
          observer.error(error);
        }
      );
    });
  }

  private handleAuthentication(accessToken: any, refreshToken: any): void {
    const expiresIn = new Date(accessToken.expire).getTime() - new Date().getTime();
    localStorage.setItem('accessToken', accessToken.value);
    localStorage.setItem('refreshToken', refreshToken.value);
    localStorage.setItem('tokenExpirationDate', accessToken.expire);

    this.autoLogout(expiresIn);
  }

  autoLogin(): boolean {
    const accessToken = localStorage.getItem('accessToken');
    const tokenExpirationDate = localStorage.getItem('tokenExpirationDate');

    if (!accessToken || !tokenExpirationDate) {
      return false;
    }

    const expirationDate = new Date(tokenExpirationDate);
    if (expirationDate <= new Date()) {
      this.refreshToken();
      return false;
    }

    const expiresIn = expirationDate.getTime() - new Date().getTime();
    this.autoLogout(expiresIn);
    return true;
  }

  refreshToken(): void {
    const refreshToken = localStorage.getItem('refreshToken');
    if (!refreshToken) {
      this.logout();
      return;
    }

    const requestData: GetAccessByRefreshRequestTokenModel = {
      refreshToken: refreshToken,
    };

    this.api.token.refreshCreate(requestData).then(
      (response) => {
        this.handleAuthentication(response, { value: refreshToken, expire: response.data.expire });
      },
      (error: any) => {
        this.logout();
      }
    );
  }

  logout(): void {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('tokenExpirationDate');
    if (this.tokenExpirationTimer) {
      clearTimeout(this.tokenExpirationTimer);
    }
    this.router.navigate(['/login']);
  }

  autoLogout(expirationDuration: number): void {
    this.tokenExpirationTimer = setTimeout(() => {
      this.refreshToken();
    }, expirationDuration);
  }

  getToken(): string | null {
    return localStorage.getItem('accessToken');
  }


  isAuthenticated(): boolean {
    const token = this.getToken();
    const expirationDate = localStorage.getItem('tokenExpirationDate');

    if (!token || !expirationDate) {
      return false;
    }

    const now = new Date().getTime();
    const tokenExpiration = new Date(expirationDate).getTime();

    if (tokenExpiration <= now) {
      return false;
    }

    // Token is valid
    return true;
  }
}
