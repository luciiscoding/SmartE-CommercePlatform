import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LoginComponent, RegisterComponent } from './components';

@NgModule({
  declarations: [LoginComponent, RegisterComponent],
  exports: [],
  imports: [BrowserModule, CommonModule, FormsModule, ReactiveFormsModule],
  providers: [],
})
export class CoreModule {}
