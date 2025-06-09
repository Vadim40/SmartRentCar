import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { RentListComponent } from './components/rent-list/rent-list.component';
import { DateComponent } from './components/date/date.component';
import { DxButtonModule, DxDateBoxModule, DxTextBoxModule } from 'devextreme-angular';
import { MatSelectModule } from '@angular/material/select';
import { ScrollingModule } from '@angular/cdk/scrolling';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { CompanyRentListComponent } from './components/company-rent-list/company-rent-list.component';
import { AuthInterceptor } from './services/config/auth.interceptor';
import { LoginComponent } from './components/login/login.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    RentListComponent,
    DateComponent,
    CompanyRentListComponent,
    LoginComponent,
   
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    DxDateBoxModule,
    MatSelectModule,
    DxTextBoxModule,
    DxButtonModule,
    BrowserAnimationsModule,
    ScrollingModule,
    FormsModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ]
  ,
  bootstrap: [AppComponent]
})
export class AppModule { }
