import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnDestroy } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {
  ADD_EDIT_PRODUCT_MODAL_CONFIG,
  AddEditProductModalComponent,
  Product,
} from '@app/features';
import { ProductService } from '@app/features/services';
import { ErrorHandlerService } from '@app/shared';
import { ToastrService } from 'ngx-toastr';
import { EMPTY, finalize, Subscription, switchMap } from 'rxjs';

@Component({
  selector: 'smart-ecommerce-platform-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
})
export class HomeComponent implements OnDestroy {
  isLoading: boolean = false;

  private _subs$: Subscription = new Subscription();

  constructor(
    private _productService: ProductService,
    private _dialog: MatDialog,
    private _toastrService: ToastrService,
    private _errorHandlerService: ErrorHandlerService
  ) {}

  products: Product[] = [];

  ngOnInit() {
    this.getProducts();
  }

  ngOnDestroy(): void {
    this._subs$.unsubscribe();
  }

  onProductUpdated(): void {
    this.getProducts();
  }

  openAddProductModal(): void {
    this._subs$.add(
      this._dialog
        .open(AddEditProductModalComponent, {
          ...ADD_EDIT_PRODUCT_MODAL_CONFIG,
          data: { product: null, modalTitle: 'Add Product' },
        })
        .afterClosed()
        .pipe(
          switchMap((data) => {
            if (!data) return EMPTY;

            this.isLoading = true;

            return this._productService.createProduct(data).pipe(
              finalize(() => {
                this.isLoading = false;
              })
            );
          })
        )
        .subscribe({
          next: () => {
            this.getProducts();
            this._toastrService.success('Product added successfully');
          },
          error: (error: HttpErrorResponse) => {
            this._errorHandlerService.handleError(error);
          },
        })
    );
  }

  private getProducts(): void {
    this.isLoading = true;
    this._subs$.add(
      this._productService
        .getProducts()
        .pipe(
          finalize(() => {
            this.isLoading = false;
          })
        )
        .subscribe({
          next: (products) => {
            this.products = products;
          },
          error: (error: HttpErrorResponse) => {
            this._errorHandlerService.handleError(error);
          },
        })
    );
  }
}
