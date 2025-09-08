
import { Injectable } from '@angular/core';
import { Product } from '../models/product';
export interface CartItem { product: Product; quantity: number; }
@Injectable({ providedIn: 'root' })
export class CartService {
  private key = 'organic_cart';
  private items: CartItem[] = this.load();
  private load(): CartItem[] { try { const raw = localStorage.getItem(this.key); return raw ? JSON.parse(raw) as CartItem[] : []; } catch { return []; } }
  private persist() { localStorage.setItem(this.key, JSON.stringify(this.items)); }
  getItems() { return [...this.items]; }
  add(product: Product, quantity: number = 1) { const idx = this.items.findIndex(i => i.product.id === product.id); if (idx >= 0) this.items[idx].quantity += quantity; else this.items.push({ product, quantity }); this.persist(); }
  remove(productId: number) { this.items = this.items.filter(i => i.product.id !== productId); this.persist(); }
  clear() { this.items = []; this.persist(); }
  get total() { return this.items.reduce((s, i) => s + i.product.price * i.quantity, 0); }
}
