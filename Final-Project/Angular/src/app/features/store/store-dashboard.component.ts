import { Component } from '@angular/core';

@Component({
  template: `
    <style>
      .dashboard-container {
        max-width: 600px;
        margin: 2rem auto;
        padding: 2rem;
        background: #fff;
        border-radius: 12px;
        box-shadow: 0 4px 12px rgba(0,0,0,0.1);
        text-align: center;
      }

      h2 {
        font-weight: bold;
        margin-bottom: 1.5rem;
        color: #2a7a2e; 
      }

      ul {
        list-style: none;
        padding: 0;
        margin: 0;
      }

      ul li {
        margin: 1rem 0;
      }

      ul li a {
        display: block;
        padding: 0.8rem;
        border-radius: 8px;
        background-color: #2a7a2e;
        color: #fff;
        font-weight: 500;
        text-decoration: none;
        transition: background 0.3s ease, transform 0.2s ease;
      }

      ul li a:hover {
        background-color: #1e5721;
        transform: translateY(-2px);
      }
    </style>

    <div class="dashboard-container">
      <h2>Store Dashboard</h2>
      <ul>
        <li><a routerLink="/store/orders">Manage Orders</a></li>
        <li><a routerLink="/store/menu">Manage Menu</a></li>
      </ul>
    </div>
  `
})
export class StoreDashboardComponent {}
