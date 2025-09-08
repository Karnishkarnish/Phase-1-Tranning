
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AdminDashboardComponent } from './admin-dashboard.component';
import { UsersComponent } from './users.component';
const routes: Routes = [{ path: '', component: AdminDashboardComponent },
    { path: 'users', component: UsersComponent }];
@NgModule({ declarations: [AdminDashboardComponent, UsersComponent],
     imports: [CommonModule, RouterModule.forChild(routes)] })
export class AdminModule {}
