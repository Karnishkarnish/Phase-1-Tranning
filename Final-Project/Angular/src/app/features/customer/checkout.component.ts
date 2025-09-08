import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CartService } from '../../core/services/cart.service';
import { ApiService } from '../../core/services/api.service';
import { AuthService } from 'src/app/core/services/auth.service';

@Component({
  template: `
    <div class="checkout-form">
      <h2>Checkout</h2>

      <div class="mb-3">
        <label>Delivery Address</label>
        <input 
          class="form-control" 
          [(ngModel)]="deliveryAddress" 
          placeholder="Enter delivery address"
          required>
      </div>

      <div class="mb-3">
        <label>Notes</label>
        <textarea 
          class="form-control" 
          [(ngModel)]="notes" 
          placeholder="Any special instructions?"></textarea>
      </div>

      <button 
        class="btn btn-primary w-100" 
        [disabled]="!deliveryAddress" 
        (click)="checkout()">
        Place Order
      </button>
    </div>

    <style>
    /* Checkout container */
    .checkout-form {
      background: rgba(255, 255, 255, 0.95);
      padding: 2rem;
      margin: 2rem auto;
      border-radius: 12px;
      box-shadow: 0 4px 20px rgba(0, 0, 0, 0.15);
      max-width: 500px;
      width: 100%;
    }

    /* Heading */
    .checkout-form h2 {
      text-align: center;
      margin-bottom: 1.5rem;
      font-weight: bold;
      color: #2a7a2e; /* Organic green */
    }

    /* Labels */
    .checkout-form label {
      font-weight: 500;
      color: #333;
      margin-bottom: 0.3rem;
      display: block;
    }

    /* Inputs & Textarea */
    .checkout-form .form-control {
      border-radius: 8px;
      padding: 0.7rem;
      border: 1px solid #ccc;
      transition: border-color 0.3s ease, box-shadow 0.3s ease;
    }

    .checkout-form .form-control:focus {
      border-color: #2a7a2e;
      box-shadow: 0 0 6px rgba(42, 122, 46, 0.3);
    }

    /* Button */
    .checkout-form .btn-primary {
      border-radius: 8px;
      padding: 0.8rem;
      font-weight: 500;
      background-color: #2a7a2e;
      border: none;
      transition: background 0.3s ease;
    }

    .checkout-form .btn-primary:hover {
      background-color: #1e5721;
    }
    </style>
  `
})
export class CheckoutComponent {
  deliveryAddress: string = '';
  notes: string = '';

  get items() {
    return this.cart.getItems();
  }

  constructor(
    private cart: CartService,
    private api: ApiService,
    private router: Router,
    private auth: AuthService
  ) {}

checkout() {
  const user = this.auth.currentUser;

  if (!user || !user.id || user.id === 0) {
    alert("⚠️ User is not logged in or ID is invalid.");
    console.error("Invalid user object:", user);
    return;
  }

  if (!this.items.length) {
    alert("⚠️ Cart is empty.");
    return;
  }

  const payload = {
    customerId: user.id,
    storeId: this.items[0]?.product.storeId,
    deliveryAddress: this.deliveryAddress,
    notes: this.notes,
    items: this.items.map(i => ({
      productId: i.product.id,
      quantity: i.quantity
    }))
  };

  console.log("🛒 Checkout payload:", payload);

  this.api.placeOrder(payload).subscribe({
    next: (res: any) => {
      console.log("✅ Order response:", res);
      this.cart.clear();

      const orderId = res?.data?.id; // ✅ correct path

      if (orderId) {
        this.router.navigate(['/shop/track', orderId]);
      } else {
        console.warn("⚠️ Order response missing ID:", res);
        alert("Order placed successfully, but no tracking ID found.");
        this.router.navigate(['/shop']);
      }
    },
    error: (err) => {
      console.error("❌ Order failed", err);
      alert("Order failed: " + (err.error?.message || "Unknown error"));
    }
  });
}

}
