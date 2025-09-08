
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { SalesReportComponent } from './sales-report.component';
import { TopProductsComponent } from './top-products.component';
const routes: Routes = [{ path: '', component: SalesReportComponent },{ path: 'top-products', component: TopProductsComponent }];
@NgModule({ declarations: [SalesReportComponent, TopProductsComponent], imports: [CommonModule, ReactiveFormsModule, RouterModule.forChild(routes)] })
export class ReportsModule {}
