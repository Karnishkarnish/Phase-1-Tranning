
import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../core/services/api.service';
import { Order } from '../../core/models/order';
@Component({
  template: `
    <h2>My Orders</h2>
    <table class="table">
      <thead><tr><th>#</th><th>Status</th><th>Total</th><th>Placed</th><th></th></tr></thead>
      <tbody>
        <tr *ngFor="let o of orders">
          <td>{{o.id}}</td><td>{{o.status}}</td><td>₹ {{o.totalAmount}}</td><td>{{o.createdAt | date:'short'}}</td>
          <td><a [routerLink]="['/shop/track', o.id]">Track</a></td>
        </tr>
      </tbody>
    </table>
    <style>
h2 {
  text-align: center;
  margin-bottom: 1.5rem;
  font-weight: bold;
  color: #2a7a2e;
}


.table {
  background: #fff;
  border-radius: 12px;
  overflow: hidden;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  width: 100%;
  margin: auto;
  max-width: 900px;
}


.table thead th {
  background: #2a7a2e;
  color: #fff;
  font-weight: 500;
  text-align: center;
  padding: 0.8rem;
}


.table td {
  text-align: center;
  vertical-align: middle;
  padding: 0.8rem;
  color: #333;
}


.table tbody tr:nth-child(even) {
  background-color: #f8f9fa;
}


.table a {
  color: #2a7a2e;
  font-weight: 500;
  text-decoration: none;
}

.table a:hover {
  text-decoration: underline;
}
</style>`
})
export class MyOrdersComponent implements OnInit {
  orders: Order[] = [];
  constructor(private api: ApiService) {}
  ngOnInit() { this.api.getCustomerOrders().subscribe(res => this.orders = res); }
}
