import { Component, OnInit } from '@angular/core';
import { ApiService } from '../../core/services/api.service';

@Component({
  template: `
    <div class="top-products">
      <h2>Top Products</h2>

      <table *ngIf="data?.length" class="table table-hover">
        <thead>
          <tr>
            <th>#</th>
            <th>Product</th>
            <th>Sold</th>
            <th>Total Revenue</th>
          </tr>
        </thead>
        <tbody>
          <tr *ngFor="let p of data; let i = index">
            <td>{{ i + 1 }}</td>
            <td>{{ p.name }}</td>
            <td>{{ p.quantity }}</td>
            <td>₹ {{ p.total | number:'1.2-2' }}</td>
          </tr>
        </tbody>
      </table>

      <div *ngIf="!data?.length" class="alert alert-info">
        No products found.
      </div>
    </div>
    <style>
.top-products {
  max-width: 1000px;
  margin: 2rem auto;
  padding: 1rem;
}

.top-products h2 {
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
  border-collapse: separate;
  border-spacing: 0;
}


.table thead {
  background: #2a7a2e;
  color: #fff;
}

.table th {
  padding: 0.85rem;
  text-align: center;
  font-weight: 600;
  font-size: 0.95rem;
}

.table td {
  text-align: center;
  padding: 0.75rem;
  color: #333;
  vertical-align: middle;
  font-size: 0.9rem;
}


.table tbody tr:nth-child(even) {
  background-color: #f8f9fa;
}


.table-hover tbody tr:hover {
  background-color: #eaf3ea;
  cursor: pointer;
}


.table td:first-child {
  font-weight: bold;
  color: #2a7a2e;
}


.table td:last-child {
  font-weight: 600;
  color: #1e5721;
}

.alert-info {
  text-align: center;
  font-size: 1rem;
  border-radius: 8px;
}
</style>
  `
})
export class TopProductsComponent implements OnInit {
  data: any;
  constructor(private api: ApiService) {}

  ngOnInit() {
    this.api.topProducts().subscribe(res => this.data = res);
  }
}
