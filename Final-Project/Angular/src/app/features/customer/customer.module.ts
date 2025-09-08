import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';  // ✅ Import FormsModule

import { ShopComponent } from './shop.component';
import { ProductCardComponent } from './product-card.component';
import { CartComponent } from './cart.component';
import { CheckoutComponent } from './checkout.component';
import { MyOrdersComponent } from './my-orders.component';
import { TrackOrderComponent } from './track-order.component';

const routes: Routes = [
  { path: '', component: ShopComponent },
  { path: 'cart', component: CartComponent },
  { path: 'checkout', component: CheckoutComponent },
  { path: 'orders', component: MyOrdersComponent },
  { path: 'track/:id', component: TrackOrderComponent }
];

@NgModule({
  declarations: [
    ShopComponent,
    ProductCardComponent,
    CartComponent,
    CheckoutComponent,
    MyOrdersComponent,
    TrackOrderComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes)
  ]
})
export class CustomerModule {}
