import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Car, cars } from '../models/car';
import {  Company } from '../models/company';

@Component({
  selector: 'app-car',
  templateUrl: './car.component.html',
  styleUrls: ['./car.component.css']
})
export class CarComponent {

  carId : number = 0;
  car!: Car; 
  company: Company = Company;
  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
  ){

  }
  ngOnInit(): void{
    this.carId=Number(this.activatedRoute.snapshot.params['carId']);
    this.car = cars[this.carId-1];
  }

  viewMoreImages() {
   
  }
  
}
