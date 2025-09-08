import { Store } from "./store";

export interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  stockQuantity: number;
  category: string;
  imageUrl: string;
  isAvailable: boolean;
  createdAt: Date;
  storeId: number;
  store?: Store;
}
export interface ApiResponse {
  data?: Product[];
  items?: Product[];
}