import { Component } from '@angular/core';
import { Car, cars } from '../models/car';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.css']
})
export class CarListComponent {
  cars: Car[] = cars;
  ngOnInit() {
    console.log(this.cars);
  }


}
