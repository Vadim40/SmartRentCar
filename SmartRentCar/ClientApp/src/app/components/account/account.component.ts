import { Component } from '@angular/core';
import { RentContract } from 'src/app/models/rentContract';
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

  rentActive: RentContract [] = [];

  rentHistory: RentContract [] = [];

activeTab: string = 'activeRents';
setActiveTab(tabName: string) {
  this.activeTab = tabName;
}

editUser() {
  console.log('Редактирование пользователя');
}

cancelRent(rent: RentContract) {
  console.log(`Бронирование ${rent.rentId} отменено`);
}
}
