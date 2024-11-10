import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { LoaderComponent } from './components/loader/loader.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

@NgModule({
  declarations: [LoaderComponent],
  exports: [LoaderComponent],
  imports: [BrowserModule, HttpClientModule, MatProgressSpinnerModule],
  providers: [],
  bootstrap: [],
})
export class SharedModule {}
