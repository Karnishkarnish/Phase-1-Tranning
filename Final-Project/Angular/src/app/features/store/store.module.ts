
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { StoreDashboardComponent } from './store-dashboard.component';
import { MenuManagementComponent } from './menu-management.component';
import { StoreOrdersComponent } from './store-orders.component';
const routes: Routes = [{ path: '', component: StoreDashboardComponent },
    { path: 'menu', component: MenuManagementComponent },
    { path: 'orders', component: StoreOrdersComponent }];
@NgModule({ declarations: [StoreDashboardComponent, MenuManagementComponent, StoreOrdersComponent], imports: [CommonModule, ReactiveFormsModule, RouterModule.forChild(routes)] })
export class StoreModule {}
