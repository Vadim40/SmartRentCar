import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';

import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { DxButtonModule, DxDateBoxModule, DxTextBoxModule, DxSelectBoxModule, DxListModule, DxDateRangeBoxModule, DxCalendarModule } from 'devextreme-angular';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { CarListComponent } from './components/car-list/car-list.component';
import { DateComponent } from './components/date/date.component';
import { AccountComponent } from './components/account/account.component';
import { CarComponent } from './components/car/car.component';
import { CarBookingComponent } from './components/car-booking/car-booking.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { DateInterceptor } from './services/config/date.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CarListComponent,
    DateComponent,
    AccountComponent,
    CarComponent,
    CarBookingComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    DxButtonModule,
    DxDateBoxModule,
    DxTextBoxModule,
    DxListModule,
    DxDateRangeBoxModule,
    DxCalendarModule,
    FormsModule,
    ReactiveFormsModule,
    MatDatepickerModule, 
    MatNativeDateModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatFormFieldModule
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: DateInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
