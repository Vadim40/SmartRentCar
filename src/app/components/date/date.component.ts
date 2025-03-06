import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

@Component({
  selector: 'date-component',
  templateUrl: './date.component.html',
})
export class DateComponent implements OnInit {

  @Input()
  date: Date | undefined = undefined; // Используем Date | undefined

  @Output()
  dateChange = new EventEmitter<Date>();

  constructor() {}

  ngOnInit(): void {}

  setDate(date: Date | number | string | null) { // Добавляем проверку на null
    if (date) {
      this.date = new Date(date);
      this.dateChange.emit(this.date);
    } else {
      this.date = undefined;
    }
  }

  dateBox_valueChanged() {
    if (this.date) {
      this.dateChange.emit(this.date);
    }
  }
}
