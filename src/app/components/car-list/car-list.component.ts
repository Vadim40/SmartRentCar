import { Component } from '@angular/core';

import { Router } from '@angular/router';
import { Car, cars } from 'src/app/models/car';

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
