import { Component } from '@angular/core';

import { Router } from '@angular/router';
import { debounceTime, Subject, Subscription } from 'rxjs';
import { Car } from 'src/app/models/car';
import { CarBrand, CarClass, CarFuelType, CarImage, CarTransmissionType } from 'src/app/models/carInfo';
import { FilterToCars } from 'src/app/models/filtetToCars';
import { CarService } from 'src/app/services/car.service';

@Component({
  selector: 'app-car-list',
  templateUrl: './car-list.component.html',
  styleUrls: ['./car-list.component.css']
})
export class CarListComponent {


  cars: Car[] = [];
  private filterSubject = new Subject<FilterToCars>();
  filter: FilterToCars = {
    carClasses: [0],
    carBrands: [0],
    carFuel: 0,
    carTransmission: 0
  };
  carBrands: CarBrand [] = [];
  carClasses: CarClass [] = [];
  carFuelTypes: CarFuelType [] = [];
  carTransmissionTypes: CarTransmissionType [] = []

  selectedCarClasses: number [] = [];
  selectedCarBrands: number [] = [];
  startDate: Date | undefined = undefined;
  endDate: Date | undefined = undefined;


  constructor(
    private router: Router,
    private carService: CarService
  ) {
   
  }
  private filterSubscription!: Subscription;



  ngOnInit() {
    this.filterSubscription = this.filterSubject.pipe(
      debounceTime(1000)
    ).subscribe(updatedFilter => {
      this.getCars(updatedFilter);
    });
    this.getCars(this.filter);
    this.getCarBrands();
    this.getCarClasses();
    this.getCarFuelTypes();
    this.getCarTransmissionTypes();
  }
  ngOnDestroy(): void {
    this.filterSubscription.unsubscribe();
  }
  
  getFirstCarImage(car: Car): string | null {
    if (car.carImages && car.carImages.length > 0 && car.carImages[0]?.imageData) {
      return car.carImages[0].imageData;
    }
    return null;
  }

  redirectToCar(carId: number) {
    this.router.navigate(['/car', carId])
  }

  getCars(filter: FilterToCars) {
    this.carService.getCars(filter).subscribe({
      next: (cars: Car[]) => {

        this.cars = cars
        // console.log(this.cars)
        this.cars.forEach(car => {
          this.carService.getFirstCarImage(car.carId).subscribe({
            next: (carImage: CarImage) => {
              if (!car.carImages) {
                car.carImages = [];
              }
              car.carImages.push(carImage);

            },
            error: (error) => {
              console.error('Ошибка получения фото автомобиля', error)
            }

          })

        })
        console.log(cars)
      },
      error: (error) => {
        console.error('Ошибка получения автомобилей', error)
      }
    })
  }

  getCarBrands(){
    this.carService.getBrands().subscribe({
      next: (carBrands: CarBrand []) =>{
        this.carBrands = carBrands
      },
      error : (error) =>{
        console.error('Ошибка получения брендов', error)
      }
    })
  }
  getCarClasses() {
    this.carService.getCarClasses().subscribe({
      next: (carClasses: CarClass[]) => {
        this.carClasses = carClasses;
      },
      error: (error) => {
        console.error('Ошибка получения классов автомобилей', error);
      }
    });
  }
  
  getCarFuelTypes() {
    this.carService.getFuelTypes().subscribe({
      next: (carFuelTypes: CarFuelType[]) => {
        this.carFuelTypes = carFuelTypes;
      },
      error: (error) => {
        console.error('Ошибка получения типов топлива', error);
      }
    });
  }
  
  getCarTransmissionTypes() {
    this.carService.getTransmissionTypes().subscribe({
      next: (carTransmissionTypes: CarTransmissionType[]) => {
        this.carTransmissionTypes = carTransmissionTypes;
      },
      error: (error) => {
        console.error('Ошибка получения типов трансмиссий', error);
      }
    });
  }
  
  updateFilter() {
    this.filterSubject.next(this.filter);
  }
 
  onCarClassSelectionChange(event: any) {
    if (this.filter.carClasses?.includes(0) && this.selectedCarClasses.length>0 && !this.selectedCarClasses.includes(0)) {
      this.filter.carClasses = [0]; 
      this.selectedCarClasses = [0];
    } else {
      this.filter.carClasses = this.filter.carClasses?.filter((value: number) => value !== 0); 
      this.selectedCarClasses = this.filter.carClasses || []
  }
  this.updateFilter();
}
  
  onCarBrandSelectionChange(event: any) {

    if ( this.filter.carBrands?.includes(0) && this.selectedCarBrands.length>0 && !this.selectedCarBrands.includes(0)) {
      this.filter.carBrands = [0];
      this.selectedCarBrands = [0];
    } 
    else {
      this.filter.carBrands = this.filter.carBrands?.filter((value: number) => value !== 0); 
      this.selectedCarBrands = this.filter.carBrands || []
    }
    this.updateFilter();
  }



  onTransmissionChange(event: any) {
    console.log('some')
    this.filter.carTransmission = event.value;
    this.updateFilter();
  }
  
  onFuelTypeChange(event: any) {
    this.filter.carFuel= event.value;
    this.updateFilter();
  }
  onCostMinChange(event: any) {
    this.filter.costMin = event.value
    this.updateFilter();
  }
  
  onCostMaxChange(event: any) {
    this.filter.costMax = event.value
    this.updateFilter();
  }
  
  onDepositMinChange(event: any) {
    this.filter.depositMin = event.value ;
    this.updateFilter(); 
  }
  
  onDepositMaxChange(event: any) {

    this.filter.depositMax = event.value;
    this.updateFilter(); 
  }

  onStartDateChange(event: Date) {
    this.startDate = event;
    this.checkDateAndUpdateFilter();
  }


  onEndDateChange(event: Date) {
    this.endDate = event;
    this.checkDateAndUpdateFilter();

  }
private checkDateAndUpdateFilter() {
  if (this.startDate != null && this.endDate != null) { 
    this.filter.startDate = this.startDate;
    this.filter.endDate = this.endDate;
    console.log(this.filter);
    this.updateFilter(); 
  }
}

}
