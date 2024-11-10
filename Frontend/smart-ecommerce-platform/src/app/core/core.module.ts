import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LoginComponent, RegisterComponent } from './components';
import { SharedModule } from '@app/shared';

@NgModule({
  declarations: [LoginComponent, RegisterComponent],
  exports: [],
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
  ],
  providers: [],
})
export class CoreModule {}
