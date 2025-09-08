
import { Component } from '@angular/core';
import { NavbarComponent } from './core/components/navbar/navbar.component';
@Component({
  selector: 'app-root',
  template: `<app-navbar></app-navbar><main class="container py-4"><router-outlet></router-outlet></main>`,
  
})
export class AppComponent {}
