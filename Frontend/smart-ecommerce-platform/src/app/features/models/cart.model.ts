import { Product } from './product.model';

export interface Cart {
  id?: string;
  products: Product[];
}
