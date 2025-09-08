import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import { environment } from '../../../environments/environment';
import { User, Role } from '../models/user';
import { Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private currentUserKey = 'organic_user';

  constructor(private http: HttpClient, private router: Router) {}

  login(email: string, password: string) {
    return this.http.post<{ data: string; success: boolean; message: string }>(
      `${environment.apiUrl}/auth/login`,
      { email, password }
    ).pipe(
      tap(res => {
        if (res.success && res.data) {
          const decoded = this.decodeToken(res.data);
          console.log('🔍 Decoded JWT:', decoded);

          const role =
            decoded["role"] ??
            decoded["roles"] ??
            decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] ??
            'Customer';

          // ✅ Extract correct userId
          const userId =
            decoded["nameid"] ??
            decoded["sub"] ??
            decoded["id"] ??
            decoded["userId"] ??
            decoded["customerId"] ??
            decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"];

          const userEmail =
            decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"] || email;

          // ✅ Extract storeId from multiple possible claims
          const storeId =
            decoded["storeId"] ??
            decoded["store_id"] ??
            decoded["sid"] ??
            decoded["StoreID"] ??
            decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sid"];

          const user: User = {
            id: userId ? +userId : 0,
            email: userEmail,
            name: decoded["unique_name"] || '',
            role,
            token: res.data,
            storeId: storeId ? +storeId : undefined
          };

          this.setCurrentUser(user);

          // 🔹 Redirect based on role
          switch (user.role) {
            case 'Customer':
              this.router.navigate(['/shop']);
              break;
            case 'Store':
              this.router.navigate(['/store']);
              break;
            case 'Admin':
              this.router.navigate(['/admin']);
              break;
            default:
              this.router.navigate(['/auth/login']);
          }
        }
      })
    );
  }

  register(payload: { name: string; email: string; password: string; role: Role }) {
    return this.http.post(`${environment.apiUrl}/auth/register`, payload);
  }

  logout() {
    localStorage.removeItem(this.currentUserKey);
    this.router.navigate(['/auth/login']);
  }

  get currentUser(): User | null {
    const raw = localStorage.getItem(this.currentUserKey);
    return raw ? JSON.parse(raw) as User : null;
  }

  setCurrentUser(user: User) {
    localStorage.setItem(this.currentUserKey, JSON.stringify(user));
  }

  get token(): string | null {
    return this.currentUser?.token ?? null;
  }

  hasRole(roles: Role[]): boolean {
    const role = this.currentUser?.role;
    return !!role && roles.includes(role);
  }

  isAuthenticated(): boolean {
    return !!this.token;
  }

  private decodeToken(token: string): any {
    try {
      return JSON.parse(atob(token.split('.')[1]));
    } catch {
      return {};
    }
  }
}
