import { Component } from '@angular/core';

@Component({
  template: `
    <h2>Admin Dashboard</h2>
    <ul>
      <li><a routerLink='/admin/users'>User Management</a></li>
      <li><a routerLink='/reports'>Reports</a></li>
    </ul>
  `,
  styles: [`
    h2 {
      color: #2c3e50;
      font-family: Arial, sans-serif;
      margin-bottom: 20px;
    }

    ul {
      list-style: none;
      padding: 0;
    }

    li {
      margin: 10px 0;
    }

    a {
      text-decoration: none;
      color: #2980b9;
      font-weight: bold;
    }

    a:hover {
      color: #e74c3c;
    }
  `]
})
export class AdminDashboardComponent {}
