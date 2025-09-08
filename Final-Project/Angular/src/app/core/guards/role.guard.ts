import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, Router, UrlTree } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { Role } from '../models/user';

@Injectable({ providedIn: 'root' })
export class RoleGuard implements CanActivate {
  constructor(private auth: AuthService, private router: Router) {}

  canActivate(route: ActivatedRouteSnapshot): boolean | UrlTree {
    const roles = route.data['roles'] as Role[];

  
    if (roles && this.auth.hasRole(roles)) {
      return true;
    }

    
    return this.router.parseUrl('/auth/login');
  }
}
