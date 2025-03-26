import flatpickr from 'flatpickr';
import { Russian } from "flatpickr/dist/l10n/ru.js";
import { Component, OnInit, Input } from '@angular/core';
import { Car } from 'src/app/models/car';

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

  ngOnInit(): void {
    const today = new Date();
    today.setHours(0, 0, 0, 0);

    this.flatpickrInstance = flatpickr('#dateRangePicker', {
      mode: 'range',
      locale: Russian,
      dateFormat: "d.m.Y",
      minDate: today,
      disable: this.generateDisabledDates(),
      onClose: (selectedDates) => this.handleDateSelection(selectedDates)
    }) as flatpickr.Instance;
  }

  private generateDisabledDates(): Date[] {
    const disabledDates: Date[] = [];
    if (this.car.bookings) {
      this.car.bookings.forEach(booking => {
        let currentDate = new Date(booking.startDate);
        while (currentDate <= booking.endDate) {
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
      const [startDate, endDate] = selectedDates;
      let hasConflict = false;
      if (this.car.bookings) {
        hasConflict = this.car.bookings.some(booking =>
          startDate <= booking.endDate && endDate >= booking.startDate
        );
      }

      if (hasConflict) {
        alert('Выбранный диапазон пересекается с занятыми периодами.');
        this.resetPicker();
      } else {
        this.calculateCost(startDate, endDate);
        this.selectedDateRange = `${this.formatDate(startDate)} - ${this.formatDate(endDate)}`; 
        console.log('Выбранный диапазон:', startDate, endDate);
      }
    }
  }

  private calculateCost(startDate: Date, endDate: Date): void {
    const days = Math.ceil((endDate.getTime() - startDate.getTime()) / (1000 * 60 * 60 * 24)) + 1;
    this.totalCost = days * this.car.costPerDay + this.car.depositAmount;
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
    this.isPopupOpen = true;
  }

  closePopup() {
    this.isPopupOpen = false;
  }

  confirmBooking() {
    alert('Бронирование подтверждено!');
    this.isPopupOpen = false;

  }
}
