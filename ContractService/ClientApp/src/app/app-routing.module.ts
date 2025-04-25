import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { RentListComponent } from './components/rent-list/rent-list.component';

const routes: Routes = [
  {
    path: '', component: HomeComponent, children: [

     {path: 'rents' , component : RentListComponent}, 
     { path: '', redirectTo: 'rents', pathMatch: 'full' },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
