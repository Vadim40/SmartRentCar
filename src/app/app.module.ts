import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { DxButtonModule, DxCalendarModule, DxDateBoxModule, DxListModule, DxSelectBoxModule, DxTemplateModule, DxTextBoxModule } from 'devextreme-angular';
import { CarListComponent } from './components/car-list/car-list.component';
import { loadMessages, locale } from 'devextreme/localization';
import ruMessages from 'devextreme/localization/messages/ru.json';
import { DateComponent } from './components/date/date.component';
import { AccountComponent } from './components/account/account.component';
import { CarComponent } from './components/car/car.component';
import { CarBookingComponent } from './components/car-booking/car-booking.component';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button'



loadMessages(ruMessages);
locale('ru');

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
    AppRoutingModule,
    DxButtonModule,
    DxDateBoxModule,
    DxTextBoxModule,
    DxSelectBoxModule,
    DxListModule,
    MatDatepickerModule, 
    MatNativeDateModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
