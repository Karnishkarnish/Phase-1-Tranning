import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { Observable, map } from 'rxjs';
import { ApiResponse, Product } from '../models/product';
import { Store } from '../models/store';
import { Order } from '../models/order';
import { AuthService } from './auth.service';

@Injectable({ providedIn: 'root' })
export class ApiService {
  constructor(private http: HttpClient, private auth: AuthService) {}

  register(payload: any) { 
    return this.http.post(`${environment.apiUrl}/auth/register`, payload); 
  }

  login(payload: any) { 
    return this.http.post(`${environment.apiUrl}/auth/login`, payload); 
  }


  getProducts(opts?: { category?: string; search?: string }): Observable<Product[]> {
    let req$: Observable<Product[] | ApiResponse>;

    if (opts?.search) {
      req$ = this.http.get<Product[] | ApiResponse>(
        `${environment.apiUrl}/products/search/${encodeURIComponent(opts.search)}`
      );
    } else if (opts?.category) {
      req$ = this.http.get<Product[] | ApiResponse>(
        `${environment.apiUrl}/products/category/${encodeURIComponent(opts.category)}`
      );
    } else {
      req$ = this.http.get<Product[] | ApiResponse>(`${environment.apiUrl}/products`);
    }


    return req$.pipe(
      map(res => {
        if (Array.isArray(res)) return res;
        if (res?.data) return res.data;
        if (res?.items) return res.items;
        return [];
      })
    );
  }

  getProduct(id: number) { 
    return this.http.get<Product>(`${environment.apiUrl}/products/${id}`); 
  }

  createProduct(payload: Partial<Product>) { 
    const storeId = this.auth.currentUser?.storeId ?? this.auth.currentUser?.id; 
    return this.http.post(`${environment.apiUrl}/stores/${storeId}/products`, payload); 
  }

updateProduct(product: Product) {
  return this.http.put<Product>(`${environment.apiUrl}/products/${product.id}`, {
    id: product.id,
    name: product.name,
    price: product.price,
    description: product.description,
    stockQuantity: product.stockQuantity,
    category: product.category,
    imageUrl: product.imageUrl,
    isAvailable: product.isAvailable,
    storeId: product.storeId,
    createdAt: product['createdAt'] ?? new Date(),  // 👈 ensure not null
    updatedAt: new Date()                          // 👈 optional but good
  });
}


  deleteProduct(id: number) { 
    return this.http.delete(`${environment.apiUrl}/products/${id}`); 
  }

  getStores(): Observable<Store[]> { 
    return this.http.get<Store[]>(`${environment.apiUrl}/stores`); 
  }

  getStore(id: number) { 
    return this.http.get<Store>(`${environment.apiUrl}/stores/${id}`); 
  }
  

  placeOrder(payload: any) { 
    return this.http.post<Order>(`${environment.apiUrl}/orders`, payload); 
  }

  getOrder(id: number) { 
    return this.http.get<Order>(`${environment.apiUrl}/orders/${id}`); 
  }

  getCustomerOrders(customerId?: number): Observable<Order[]> { 
    const id = customerId ?? this.auth.currentUser?.id!; 
    return this.http.get<Order[]>(`${environment.apiUrl}/orders/customer/${id}`); 
  }
getStoreProducts(storeId: number) {
  return this.http.get<Product[]>(`${environment.apiUrl}/stores/${storeId}/orders`);
}

getStoreOrders(storeId: number) {
  return this.http.get<{ data: Order[] }>(`${environment.apiUrl}/orders/store/${storeId}`);
}


  updateOrderStatus(orderId: number, status: string) { 
    return this.http.put(`${environment.apiUrl}/orders/${orderId}/status`, { status }); 
  }

  getUsers() { 
    return this.http.get(`${environment.apiUrl}/admin/users`); 
  }

  getAdminStores() { 
    return this.http.get(`${environment.apiUrl}/admin/stores`); 
  }

  getAdminOrders() { 
    return this.http.get(`${environment.apiUrl}/admin/orders`); 
  }

  getAdminDashboard() { 
    return this.http.get(`${environment.apiUrl}/admin/dashboard`); 
  }

  salesReport(params: { from?: string; to?: string; groupBy?: string }) { 
    return this.http.get(`${environment.apiUrl}/reports/sales`, { params: params as any }); 
  }

  topProducts(limit: number = 10) { 
    return this.http.get(`${environment.apiUrl}/reports/top-products`, { params: { limit } as any }); 
  }
}
