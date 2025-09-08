
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Product } from '../../core/models/product';
@Component({ selector: 'app-product-card', template: `
<div class="card h-100">
  <img *ngIf="product.imageUrl" [src]="product.imageUrl" class="card-img-top" alt="{{product.name}}">
  <div class="card-body d-flex flex-column">
    <h5 class="card-title">{{product.name}}</h5>
    <p class="card-text small text-muted">{{product.description}}</p>
    <div class="mt-auto d-flex justify-content-between align-items-center">
      <span class="fw-bold">₹ {{product.price | number:'1.2-2'}}</span>
      <button class="btn btn-sm btn-success" (click)="add.emit()">Add</button>
    </div>
  </div>
</div>
<style>

.card {
  border: none;
  border-radius: 12px;
  overflow: hidden;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  transition: transform 0.2s ease, box-shadow 0.2s ease;
}


.card:hover {
  transform: translateY(-5px);
  box-shadow: 0 6px 18px rgba(0, 0, 0, 0.15);
}


.card-img-top {
  height: 200px;
  object-fit: cover;
  border-bottom: 1px solid #eee;
}


.card-body {
  display: flex;
  flex-direction: column;
  padding: 1rem;
}


.card-title {
  font-size: 1.1rem;
  font-weight: 600;
  color: #2a7a2e;
  margin-bottom: 0.5rem;
}


.card-text {
  font-size: 0.9rem;
  color: #555;
}


.mt-auto {
  margin-top: auto;
}


.fw-bold {
  font-size: 1rem;
  color: #000;
}

.btn-success {
  background-color: #2a7a2e;
  border: none;
  border-radius: 6px;
  padding: 0.4rem 0.8rem;
  font-weight: 500;
  transition: background 0.3s ease;
}

.btn-success:hover {
  background-color: #1e5721;
}

</style>` })
export class ProductCardComponent { @Input() product!: Product; @Output() add = new EventEmitter<void>(); }
