import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../core/services/api.service';
import { CartService } from '../../core/services/cart.service';
import { Product } from '../../core/models/product';
import { FormBuilder } from '@angular/forms';

interface StoreGroup {
  storeId: number;
  products: Product[];
}

@Component({
  template: `
    <h2>Shop Organic Products</h2>

   
    <form [formGroup]="filters" class="row g-2 mb-3">
      <div class="col-md-4">
        <input type="text" class="form-control" placeholder="Search..." formControlName="search" />
      </div>
      <div class="col-md-3">
        <select class="form-select" formControlName="category">
          <option value="">All</option>
          <option>Vegetables</option>
          <option>Dairy</option>
          <option>Grains</option>
        </select>
      </div>
      <div class="col-md-2">
        <button class="btn btn-outline-secondary w-100" type="button" (click)="load()">Filter</button>
      </div>
    </form>

   
    <div *ngFor="let store of storeGroups" class="mb-4">
      <h4 class="border-bottom pb-2">Store ID: {{ store.storeId }}</h4>
      <div class="row g-3">
        <div class="col-md-3" *ngFor="let p of store.products">
          <app-product-card [product]="p" (add)="addToCart(p)"></app-product-card>
        </div>
      </div>
    </div>
    <style>
h2 {
  text-align: center;
  margin: 2rem 0 1.5rem;
  font-weight: bold;
  color: #2a7a2e;
}

form {
  background: #fff;
  padding: 1rem;
  border-radius: 12px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  margin-bottom: 2rem;
}


.form-control {
  border-radius: 8px;
  padding: 0.6rem;
  border: 1px solid #ccc;
  transition: border-color 0.3s ease, box-shadow 0.3s ease;
}

.form-control:focus {
  border-color: #2a7a2e;
  box-shadow: 0 0 6px rgba(42, 122, 46, 0.3);
}


.form-select {
  border-radius: 8px;
  padding: 0.6rem;
  border: 1px solid #ccc;
  transition: border-color 0.3s ease, box-shadow 0.3s ease;
}

.form-select:focus {
  border-color: #2a7a2e;
  box-shadow: 0 0 6px rgba(42, 122, 46, 0.3);
}


.btn-outline-secondary {
  border-radius: 8px;
  font-weight: 500;
  transition: all 0.3s ease;
}

.btn-outline-secondary:hover {
  background-color: #2a7a2e;
  color: #fff;
  border-color: #2a7a2e;
}


h4 {
  margin: 1.5rem 0 1rem;
  padding-bottom: 0.5rem;
  border-bottom: 2px solid #2a7a2e;
  color: #2a7a2e;
  font-weight: 600;
}

.row.g-3 {
  margin-top: 0.5rem;
}
</style>
  `
})
export class ShopComponent implements OnInit {
  products: Product[] = [];
  storeGroups: StoreGroup[] = [];
  filters = this.fb.group({ search: [''], category: [''] });

  constructor(
    private api: ApiService,
    private cart: CartService,
    private fb: FormBuilder
  ) {}

  ngOnInit() {
    this.load();
  }

  load() {
    const v = this.filters.value;
    this.api.getProducts({ search: v.search || '', category: v.category || '' })
      .subscribe(products => {
        this.products = products;  

        
        const groups: { [key: number]: StoreGroup } = {};
        for (let p of products) {
          if (!groups[p.storeId]) {
            groups[p.storeId] = {
              storeId: p.storeId,
              products: []
            };
          }
          groups[p.storeId].products.push(p);
        }

        this.storeGroups = Object.values(groups);
      });
  }

  addToCart(p: Product) {
    this.cart.add(p);
  }
}
