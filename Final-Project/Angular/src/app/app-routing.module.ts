import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './core/guards/auth.guard';
import { RoleGuard } from './core/guards/role.guard';


const routes: Routes = [
   
  { path: '', redirectTo: 'auth/login', pathMatch: 'full' },


  { 
    path: 'auth', 
    loadChildren: () => import('./features/auth/auth.module').then(m => m.AuthModule) 
  },

 
  { 
    path: 'shop',
    canActivate: [AuthGuard, RoleGuard],  
        data: { roles: ['Customer'] },
    loadChildren: () => import('./features/customer/customer.module').then(m => m.CustomerModule) 
  },

    { 
    path: 'store', 
    canActivate: [AuthGuard, RoleGuard], 
    data: { roles: ['Store'] },
    loadChildren: () => import('./features/store/store.module').then(m => m.StoreModule) 
  },

 
  { 
    path: 'admin', 
    canActivate: [AuthGuard, RoleGuard], 
    data: { roles: ['Admin'] },
    loadChildren: () => import('./features/admin/admin.module').then(m => m.AdminModule) 
  },


  { 
    path: 'reports', 
    canActivate: [AuthGuard, RoleGuard], 
    data: { roles: ['Admin', 'Store'] },
    loadChildren: () => import('./features/reports/reports.module').then(m => m.ReportsModule) 
  },

  { path: '**', redirectTo: 'auth/login' }
];

@NgModule({ 
  imports: [RouterModule.forRoot(routes)], 
  exports: [RouterModule] 
})
export class AppRoutingModule {}
