export interface Product {
    productASIN: string;
    name: string;
    price: number;
    rating: number;
    imageUrl: string;
    oldPrice?: number | null;
    discount?: number | null;
    lifestyle: string;
  }
  