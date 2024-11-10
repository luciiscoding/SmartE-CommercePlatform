import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {
  ADD_EDIT_PRODUCT_MODAL_CONFIG,
  AddEditProductModalComponent,
  Product,
} from '@app/features';
import { ProductService } from '@app/features/services';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'smart-ecommerce-platform-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent {
  constructor(
    private _productService: ProductService,
    private _dialog: MatDialog,
    private _toastrService: ToastrService
  ) {}

  products: Product[] = [];

  ngOnInit() {
    this.getProducts();
  }

  onProductUpdated(): void {
    this.getProducts();
  }

  openAddProductModal(): void {
    this._dialog
      .open(AddEditProductModalComponent, {
        ...ADD_EDIT_PRODUCT_MODAL_CONFIG,
        data: { product: null, modalTitle: 'Add Product' },
      })
      .afterClosed()
      .subscribe((data) => {
        if (data) {
          this._productService.createProduct(data).subscribe({
            next: () => {
              this.getProducts();
              this._toastrService.success('Product added successfully');
            },
            error: () => {
              this._toastrService.error('Error occurred while adding product');
            },
          });
        }
      });
  }

  private getProducts(): void {
    this._productService.getProducts().subscribe((products) => {
      this.products = products;
    });
  }
}
