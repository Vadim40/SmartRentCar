import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { DxButtonModule, DxDateBoxModule, DxSelectBoxModule, DxTextBoxModule } from 'devextreme-angular';
import { CarListComponent } from './components/car-list/car-list.component';
import { loadMessages, locale } from 'devextreme/localization';
import ruMessages from 'devextreme/localization/messages/ru.json';
import { DateComponent } from './components/date/date.component';


loadMessages(ruMessages);
locale('ru');

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CarListComponent,
    DateComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    DxButtonModule,
    DxDateBoxModule,
    DxTextBoxModule,
    DxSelectBoxModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
