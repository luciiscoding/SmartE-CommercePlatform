import { HomeComponent } from './home.component';
import { MatDialog } from '@angular/material/dialog';
import { ProductService } from '@app/features/services';
import { ToastrService } from 'ngx-toastr';
import { ErrorHandlerService } from '@app/shared';
import { of, throwError, EMPTY } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';

describe('HomeComponent', () => {
  let component: HomeComponent;
  let mockProductService: Partial<ProductService>;
  let mockDialog: Partial<MatDialog>;
  let mockToastrService: Partial<ToastrService>;
  let mockErrorHandlerService: Partial<ErrorHandlerService>;

  beforeEach(() => {
    mockProductService = {
      getProducts: jest.fn().mockReturnValue(of({ data: [], totalItems: 0 })),
      createProduct: jest.fn().mockReturnValue(of({})),
      getFilteredProducts: jest.fn().mockReturnValue(of([])),  
    };
    mockDialog = {
      open: jest.fn().mockReturnValue({
        afterClosed: jest.fn().mockReturnValue(of(true)),
      }),
    };
    mockToastrService = { success: jest.fn() };
    mockErrorHandlerService = { handleError: jest.fn() };
  
    component = new HomeComponent(
      mockProductService as ProductService,
      mockDialog as MatDialog,
      mockToastrService as ToastrService,
      mockErrorHandlerService as ErrorHandlerService
    );
  });
  

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should initialize and load products on init', () => {
    const mockProducts = [
      { id: 1, name: 'Product 1' },
      { id: 2, name: 'Product 2' },
    ];
  
    (mockProductService.getProducts as jest.Mock).mockReturnValue(of({ data: mockProducts }));
    component.ngOnInit();  
    expect(component.products).toEqual(mockProducts);  
    expect(component.paginatedProducts).toEqual(mockProducts.slice(0, 5));  
  });
  

  it('should unsubscribe on destroy', () => {
    const unsubscribeSpy = jest.spyOn(component['_subs$'], 'unsubscribe');
    component.ngOnDestroy();
    expect(unsubscribeSpy).toHaveBeenCalled();
  });

  it('should update products on product update', () => {
    const getProductsSpy = jest.spyOn(component as any, 'getProducts');
    component.onProductUpdated();
    expect(getProductsSpy).toHaveBeenCalledWith(
      component.pageNumber,
      component.pageSize
    );
  });

  it('should open add product modal and add product on confirmation', () => {
    component.openAddProductModal();
    expect(mockDialog.open).toHaveBeenCalled();
    expect(mockProductService.createProduct).toHaveBeenCalled();
    expect(mockToastrService.success).toHaveBeenCalledWith(
      'Product added successfully'
    );
  });

  it('should not add product when modal is cancelled', () => {
    (mockDialog.open as jest.Mock).mockReturnValue({
      afterClosed: jest.fn().mockReturnValue(of(false)),
    });
    component.openAddProductModal();
    expect(mockProductService.createProduct).not.toHaveBeenCalled();
  });

  it('should handle error when getting products fails', () => {
    const mockErrorResponse = new HttpErrorResponse({
      error: 'Error message',
      status: 400,
      statusText: 'Bad Request',
    });
  
    (mockProductService.getProducts as jest.Mock).mockReturnValue(throwError(() => mockErrorResponse));
    component['getProducts'](0, 5);
    expect(mockErrorHandlerService.handleError).toHaveBeenCalledWith(mockErrorResponse);
  });
  
  it('should update paginated products on page change', () => {
    component.products = Array.from(
      { length: 10 },
      (_, i) => ({
        id: i + 1,
        name: `Product ${i + 1}`,
        type: 'Type',
        description: 'Description',
        price: 100,
        review: 'Review'
      } as any)
    );
  
    const updatePaginatedProductsSpy = jest.spyOn(component as any, 'updatePaginatedProducts');
    component.onPageChange({ pageIndex: 1, pageSize: 5 });
    expect(updatePaginatedProductsSpy).toHaveBeenCalledWith(5, 10);
  });
  
  it('should update paginated products on page change', () => {
    component.products = Array.from(
      { length: 10 },
      (_, i) => ({
        id: i + 1,
        name: `Product ${i + 1}`,
        type: 'Type',
        description: 'Description',
        price: 100,
        review: 'Review'
      } as any)
    );
  
    const updatePaginatedProductsSpy = jest.spyOn(component as any, 'updatePaginatedProducts');
    component.onPageChange({ pageIndex: 1, pageSize: 5 });
    expect(updatePaginatedProductsSpy).toHaveBeenCalledWith(5, 10);
  });
  
  

  it('should handle error when getting products fails', () => {
    const mockErrorResponse = new HttpErrorResponse({
      error: 'Error message',
      status: 400,
      statusText: 'Bad Request',
    });
  
    (mockProductService.getProducts as jest.Mock).mockReturnValue(throwError(() => mockErrorResponse));
    component['getProducts'](0, 5);
    expect(mockErrorHandlerService.handleError).toHaveBeenCalledWith(mockErrorResponse);
  });
  
  it('should handle error when adding product fails', () => {
    const mockErrorResponse = new HttpErrorResponse({
      error: 'Error message',
      status: 400,
      statusText: 'Bad Request',
    });
    (mockProductService.createProduct as jest.Mock).mockReturnValue(
      throwError(() => mockErrorResponse)
    );
    component.openAddProductModal();
    expect(mockErrorHandlerService.handleError).toHaveBeenCalledWith(mockErrorResponse);
  });
  
});