import 'zone.js';  
import 'zone.js/testing';
import { getTestBed } from '@angular/core/testing';
import { BrowserDynamicTestingModule, platformBrowserDynamicTesting } from '@angular/platform-browser-dynamic/testing';

// Inițializează mediul de testare pentru Angular
getTestBed().initTestEnvironment(
  BrowserDynamicTestingModule,
  platformBrowserDynamicTesting()
);

// Încarcă toate fișierele `.spec.ts` folosind require.context
declare const require: any;
const context = require.context('./', true, /\.spec\.ts$/);

// Execută fișierele de test
context.keys().map(context);
