import { Component } from '@angular/core';
import { forkJoin, switchMap } from 'rxjs';
import { Car } from 'src/app/models/car';
import { CarImage } from 'src/app/models/carInfo';
import { RentContract, RentContractStatus } from 'src/app/models/rentContract';
import { User } from 'src/app/models/user';
import { CarService } from 'src/app/services/car.service';
import { RentContractService } from 'src/app/services/contract.service';


@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrls: ['./account.component.css']
})
export class AccountComponent {

  user: User = {
    userId: 1,
    avatar: 'assets/ava.jpg',
    email: 'ivan.ivanov@example.com',
    phone: '+7 123 456 7890',
    license: '1234-567890'
};

  rentActive: RentContract [] = [];

  rentHistory: RentContract [] = [];
  rentContractStatus = RentContractStatus;

activeTab: string = 'activeRents';

constructor(private contractService: RentContractService,
            private carService: CarService
){

}


ngOnInit() {
  this.getRents();
}

getRents() {
  // Получаем активные и завершенные контракты параллельно
  forkJoin({
    rentActive: this.contractService.getRentContractsByStatus(RentContractStatus.Active),
    rentHistory: this.contractService.getRentContractsByStatus(RentContractStatus.Completed)
  }).subscribe({
    next: ({ rentActive, rentHistory }) => {
      this.rentActive = rentActive;
      this.rentHistory = rentHistory;

      // Обрабатываем автомобили и изображения для каждого контракта
      [...this.rentActive, ...this.rentHistory].forEach(rent => {
        this.carService.getCar(rent.carId).pipe(
          switchMap(car => {
            rent.car = car;
            return this.carService.getFirstCarImage(car.carId);
          })
        ).subscribe({
          next: (carImage: CarImage) => {
            if (!rent.car.carImages) {
              rent.car.carImages = [];
            }
            rent.car.carImages.push(carImage);
          },
          error: error => console.error("Ошибка загрузки автомобиля или изображения", error)
        });
      });
    },
    error: error => console.error("Ошибка получения контрактов", error)
  });
}

setActiveTab(tabName: string) {
  this.activeTab = tabName;
}

editUser() {
  console.log('Редактирование пользователя');
  console.log('some');
  

}

cancelRent(rent: RentContract) {
  console.log(`Бронирование ${rent.rentId} отменено`);
}

CancelBooking(){}
}
