import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { RentListComponent } from './components/rent-list/rent-list.component';
import { DateComponent } from './components/date/date.component';
import { DxDateBoxModule, DxTextBoxModule } from 'devextreme-angular';
import { MatSelectModule } from '@angular/material/select';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    RentListComponent,
    DateComponent,
   
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    DxDateBoxModule,
    MatSelectModule,
    DxTextBoxModule,
    BrowserAnimationsModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
