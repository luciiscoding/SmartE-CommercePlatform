import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HeaderComponent } from './header';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';

@NgModule({
  declarations: [HeaderComponent],
  exports: [HeaderComponent],
  imports: [BrowserModule, MatIconModule, MatTooltipModule],
  providers: [],
})
export class LayoutModule {}
