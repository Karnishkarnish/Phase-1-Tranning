import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ApiService } from '../../core/services/api.service';
import { Order } from '../../core/models/order';

@Component({
  template: `
    <h2>Order #{{order?.id}} - {{order?.status}}</h2>
    <div *ngIf="order">
      <p>Tracking Code: {{order.trackingCode || 'N/A'}}</p>
      <h5>Items</h5>
      <ul>
        <li *ngFor="let i of order?.orderItems">
          {{i.name}} x {{i.quantity}} (₹ {{i.price}})
        </li>
      </ul>
    </div>

    <style>
      h2 {
        text-align: center;
        margin: 1.5rem 0;
        font-weight: bold;
        color: #2a7a2e;
      }

      p {
        font-size: 1rem;
        color: #555;
        text-align: center;
        margin-bottom: 1.5rem;
      }

      h5 {
        margin-top: 1rem;
        font-weight: 600;
        color: #2a7a2e;
        border-bottom: 2px solid #eaeaea;
        padding-bottom: 0.5rem;
      }

      ul {
        list-style: none;
        padding: 0;
        margin-top: 1rem;
      }

      ul li {
        background: #fff;
        border-radius: 8px;
        padding: 0.8rem 1rem;
        margin-bottom: 0.6rem;
        box-shadow: 0 2px 6px rgba(0, 0, 0, 0.08);
        font-size: 0.95rem;
        color: #333;
        display: flex;
        justify-content: space-between;
        align-items: center;
      }

      ul li span {
        font-weight: 500;
        color: #2a7a2e;
      }
    </style>
  `
})
export class TrackOrderComponent implements OnInit {
  order?: Order;

  constructor(private route: ActivatedRoute, private api: ApiService) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.api.getOrder(id).subscribe((res: any) => {
      this.order = res?.data; 
    });
  }
}
