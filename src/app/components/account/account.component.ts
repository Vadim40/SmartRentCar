import { Component } from '@angular/core';
import { Rent, RentActive, RentHistory } from 'src/app/models/rent';
import { User } from 'src/app/models/user';


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

  rentActive: Rent [] = RentActive

  rentHistory: Rent [] = RentHistory

activeTab: string = 'activeRents';
setActiveTab(tabName: string) {
  this.activeTab = tabName;
}

editUser() {
  console.log('Редактирование пользователя');
}

cancelRent(rent: Rent) {
  console.log(`Бронирование ${rent.rentId} отменено`);
}
}
