import { Component, OnInit } from '@angular/core';
import flatpickr from 'flatpickr';

@Component({
  selector: 'app-car-booking',
  templateUrl: './car-booking.component.html',
  styleUrls: ['./car-booking.component.css']
})
export class CarBookingComponent implements OnInit {
  bookings = [
    { startDate: new Date(2025, 2, 15), endDate: new Date(2025, 2, 20) },
    { startDate: new Date(2025, 2, 25), endDate: new Date(2025, 2, 28) }
  ];

  ngOnInit(): void {
    // Инициализация Flatpickr
    flatpickr('#dateRangePicker', {
      mode: 'range',
      dateFormat: 'Y-m-d',
      disable: this.generateDisabledDates(),
      onClose: (selectedDates) => {
        if (selectedDates.length === 2) {
          const [startDate, endDate] = selectedDates;

          // Проверяем пересечения с уже существующими бронированиями
          const hasConflict = this.bookings.some(booking =>
            (startDate <= booking.endDate && endDate >= booking.startDate)
          );

          if (hasConflict) {
            alert('Выбранный диапазон пересекается с занятыми периодами.');
            // Сброс выбора
            (document.querySelector('#dateRangePicker') as HTMLInputElement).value = '';
          } else {
            console.log('Выбранный диапазон:', startDate, endDate);
          }
        }
      }
    });
  }

  // Генерация массива всех занятых дат для их блокировки в календаре
  private generateDisabledDates(): Date[] {
    const disabledDates: Date[] = [];

    this.bookings.forEach(booking => {
      let currentDate = new Date(booking.startDate);
      while (currentDate <= booking.endDate) {
        disabledDates.push(new Date(currentDate));
        currentDate.setDate(currentDate.getDate() + 1);
      }
    });

    return disabledDates;
  }
}
