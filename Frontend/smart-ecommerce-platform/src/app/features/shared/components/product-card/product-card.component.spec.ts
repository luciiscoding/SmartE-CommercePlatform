import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ProductCardComponent } from './product-card.component';
import { MatDialog } from '@angular/material/dialog';
import { CartService } from '../../../services/cart.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ToastrService, TOAST_CONFIG } from 'ngx-toastr';
import { InjectionToken } from '@angular/core';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatIconModule } from '@angular/material/icon'; 

describe('ProductCardComponent', () => {
  let component: ProductCardComponent;
  let fixture: ComponentFixture<ProductCardComponent>;

  const mockToastrService = {
    success: jest.fn(),
    error: jest.fn(),
    warning: jest.fn(),
    info: jest.fn(),
  };

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ProductCardComponent],
      imports: [
        HttpClientTestingModule,
        MatProgressSpinnerModule,
        MatIconModule 
      ],
      providers: [
        { provide: MatDialog, useValue: {} },
        CartService,
        { provide: ToastrService, useValue: mockToastrService },
        { provide: TOAST_CONFIG, useValue: {} },
      ],
    });
    fixture = TestBed.createComponent(ProductCardComponent);
    component = fixture.componentInstance;
    
 
    component.product = { 
      name: 'Test Product', 
      price: 100, 
      type: 'Electronics', 
      description: 'A test product description', 
      review: 5 
    };  

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
