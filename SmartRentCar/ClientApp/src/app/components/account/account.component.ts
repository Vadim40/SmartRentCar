import { ChangeDetectorRef, Component } from '@angular/core';
import { forkJoin, switchMap } from 'rxjs';
import { Car } from 'src/app/models/car';
import { CarImage } from 'src/app/models/carInfo';
import { RentContract, RentContractStatus, RentContractUpdate } from 'src/app/models/rentContract';
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

  rentActive: RentContract[] = [];

  rentHistory: RentContract[] = [];
  rentContractStatus = RentContractStatus;

  selectedRent?: RentContract;


  activeTab: string = 'activeRents';

  constructor(private contractService: RentContractService,
    private carService: CarService,
    private cdr: ChangeDetectorRef,
  ) {

  }


  ngOnInit() {
    this.getRents();
    console.log(1400000000000000000 + 2520000000000000000)
  }

  getRents() {
    // Получаем активные и завершенные контракты параллельно
    forkJoin({
      rentActive: this.contractService.getRentContractsActive(),
      rentHistory: this.contractService.getRentContractsCompleted()
    }).subscribe({
      next: ({ rentActive, rentHistory }) => {
        this.rentActive = rentActive;
        this.rentHistory = rentHistory;

        // Обрабатываем автомобили и изображения для каждого контракта
        [...this.rentActive, ...this.rentHistory].forEach(rent => {
          this.carService.getCar(rent.carId).pipe(
            switchMap(car => {
              rent.car = car;
              return this.carService.getCarImages(car.carId);
            })
          ).subscribe({
            next: (carImage: CarImage[]) => {
              if (!rent.car.carImages) {
                rent.car.carImages = [];
              }
              rent.car.carImages.push(...carImage);
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

  CancelBooking() { }

  openPopup(rent: any) {
    this.selectedRent = rent;
  }

  closePopup() {
    this.selectedRent = undefined;
  }



 

  confirmAndOpen(event: MouseEvent, url: string): void {
    event.preventDefault();
    const confirmed = confirm(`Перейти по ссылке?\n\n${url}`);
    if (confirmed) {
      window.open(url, '_blank');
    }
  }
  stopPropagation(event: Event) {
    event.stopPropagation();
  }

  async depositFunds() {
    if (!this.selectedRent?.contractAddress) return;
    
    await this.handleContractAction(
      () => this.contractService.depositFunds(this.selectedRent!.contractAddress as string, this.selectedRent!.deposit, this.selectedRent!.totalCost),
      "Оплата прошла",
      RentContractStatus.PendingConfirmation
    );
  }
  
  async cancelContract() {
    if (!this.selectedRent?.contractAddress) return;
  
    await this.handleContractAction(
      () => this.contractService.cancelRent(this.selectedRent!.contractAddress as string),
      "Бронь отменена",
      RentContractStatus.Canceled
    );
  }
  
  async renterConfirmStart() {
    if (!this.selectedRent?.contractAddress) return;
  
    await this.handleContractAction(
      () => this.contractService.confirmStart(this.selectedRent!.contractAddress as string),
      "Начало аренды подтверждено.",
      RentContractStatus.PendingCompletion
    );
  }
  
  async finishRentalEarly() {
    if (!this.selectedRent?.contractAddress) return;
  
    await this.handleContractAction(
      () => this.contractService.finishRentalEarly(this.selectedRent!.contractAddress as string),
      "Запрошено досрочное завершение аренды.",
      RentContractStatus.PendingEarlyEnd
    );
  }
  
  async approveCompletion() {
    if (!this.selectedRent?.contractAddress) return;
  
    await this.handleContractAction(
      () => this.contractService.approveCompletion(this.selectedRent!.contractAddress as string),
      "Завершение аренды подтверждено.",
      RentContractStatus.Completed
    );
  }
  
  async raiseDispute() {
    if (!this.selectedRent?.contractAddress) return;
  
    await this.handleContractAction(
      () => this.contractService.raiseDispute(this.selectedRent!.contractAddress as string),
      "Спор был инициирован.",
      RentContractStatus.PendingArbitration
    );
  }
  
  private async handleContractAction(
    actionFn: () => Promise<void>,
    successMessage: string,
    newStatus?: RentContractStatus
  ) {
    try {
      await actionFn();
      alert(successMessage);
  
      if (this.selectedRent?.rentContractId && newStatus !== undefined) {
        const rentUpdate: RentContractUpdate = {
          rentContractId: this.selectedRent.rentContractId,
          contractStatusId: newStatus
        };
        this.contractService.updateRentContract(rentUpdate).subscribe();
      }
  
      this.closePopup();
      this.getRents();
      this.cdr.detectChanges();
    } catch (error: any) {
      console.error(`Ошибка при выполнении действия:`, error);
      if (error.code === 'ACTION_REJECTED') {
        alert("Вы отменили подписание транзакции.");
      } else {
        alert("Ошибка при выполнении действия.");
      }
    }
  }
  
}
