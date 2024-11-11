import { HttpErrorResponse } from '@angular/common/http';
import {
  Component,
  EventEmitter,
  Input,
  OnDestroy,
  Output,
} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import {
  ADD_EDIT_PRODUCT_MODAL_CONFIG,
  AddEditProductModalComponent,
  Product,
} from '@app/features';
import { CartService, ProductService } from '@app/features/services';
import { ErrorHandlerService, GenericWarningModalComponent } from '@app/shared';
import { ToastrService } from 'ngx-toastr';
import { EMPTY, finalize, Subscription, switchMap } from 'rxjs';

@Component({
  selector: 'smart-ecommerce-platform-product-card',
  templateUrl: './product-card.component.html',
  styleUrls: ['./product-card.component.scss'],
})
export class ProductCardComponent implements OnDestroy {
  @Input() product!: Product;
  @Output() productUpdated: EventEmitter<void> = new EventEmitter<void>();

  isLoading: boolean = false;

  private _subs$: Subscription = new Subscription();

  constructor(
    private _dialog: MatDialog,
    private _productService: ProductService,
    private _toastrService: ToastrService,
    private _cartService: CartService,
    private _errorHandlerService: ErrorHandlerService
  ) {}

  ngOnDestroy(): void {
    this._subs$.unsubscribe();
  }

  openDeleteDialog(): void {
    this._subs$.add(
      this._dialog
        .open(GenericWarningModalComponent, {
          data: {
            title: 'Delete Product',
            message: 'Are you sure you want to delete this product?',
            noButtonText: 'No',
            proceedButtonText: 'Proceed',
          },
        })
        .afterClosed()
        .pipe(
          switchMap((response) => {
            if (!response) return EMPTY;
            this.isLoading = true;

            return this._productService.deleteProduct(this.product.id!).pipe(
              finalize(() => {
                this.isLoading = false;
              })
            );
          })
        )
        .subscribe({
          next: () => {
            this.productUpdated.emit();
            this._toastrService.success('Product deleted successfully');
          },
          error: (error: HttpErrorResponse) => {
            this._errorHandlerService.handleError(error);
          },
        })
    );
  }

  openEditDialog(): void {
    this._subs$.add(
      this._dialog
        .open(AddEditProductModalComponent, {
          ...ADD_EDIT_PRODUCT_MODAL_CONFIG,
          data: { product: this.product, modalTitle: 'Edit Product' },
        })
        .afterClosed()
        .pipe(
          switchMap((updatedProduct: Product | undefined) => {
            if (!updatedProduct) return EMPTY;

            this.isLoading = true;
            updatedProduct.id = this.product.id;

            return this._productService.updateProduct(updatedProduct).pipe(
              finalize(() => {
                this.isLoading = false;
              })
            );
          })
        )
        .subscribe({
          next: () => {
            this.productUpdated.emit();
            this._toastrService.success('Product updated successfully');
          },
          error: (error: HttpErrorResponse) => {
            this._errorHandlerService.handleError(error);
          },
        })
    );
  }

  addToCart(productId: string): void {
    this.isLoading = true;
    this._subs$.add(
      this._cartService
        .addToCart(productId)
        .pipe(
          finalize(() => {
            this.isLoading = false;
          })
        )
        .subscribe({
          next: () => {
            this._toastrService.success('Product added to cart');
            this._cartService.cartUpdated$.next();
          },
          error: (error: HttpErrorResponse) => {
            this._errorHandlerService.handleError(error);
          },
        })
    );
  }
}
