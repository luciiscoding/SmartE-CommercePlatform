import { ComponentFixture, TestBed } from '@angular/core/testing';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog'; 
import { GenericWarningModalComponent } from './generic-warning-modal.component';

describe('GenericWarningModalComponent', () => {
  let component: GenericWarningModalComponent;
  let fixture: ComponentFixture<GenericWarningModalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GenericWarningModalComponent],
      providers: [
        { provide: MatDialogRef, useValue: {} },
        { provide: MAT_DIALOG_DATA, useValue: {} } 
      ]
    });

    fixture = TestBed.createComponent(GenericWarningModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
