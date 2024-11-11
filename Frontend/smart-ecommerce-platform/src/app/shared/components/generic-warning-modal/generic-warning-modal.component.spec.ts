import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GenericWarningModalComponent } from './generic-warning-modal.component';

describe('GenericWarningModalComponent', () => {
  let component: GenericWarningModalComponent;
  let fixture: ComponentFixture<GenericWarningModalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [GenericWarningModalComponent]
    });
    fixture = TestBed.createComponent(GenericWarningModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
