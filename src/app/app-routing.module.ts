import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { CarListComponent } from './components/car-list/car-list.component';
import { AccountComponent } from './components/account/account.component';
import { CarComponent } from './components/car/car.component';

const routes: Routes = [
  {
    path: '', component: HomeComponent, children: [

     {path: 'cars' , component : CarListComponent}, 
     {path: 'car/:carId', component: CarComponent},
     {path: 'account', component: AccountComponent}, 
     { path: '', redirectTo: 'cars', pathMatch: 'full' },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
