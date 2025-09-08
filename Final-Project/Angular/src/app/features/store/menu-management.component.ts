import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { ApiService } from '../../core/services/api.service';
import { Product } from '../../core/models/product';

@Component({
  template: `
    <h2>Menu Management</h2>

    <!-- Product Form -->
    <form [formGroup]="form" (ngSubmit)="save()" class="row g-2 mb-4">
      <div class="col-md-3">
        <input class="form-control" placeholder="Name" formControlName="name">
      </div>

      <div class="col-md-3">
        <input class="form-control" placeholder="Price" type="number" formControlName="price">
      </div>

      <div class="col-md-3">
        <input class="form-control" placeholder="Category" formControlName="category">
      </div>

      <div class="col-md-3">
        <input class="form-control" placeholder="Stock Quantity" type="number" formControlName="stockQuantity">
      </div>

      <div class="col-md-6 mt-2">
        <input class="form-control" placeholder="Image URL" formControlName="imageUrl">
      </div>

      <div class="col-md-6 mt-2">
        <input class="form-control" placeholder="Description" formControlName="description">
      </div>

      <div class="col-md-12 mt-3">
        <button class="btn btn-primary w-100" [disabled]="form.invalid">Add / Update</button>
      </div>
    </form>

    <!-- Products Table -->
    <table class="table">
      <thead>
        <tr><th>Name</th><th>Price</th><th>Category</th><th>Stock</th><th></th></tr>
      </thead>
      <tbody>
        <tr *ngFor="let p of products">
          <td>{{p.name}}</td>
          <td>₹ {{p.price}}</td>
          <td>{{p.category}}</td>
          <td>{{p.stockQuantity}}</td>
          <td>
            <button class="btn btn-sm btn-outline-secondary me-2" (click)="edit(p)">Edit</button>
            <button class="btn btn-sm btn-outline-danger" (click)="remove(p)">Delete</button>
          </td>
        </tr>
      </tbody>
    </table>
  `
})
export class MenuManagementComponent implements OnInit {
  products: Product[] = [];

  form = this.fb.group({
    id: [0 as number | null],
    name: ['', Validators.required],
    description: ['', Validators.required],
    price: [0, Validators.required],
    stockQuantity: [1, Validators.required],
    category: ['', Validators.required],
    imageUrl: ['', Validators.required],
    isAvailable: [true]
  });

  constructor(
    private api: ApiService,
    private fb: FormBuilder
  ) {}

  ngOnInit() {
    this.reload();
  }

  reload() {
    this.api.getProducts().subscribe(res => this.products = res);
  }

  edit(p: Product) {
    this.form.patchValue(p);
  }

  save() {
    const v = this.form.value;

    const product: Product = {
      id: v.id ?? 0,
      name: v.name!,
      description: v.description || 'N/A',
      price: v.price!,
      stockQuantity: v.stockQuantity ?? 1,
      category: v.category || '',
      imageUrl: v.imageUrl || 'default.png',
      isAvailable: v.isAvailable ?? true,
       createdAt: new Date(), 
       storeId: 0 
    };

    if (v.id) {
      this.api.updateProduct(product).subscribe(() => this.reload());
    } else {
      this.api.createProduct({ ...product, createdAt: new Date() } as Product)
        .subscribe(() => this.reload());
    }

    this.form.reset({
      id: null,
      name: '',
      description: '',
      price: 0,
      stockQuantity: 1,
      category: '',
      imageUrl: '',
      isAvailable: true
    });
  }

  remove(p: Product) {
    this.api.deleteProduct(p.id).subscribe(() => this.reload());
  }
}
