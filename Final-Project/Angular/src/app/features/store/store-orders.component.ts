import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../core/services/api.service';
import { Order } from '../../core/models/order';
import { AuthService } from '../../core/services/auth.service';

@Component({
  template: `
    <h2>Incoming Orders</h2>
    <table class="table">
      <thead><tr><th>#</th><th>Status</th><th>Total</th><th>Actions</th></tr></thead>
      <tbody>
        <tr *ngFor="let o of orders">
          <td>{{o.id}}</td><td>{{o.status}}</td><td>₹ {{o.totalAmount}}</td>
          <td>
            <div class="btn-group btn-group-sm">
              <button class="btn btn-outline-primary" (click)="set(o, 'Accepted')">Accept</button>
              <button class="btn btn-outline-secondary" (click)="set(o, 'Packing')">Packing</button>
              <button class="btn btn-outline-info" (click)="set(o, 'Ready')">Ready</button>
              <button class="btn btn-outline-success" (click)="set(o, 'OutForDelivery')">Out</button>
              <button class="btn btn-outline-success" (click)="set(o, 'Delivered')">Delivered</button>
              <button class="btn btn-outline-danger" (click)="set(o, 'Cancelled')">Cancel</button>
            </div>
          </td>
        </tr>
      </tbody>
    </table>
    <style>
h2 {
  text-align: center;
  margin: 1.5rem 0;
  font-weight: bold;
  color: #2a7a2e
}


.table {
  background: #fff;
  border-radius: 12px;
  overflow: hidden;
  box-shadow: 0 4px 12px rgba(0,0,0,0.1);
  margin-top: 1rem;
}


.table thead {
  background-color: #2a7a2e;
  color: #fff;
  font-weight: 600;
  text-align: center;
}

.table th,
.table td {
  padding: 0.9rem;
  text-align: center;
  vertical-align: middle;
  font-size: 0.95rem;
}


.table tbody tr:nth-child(even) {
  background-color: #f9f9f9;
}


.table tbody tr:hover {
  background-color: #eef6ee;
  transition: background 0.2s ease;
}


.btn-group {
  display: flex;
  flex-wrap: wrap;
  gap: 0.3rem;
  justify-content: center;
}


.btn-group .btn {
  border-radius: 6px !important;
  font-size: 0.8rem;
  padding: 0.35rem 0.7rem;
  min-width: 80px;
  transition: transform 0.2s ease, background 0.3s ease;
}

.btn-group .btn:hover {
  transform: translateY(-2px);
}
</style>`
})
export class StoreOrdersComponent implements OnInit {
  orders: Order[] = [];
  constructor(private api: ApiService, private auth: AuthService) {}

  ngOnInit() {
    this.load();
  }
storeId = 1;
load() {
  this.api.getStoreOrders(this.storeId).subscribe(res => this.orders = res.data);
}

  set(o: Order, status: string) {
    this.api.updateOrderStatus(o.id, status).subscribe(() => this.load());
  }
}
