import { Component } from '@angular/core';

export interface BookingPeriod {
  startDate: Date;
  endDate: Date;
}

@Component({
  selector: 'app-car-booking',
  templateUrl: './car-booking.component.html',
  styleUrls: ['./car-booking.component.css']
})
export class CarBookingComponent {
  // Пример бронированных периодов
  bookings: BookingPeriod[] = [
    { startDate: new Date('2025-02-23'), endDate: new Date('2025-02-25') },
    { startDate: new Date('2025-03-15'), endDate: new Date('2025-03-20') },
    { startDate: new Date('2025-04-01'), endDate: new Date('2025-04-05') }
  ];

  // Минимальная и максимальная доступные даты
  minDate: Date = new Date('2025-01-01');
  maxDate: Date = new Date('2025-12-31');

  // Изначально выбранный диапазон (например, с 1 января 2025 по 10 января 2025)
  selectedRange: Date[] = [ new Date('2025-01-01'), new Date('2025-01-10') ];

  // Функция, которая будет фильтровать (блокировать) даты, попадающие в бронированные периоды
  dateFilter = (date: Date | null): boolean => {
    if (!date) return true;
    const day = date.getDay();
    // например, блокировать воскресенье (0) и субботу (6)
    return day !== 0 && day !== 6;
  }

  confirmBooking() {
    if (this.selectedRange && this.selectedRange.length === 2) {
      alert(`Бронирование подтверждено: ${this.selectedRange[0].toLocaleDateString()} - ${this.selectedRange[1].toLocaleDateString()}`);
    }
  }
}
