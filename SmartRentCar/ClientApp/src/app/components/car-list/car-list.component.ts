import { Component } from '@angular/core';

import { Router } from '@angular/router';
import { Car } from 'src/app/models/car';
import { CarImage } from 'src/app/models/carInfo';
import { FilterToCars } from 'src/app/models/filtetToCars';
import { CarService } from 'src/app/services/car.service';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.css']
})
export class CarListComponent {


  cars: Car[] = [];
  filter: FilterToCars = {}; 
  constructor(
    private router: Router,
    private carService: CarService
  ){

  }
  ngOnInit() {
   this.getCars();
  }

  redirectToCar(carId:number){
    this.router.navigate(['/car', carId])
  }
  
  getCars(){
    this.carService.getCars(this.filter).subscribe({
      next : (cars: Car []) => {
       
        this.cars = cars
        console.log(this.cars)
        this.cars.forEach(car =>{
          this.carService.getFirstCarImage(car.carId).subscribe({
            next: (carImage: CarImage) => {
              if (!car.carImages) {
                  car.carImages = [];
              }
              car.carImages.push(carImage);
              console.log(carImage)
          },
          error : (error) =>{
            console.error('Ошибка получения фото автомобиля', error)
          }

          })
        })
      } ,
      error : (error) =>{
        console.error('Ошибка получения автомобилей', error)
      }
    })
  }
}
