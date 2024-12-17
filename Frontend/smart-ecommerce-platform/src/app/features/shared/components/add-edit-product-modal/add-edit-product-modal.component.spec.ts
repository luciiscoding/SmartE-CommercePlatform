import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AddEditProductModalComponent } from './add-edit-product-modal.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { CartService } from '../../../services/cart.service';
import { ReactiveFormsModule } from '@angular/forms'; 

describe('AddEditProductModalComponent', () => {
  let component: AddEditProductModalComponent;
  let fixture: ComponentFixture<AddEditProductModalComponent>;

  const mockDialogData = { product: { id: 1, name: 'Test Product' } };

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AddEditProductModalComponent],
      imports: [HttpClientTestingModule, ReactiveFormsModule], 
      providers: [
        CartService,
        { provide: MatDialogRef, useValue: {} },
        { provide: MAT_DIALOG_DATA, useValue: mockDialogData },
      ],
    });
    fixture = TestBed.createComponent(AddEditProductModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
 
  it('should be invalid when the form is empty', () => {
    component.addProductForm.controls['name'].setValue('');
    fixture.detectChanges();
    
    expect(component.addProductForm.invalid).toBeTruthy();
  });
  
});
