import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Car } from 'src/app/models/car';
import { CarImage } from 'src/app/models/carInfo';
import { Company } from 'src/app/models/company';
import { CarService } from 'src/app/services/car.service';

@Component({
  selector: 'app-car',
  templateUrl: './car.component.html',
  styleUrls: ['./car.component.css']
})
export class CarComponent {

  carId: number = 0;
  car!: Car;
  company: Company = Company;

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private carService: CarService,
  ) { }

  ngOnInit(): void {
    this.carId = Number(this.activatedRoute.snapshot.params['carId']);
    this.getCar(this.carId);
  }

  viewMoreImages(): void {

  }

  getCar(carId: number) {
    this.carService.getCar(carId).subscribe({
      next: (car: Car) => {

        console.log(car)
        this.car = car;
        this.carService.getCarImages(carId).subscribe({
          next: (carImages: CarImage[]) => {
            console.log(carImages)
            this.car.carImages = carImages;
          },
          error: (error) => {
            console.error('Ошибка получения фотографий автомобиля', error)
          }
        })
      },
      error: (error) => {
        console.error('Ошибка получения  автомобиля', error)
      }
    })
  }
  async processImages(): Promise<void> {
    const filePaths = ['./assets/hy1.jpg', './assets/hy2.jpg', './assets/hy3.jpg'];

    for (const filePath of filePaths) {
      const response = await fetch(filePath);
      const blob = await response.blob();

      const base64 = await this.blobToBase64(blob);


      console.log(`INSERT INTO CarImages (CarId, ImageData) VALUES (4, '${base64}');`);
    }
  }

  private blobToBase64(blob: Blob): Promise<string> {
    return new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.onloadend = () => resolve(reader.result as string);
      reader.onerror = reject;
      reader.readAsDataURL(blob);
    });
  }

}
