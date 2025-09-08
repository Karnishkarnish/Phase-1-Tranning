
import { Component } from '@angular/core';
import { CartService } from '../../core/services/cart.service';
@Component({
  template: `
    <h2>Your Cart</h2>
    <div *ngIf="!items.length" class="alert alert-info">Cart is empty.</div>
    <table class="table" *ngIf="items.length">
      <thead><tr><th>Product</th><th>Qty</th><th>Price</th><th>Sub-total</th><th></th></tr></thead>
      <tbody>
        <tr *ngFor="let i of items">
          <td>{{i.product.name}}</td><td>{{i.quantity}}</td><td>₹ {{i.product.price}}</td><td>₹ {{i.product.price * i.quantity}}</td>
          <td><button class="btn btn-sm btn-outline-danger" (click)="remove(i.product.id)">Remove</button></td>
        </tr>
      </tbody>
    </table>
    <div class="d-flex justify-content-between" *ngIf="items.length">
      <h4>Total: ₹ {{cart.total}}</h4>
      <a class="btn btn-primary" routerLink="/shop/checkout">Checkout</a>
    </div><style>/* Page heading */
h2 {
  text-align: center;
  margin-bottom: 1.5rem;
  font-weight: bold;
  color: #2a7a2e; /* organic green */
}

/* Empty cart alert */
.alert-info {
  text-align: center;
  font-size: 1.1rem;
  border-radius: 8px;
  padding: 1rem;
}

/* Table styling */
.table {
  background: #fff;
  border-radius: 12px;
  overflow: hidden;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
}

.table th {
  background: #2a7a2e;
  color: #fff;
  font-weight: 500;
  text-align: center;
  padding: 0.8rem;
}

.table td {
  vertical-align: middle;
  text-align: center;
  padding: 0.8rem;
}

/* Remove button */
.btn-outline-danger {
  border-radius: 6px;
  padding: 0.3rem 0.8rem;
  font-size: 0.9rem;
}

.btn-outline-danger:hover {
  background-color: #dc3545;
  color: #fff;
}

/* Total + Checkout section */
.d-flex {
  margin-top: 1rem;
  align-items: center;
}

h4 {
  font-weight: bold;
  color: #333;
}

.btn-primary {
  border-radius: 8px;
  padding: 0.6rem 1.5rem;
  font-weight: 500;
  background-color: #2a7a2e;
  border: none;
}

.btn-primary:hover {
  background-color: #1e5721;
}
</style>`
})
export class CartComponent {
  get items() { return this.cart.getItems(); }
  constructor(public cart: CartService) {}
  remove(id: number) { this.cart.remove(id); }
}
