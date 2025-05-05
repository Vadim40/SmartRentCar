import flatpickr from 'flatpickr';
import { Russian } from "flatpickr/dist/l10n/ru.js";
import { Component, OnInit, Input } from '@angular/core';
import { Car, CarBooking } from 'src/app/models/car';
import { CarService } from 'src/app/services/car.service';
import { RentContractService } from 'src/app/services/contract.service';
import { RentContract, RentContractStatus, RentContractUpdate, RentContractCreate } from 'src/app/models/rentContract';
import { firstValueFrom } from 'rxjs';

@Component({
  selector: 'app-car-booking',
  templateUrl: './car-booking.component.html',
  styleUrls: ['./car-booking.component.css']
})
export class CarBookingComponent implements OnInit {
  @Input() car!: Car;

  private flatpickrInstance: flatpickr.Instance | undefined;
  totalCost: number = 0;
  canProceed: boolean = false;
  isPopupOpen: boolean = false;
  selectedDateRange: string = '';
  startDate!: Date;
  endDate!: Date;
  rentContractId: number = 0;
  constructor(
    private carService: CarService,
    private rentContractService: RentContractService
  ) {

  }
  ngOnInit(): void {
    this.getCarBookings();
  }

  getCarBookings() {
    this.carService.getCarBookings(this.car.carId).subscribe({
      next: (carBookings: CarBooking[]) => {
        this.car.bookings = carBookings;
        console.log(carBookings);

        const today = new Date();
        today.setHours(0, 0, 0, 0);

        this.flatpickrInstance = flatpickr('#dateRangePicker', {
          mode: 'range',
          locale: Russian,
          dateFormat: "d.m.Y",
          minDate: today,
          disable: this.generateDisabledDates(), // Теперь даты заблокируются корректно
          onClose: (selectedDates) => this.handleDateSelection(selectedDates)
        }) as flatpickr.Instance;
      },
      error: (error) => {
        console.error("Ошибка получения бронирования", error);
      }
    });
  }

  private generateDisabledDates(): Date[] {
    const disabledDates: Date[] = [];
    if (this.car.bookings) {
      this.car.bookings.forEach(booking => {
        let currentDate = new Date(booking.startDate);
        let endDate = new Date(booking.endDate);

        while (currentDate <= endDate) {
          disabledDates.push(new Date(currentDate));
          currentDate.setDate(currentDate.getDate() + 1);
        }
      });
    }
    return disabledDates;
  }


  private handleDateSelection(selectedDates: Date[]): void {
    if (selectedDates.length === 1) {
      const selectedDate = selectedDates[0];
      let isDisabled = null;
      if (this.car.bookings) {
        isDisabled = this.car.bookings.some(booking =>
          selectedDate >= booking.startDate && selectedDate <= booking.endDate
        );
      }

      if (isDisabled) {
        alert('Выбранная дата занята.');
        this.resetPicker();
      } else {
        console.log('Выбранная дата:', selectedDate);
      }
    } else if (selectedDates.length === 2) {
      [this.startDate, this.endDate] = selectedDates;
      let hasConflict = false;
      if (this.car.bookings) {
        hasConflict = this.car.bookings.some(booking =>
          this.startDate <= booking.endDate && this.endDate >= booking.startDate
        );
      }

      if (hasConflict) {
        alert('Выбранный диапазон пересекается с занятыми периодами.');
        this.resetPicker();
      } else {
        this.calculateCost(this.startDate, this.endDate);
        this.selectedDateRange = `${this.formatDate(this.startDate)} - ${this.formatDate(this.endDate)}`;
        console.log('Выбранный диапазон:', this.startDate, this.endDate);
      }
    }
  }

  private calculateCost(startDate: Date, endDate: Date): void {
    const days = Math.ceil((endDate.getTime() - startDate.getTime()) / (1000 * 60 * 60 * 24)) + 1;
    this.totalCost = days * this.car.costPerDay;
    this.canProceed = true;
  }

  private formatDate(date: Date): string {
    const day = String(date.getDate()).padStart(2, '0');
    const month = String(date.getMonth() + 1).padStart(2, '0');
    const year = date.getFullYear();
    return `${day}.${month}.${year}`;
  }

  resetPicker(): void {
    if (this.flatpickrInstance) {
      this.flatpickrInstance.clear();
    }
    const datePicker = document.querySelector('#dateRangePicker') as HTMLInputElement;
    if (datePicker) {
      datePicker.value = '';
    }
    this.totalCost = 0;
    this.canProceed = false;
    this.selectedDateRange = '';
  }

  openPopup() {
    if (!this.selectedDateRange) {
      alert(" Пожалуйста, выберите даты!");
      return;
    }
    this.isPopupOpen = true;
  }

  closePopup() {
    this.isPopupOpen = false;
  }

  async confirmBooking() {

    try {

      await this.createRentContract();
      const contractAddress = await this.rentContractService.createRentContract(
        this.car.depositAmount,
        this.totalCost,
        Math.floor(this.startDate.getTime() / 1000),
        Math.floor(this.endDate.getTime() / 1000),
        48
      );

      alert(" Бронирование подтверждено!");
      this.isPopupOpen = false;
      await this.UpdateRentContract(contractAddress);


    } catch (error: any) {
      console.error(" Ошибка при создании контракта:", error);

      if (error.code == 'ACTION_REJECTED') {
        console.log(this.rentContractId)
        this.rentContractService.deleteRentContractById(this.rentContractId).subscribe();
        alert(" Вы отменили подписание транзакции.");
        this.isPopupOpen = false;
      } else {
        alert(" Ошибка при бронировании");
      }
    }

  }

  async createRentContract(): Promise<number> {
    const rent: RentContractCreate = {
      carId: this.car.carId,
      startDate: this.startDate,
      endDate: this.endDate,
      totalCost: this.totalCost,
      deposit: this.car.depositAmount,
    };

    try {
      return new Promise((resolve, reject) => {
        this.rentContractService.saveRentContract(rent).subscribe({
          next: (rentContractId: number) => {
            this.rentContractId = rentContractId;
            resolve(rentContractId);
          },
          error: (err) => {
            console.error(" Ошибка при сохранении контракта:", err);
            reject(err);
          }
        });
      });
    } catch (error) {
      console.error(" Ошибка при сохранении контракта:", error);
      throw error;
    }
  }


  async UpdateRentContract(contractAddress: string) {
    const rentUpdate: RentContractUpdate = {
      rentContractId: this.rentContractId,
      contractAddress: contractAddress
    };
    console.log(rentUpdate)
    try {
      this.rentContractService.updateRentContract(rentUpdate).subscribe();
    } catch (error) {
      console.error("Ошибка при подтверждении аренды:", error);
    }

  }
}
