
import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { CartService } from '../../services/cart.service';

@Component({ selector: 'app-navbar', templateUrl: './navbar.component.html' })
export class NavbarComponent {
  constructor(public auth: AuthService, public cart: CartService) {}
  logout() { this.auth.logout(); }
}
