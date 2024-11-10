import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { LoaderComponent } from './components';
import { AuthorizationInterceptor } from './utils';

@NgModule({
  declarations: [LoaderComponent],
  exports: [LoaderComponent],
  imports: [BrowserModule, HttpClientModule, MatProgressSpinnerModule],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthorizationInterceptor,
      multi: true,
    },
  ],
  bootstrap: [],
})
export class SharedModule {}
