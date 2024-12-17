import { HomeComponent } from './home.component';
import { MatDialog } from '@angular/material/dialog';
import { ProductService } from '@app/features/services';
import { ToastrService } from 'ngx-toastr';
import { ErrorHandlerService } from '@app/shared';
import { of, throwError } from 'rxjs';
import { HttpErrorResponse } from '@angular/common/http';
import { Product } from '@app/features/models/product.model';

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



  it('should open the add product modal and add product when confirmed', () => {
    (mockDialog.open as jest.Mock).mockReturnValue({
      afterClosed: jest.fn().mockReturnValue(of(true)),
    });
  
    component.openAddProductModal();
  
    expect(mockDialog.open).toHaveBeenCalled();
    expect(mockProductService.createProduct).toHaveBeenCalled();
    expect(mockToastrService.success).toHaveBeenCalledWith('Product added successfully');
  });

  it('should not add product when modal is cancelled', () => {
    (mockDialog.open as jest.Mock).mockReturnValue({
      afterClosed: jest.fn().mockReturnValue(of(false)),
    });
  
    component.openAddProductModal();
  
    expect(mockProductService.createProduct).not.toHaveBeenCalled();
    expect(mockToastrService.success).not.toHaveBeenCalled();
  });
    

 
});
