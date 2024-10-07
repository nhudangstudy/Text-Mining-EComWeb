export interface Product {
    productASIN: string | null;
    name: string;
    price: number;
    rating: number;
    imageUrl: string;
    oldPrice?: number | null;
    discount?: number | null;
  }
  