import { HttpErrorResponse } from '@angular/common/http';
import {
  ChangeDetectorRef,
  Component,
  OnDestroy,
  ViewChild,
} from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
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
  @ViewChild('paginator') paginator: MatPaginator | undefined;
  isLoading: boolean = false;

  pageNumber: number = 0;
  pageSize: number = 5;
  totalCount: number = 0;
  products: Product[] = [];
  paginatedProducts: Product[] = [];
  selectedRating: number = -1;

  private _subs$: Subscription = new Subscription();

  constructor(
    private _productService: ProductService,
    private _dialog: MatDialog,
    private _toastrService: ToastrService,
    private _errorHandlerService: ErrorHandlerService
  ) {}

  ngOnInit() {
    this.onPageChange({ pageIndex: this.pageNumber, pageSize: this.pageSize });
  }

  ngOnDestroy(): void {
    this._subs$.unsubscribe();
  }

  onProductUpdated(): void {
    this.pageNumber = 0;
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

  onPageChange(event: { pageIndex: number; pageSize: number }): void {
    this.pageNumber = event.pageIndex;
    this.pageSize = event.pageSize;
    this.getProducts();
  }

  filterProducts(): void {
    this.getProducts();
  }

  private getProducts(): void {
    this.isLoading = true;
    this._subs$.add(
      this._productService
        .getProducts(this.pageNumber, this.pageSize, this.selectedRating)
        .pipe(
          finalize(() => {
            this.isLoading = false;
          })
        )
        .subscribe({
          next: (response) => {
            this.products = response.data;
            this.totalCount = response.totalItems;
            console.log(this.totalCount);
          },
          error: (error: HttpErrorResponse) => {
            this._errorHandlerService.handleError(error);
          },
        })
    );
  }
}
