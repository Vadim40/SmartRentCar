import { Component } from '@angular/core';
import { Car, cars } from '../models/car';
import { Router } from '@angular/router';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.css']
})
export class CarListComponent {


  cars: Car[] = cars;

  constructor(
    private router: Router
  ){

  }
  ngOnInit() {
    console.log(this.cars);
  }

  redirectToCar(carId:number){
    this.router.navigate(['/car', carId])
  }
}
