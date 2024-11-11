import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Product } from '../Model/product-model'; // Import Product interface
import { Api } from 'src/myApi';

import { LoadingService } from '../service/loading.service';
@Component({
  selector: 'app-category-page',
  templateUrl: './category-page.component.html',
  styleUrls: ['./category-page.component.css']
})
export class CategoryPageComponent implements OnInit {

  private api = new Api();
  brandArray: any;
  categoryName: string = 'All';
  productCache: any;
  products: any; // The visible products for the current page
  filteredProducts: any; // The full filtered products list
  currentPage: number = 1;  // Pagination control
  totalPages: number = 1;  // Total pages
  itemsPerPage: number = 12; // Number of products per page
  selectedBrandId: number | null = null;  // Keep track of selected brand ID

  // Define filter options including price range and brand selection
  filterOptions = {
    priceRange: [50, 200] as [number, number]
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router,  // Inject Router service
    private loadingService: LoadingService
  ) {
    
  }

  async ngOnInit(): Promise<void> {
    this.loadingService.startLoading();
    // Check if products are already stored in localStorage
    const storedProducts = localStorage.getItem('productCache');
    const storedBrands = localStorage.getItem('brandArray');
  
    if (storedProducts && storedBrands) {
      // Parse and use the stored data if available
      this.productCache = JSON.parse(storedProducts);
      this.brandArray = JSON.parse(storedBrands);
      console.log('Loaded products and brands from localStorage');
      this.filterProducts(); // Initial filter and pagination setup
    } else {
      // Fetch brand list from the API and store it in localStorage
      await this.api.api.brandsList({ size: 10, page: 1 })
        .then((res) => res.json())
        .then((fulfilled) => {
          this.brandArray = fulfilled;
          localStorage.setItem('brandArray', JSON.stringify(fulfilled));
          console.log('Brand list fetched and stored in localStorage');
        })
        .catch((error) => {
          console.error('Error fetching brand list:', error);
        });
  
      // Fetch products from the API and store them in localStorage
      await this.api.api.productsPublicList()
        .then((res) => res.json())
        .then((fulfilled) => {
          this.productCache = fulfilled;
          localStorage.setItem('productCache', JSON.stringify(fulfilled));
          console.log('Product list fetched and stored in localStorage');
          this.filterProducts(); // Initial filter and pagination setup
        })
        .catch((error) => {
          console.error('Error fetching products:', error);
        });
    }
  
    // Subscribe to route parameters to capture the 'selected' parameter
    this.route.paramMap.subscribe(params => {
      const selectedBrand = params.get('selected');
      this.selectedBrandId = selectedBrand ? parseInt(selectedBrand, 10) : null; // Update selected brand ID based on URL
      this.filterProducts();
    });

    this.loadingService.stopLoading();
  }
  

  // Filter products based on selected brand and price range
  filterProducts(): void {
    this.filteredProducts = this.productCache;

    // Filter by brand if a specific one is selected
    if (this.selectedBrandId !== null) {
      this.filteredProducts = this.filteredProducts.filter((product: { brandId: number | null; }) =>
        product.brandId === this.selectedBrandId
      );
      const selectedBrand = this.brandArray.find((brand: { id: number | null; }) => brand.id === this.selectedBrandId);
      this.categoryName = selectedBrand ? selectedBrand.brandName : 'All';
    }

    // Apply the price range filter to the filteredProducts
    this.filteredProducts = this.filteredProducts.filter((product: { productPriceHistories: string | any[]; }) =>
      product.productPriceHistories.length > 0 && 
      product.productPriceHistories[0].price >= this.filterOptions.priceRange[0] &&
      product.productPriceHistories[0].price <= this.filterOptions.priceRange[1]
    );

    // Calculate total pages
    this.totalPages = Math.ceil(this.filteredProducts.length / this.itemsPerPage);

    // Set products for the current page
    this.updateVisibleProducts();
  }

  // Update the visible products for the current page
  updateVisibleProducts(): void {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.products = this.filteredProducts.slice(startIndex, endIndex);
  }

  getPaginationArray(): number[] {
    const paginationArray: number[] = [];
    if (this.totalPages <= 5) {
      // Show all pages if total pages are 5 or less
      for (let i = 1; i <= this.totalPages; i++) {
        paginationArray.push(i);
      }
    } else {
      // Always show the first page
      paginationArray.push(1);

      if (this.currentPage > 3) {
        // Add an ellipsis if current page is beyond the third page
        paginationArray.push(-1); // Placeholder for "..."
      }

      // Add up to 2 pages before and after the current page
      for (let i = Math.max(2, this.currentPage - 1); i <= Math.min(this.totalPages - 1, this.currentPage + 1); i++) {
        paginationArray.push(i);
      }

      if (this.currentPage < this.totalPages - 2) {
        // Add an ellipsis if current page is far from the last page
        paginationArray.push(-1); // Placeholder for "..."
      }

      // Always show the last page
      paginationArray.push(this.totalPages);
    }
    return paginationArray;
  }

  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages && page !== this.currentPage) {
      this.currentPage = page;
      this.updateVisibleProducts();
    }
  }

  goToPreviousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.updateVisibleProducts();
    }
  }

  goToNextPage(): void {
    if (this.currentPage < this.totalPages) {
      this.currentPage++;
      this.updateVisibleProducts();
    }
  }

  selectBrand(brandId: number): void {
    this.selectedBrandId = brandId;
    this.filterProducts();  // Filter and update page after selecting a brand
  }

  applyFilter(priceRange: [number, number]): void {
    this.router.navigate(['/category'], {
      queryParams: {
        selected: this.selectedBrandId
      }
    });
    this.ngOnInit(); // Apply filtering after updating the URL
  }

  navigateToProduct(productAsin: string): void {
    this.router.navigate(['/product', productAsin]); // Ensure `productId` is defined and not null.

  }
}
