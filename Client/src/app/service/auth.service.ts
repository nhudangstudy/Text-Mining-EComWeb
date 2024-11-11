import { Injectable } from '@angular/core';
import { Api, GetByLoginTokenModel, GetAccessByRefreshRequestTokenModel } from 'src/myApi'
import { environment } from 'src/environment';
import { BehaviorSubject, Observable } from 'rxjs';
import { jwtDecode } from "jwt-decode";
import { Router } from '@angular/router';
import { MessageBoxService } from './message-box-service.service';



@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private api = new Api({ baseUrl: environment.apiUrl });
  private tokenExpirationTimer: any;
  private currentUserSubject = new BehaviorSubject<GetByLoginTokenModel | null>(null);
  public currentUser = this.currentUserSubject.asObservable();

  constructor(private router: Router, private messageBoxService: MessageBoxService) {}

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

  getDecodedToken(): any {
    const token = this.getToken();
    if (token) {
      try {
        return jwtDecode(token);
      } catch (error) {
        console.error('Failed to decode token', error);
        return null;
      }
    }
    return null;
  }
  
  hasRole(role: string): boolean {
    const decodedToken = this.getDecodedToken();
    return decodedToken?.role?.includes(role) ?? false;
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
    this.router.navigate(['/login']);

    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('tokenExpirationDate');
    if (this.tokenExpirationTimer) {
      clearTimeout(this.tokenExpirationTimer);
    }
    this.messageBoxService.success("Logout sucess!!!")
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
