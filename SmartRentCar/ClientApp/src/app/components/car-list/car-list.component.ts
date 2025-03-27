import { Component } from '@angular/core';

import { Router } from '@angular/router';
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
  filter: FilterToCars = {};
  carBrands: CarBrand [] = [];
  carClasses: CarClass [] = [];
  carFuelTypes: CarFuelType [] = [];
  carTransmissionTypes: CarTransmissionType [] = []
  constructor(
    private router: Router,
    private carService: CarService
  ) {

  }
  ngOnInit() {
    this.getCars();
    this.getCarBrands();
    this.getCarClasses();
    this.getCarFuelTypes();
    this.getCarTransmissionTypes();
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

  getCars() {
    this.carService.getCars(this.filter).subscribe({
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
  
 
  onCarClassSelectionChange(event: any) {
    const selectedValues = event.value as Array<number | string>; // Указываем ожидаемый тип массива
    if (selectedValues.includes(0)) {
      this.filter.carClasses = [0]; // Если выбрано "Все"
    } else {
      this.filter.carClasses = selectedValues.filter((value: number | string) => value !== 0).map(Number); // Преобразуем в числа
    }
    console.log('Выбранные категории:', this.filter.carClasses);
  }
  
  
  onCarBrandSelectionChange(event: any) {
    const selectedValues = event.value as number[]; // Указываем, что это массив чисел
  
    if (selectedValues.includes(0)) {
      // Если выбрано "Все", оставляем только "Все" (0)
      this.filter.carBrands = [0];
    } else {
      // Исключаем "Все" (0) из массива и сохраняем только выбранные бренды
      this.filter.carBrands = selectedValues.filter((value: number | string) => value !== 0).map(Number); 
    }
  
    console.log('Выбранные бренды:', this.filter.carBrands);
  }
  onCarClassChange(event: any) {
    const selectedOptions = Array.from(event.target.selectedOptions);
    const selectedValues = Array.from(event.target.selectedOptions)
    .map((option) => +(option as HTMLOptionElement).value); // Приведение типа
  
  
    if (selectedValues.includes(0)) {
      this.filter.carClasses = [0]; // Если выбрано "Все", сбрасываем остальные значения
    } else {
      this.filter.carClasses = selectedValues.filter(value => value !== 0); // Исключаем "Все"
    }
  
    console.log('Выбранные категории:', this.filter.carClasses);
  }
  dropdownOpen = false;

  toggleDropdown() {
    this.dropdownOpen = !this.dropdownOpen;
  }
  
  onCarClassCheckboxChange(event: any) {
    const value = +event.target.value; // Преобразуем в число
    const isChecked = event.target.checked;
  
    if (value === 0) {
      // Если выбрано "Все", сбросить остальные
      this.filter.carClasses = isChecked ? [0] : [];
    } else {
      if (isChecked) {
        // Добавляем категорию
        this.filter.carClasses = this.filter.carClasses || [];
        this.filter.carClasses = this.filter.carClasses.filter((val) => val !== 0); // Убираем "Все"
        this.filter.carClasses.push(value);
      } else {
        // Убираем категорию
        this.filter.carClasses = this.filter.carClasses || [];
        this.filter.carClasses = this.filter.carClasses.filter((val) => val !== value);
      }
    }
  
    console.log('Выбранные категории:', this.filter.carClasses);
  }
  
  
  onTransmissionChange(event: any) {
    this.filter.carTransmission = event.target.value === '' ? undefined : +event.target.value;
    
  }
  
  onFuelTypeChange(event: any) {
    this.filter.carFuel= event.target.value === '' ? undefined : +event.target.value;
  
  }
}
