import { DepositDispute } from "./depositDispute";


export interface Rental {
    rentalId: number;
    carId?: number;
    carName?: string;
    rentalStatusId: number;
    rentalStatusName: string;
    startDate: Date;
    endDate: Date;
    totalCost: number;
    deposit: number;
    userMessage?: string ;
    userFiles? : any [];
    contractAddress: string;
    depositDispute: DepositDispute;
}

export interface RentContractCreate {
    carId: number;
    startDate: Date;
    endDate: Date;
    totalCost: number;
    deposit: number;
}


export interface RentalStatus {
  rentalStatusId: number;
  name: string;
}
export enum RentalStatusEnum {
    PendingConfirmation = 1,  // Ожидание подтверждения
    PendingStart = 2,         // Ожидание начала аренды (оплачено, но не началось)
    Active = 3,               // Активный
    PendingInspection = 4,     // Ожидание инспекции (проверка автомобиля после возврата)
    PendingUserResponse = 5,   // Ожидание ответа от клиента (новый статус)
    PendingResolution = 6,     // Ожидание решения по возврату (обнаружены проблемы)
    Canceled = 7,             // Отменённый
    Completed = 8             // Завершенный
}

export enum DepositStatus {
  PendingResolution = 1, // Ожидание решения 
  DepositRefunded = 2,   // Залог возвращен полностью
  PartialRefunded = 3,   // Залог возвращен частично
  DepositWithheld = 4,   // Залог удержан
  DepositDeficit = 5     // Залога недостаточно
}

export interface RentContractUpdate {
    rentalId: number;
    rentalStatusId?: number;
    contractAddress?: string;
    
}

export interface MesssageToUser {
  rentalId: number;
  message: number;
  
}

// export const rentContracts: Rental[] = [
//     {
//       rentalId: 1,
//       carId: 101,
//       carName: "Tesla Model S",
//       rentalStatusId: 1,
//       rentalStatusName: "Ожидание подтверждения",
//       depositStatusId: 1,
//       depositStatusName: "Ожидание оплаты",
//       startDate: new Date("2025-04-01"),
//       endDate: new Date("2025-04-10"),
//       totalCost: 500,
//       deposit: 100,
//       contractAddress: "0x1A2b3C4D5E6F7890ABCDEF1234567890abcdef12"
//     },
//     {
//       rentalId: 2,
//       carId: 202,
//       carName: "BMW X7",
//       rentalStatusId: 2,
//       rentalStatusName: "Ожидание начала аренды",
//       depositStatusId: 2,
//       depositStatusName: "Оплачено",
//       startDate: new Date("2025-05-01"),
//       endDate: new Date("2025-05-15"),
//       totalCost: 800,
//       deposit: 150,
//       contractAddress: "0x2B3c4D5E6F7890ABCDEF1234567890abcdef34"
//     },
//     {
//       rentalId: 3,
//       carId: 303,
//       carName: "Audi Q8",
//       rentalStatusId: 3,
//       rentalStatusName: "Активный",
//       depositStatusId: 3,
//       depositStatusName: "Возвращён",
//       startDate: new Date("2025-03-10"),
//       endDate: new Date("2025-03-20"),
//       totalCost: 700,
//       deposit: 200,
//       contractAddress: "0x3C4D5E6F7890ABCDEF1234567890abcdef56"
//     },
//     {
//       rentalId: 4,
//       carId: 404,
//       carName: "Mercedes-Benz GLE",
//       rentalStatusId: 4,
//       rentalStatusName: "Ожидание инспекции",
//       depositStatusId: 4,
//       depositStatusName: "На проверке",
//       startDate: new Date("2025-02-15"),
//       endDate: new Date("2025-02-25"),
//       totalCost: 900,
//       deposit: 250,
//       contractAddress: "0x4D5E6F7890ABCDEF1234567890abcdef78"
//     },
//     {
//       rentalId: 5,
//       carId: 505,
//       carName: "Volkswagen Touareg",
//       rentalStatusId: 5,
//       rentalStatusName: "Ожидание решения по возврату",
//       depositStatusId: 5,
//       depositStatusName: "Не подлежит возврату",
//       startDate: new Date("2025-01-05"),
//       endDate: new Date("2025-01-20"),
//       totalCost: 600,
//       deposit: 120,
//       contractAddress: "0x5E6F7890ABCDEF1234567890abcdef90",
//       userMessage: "я не согласен, дайте денег"
//     }
// ];
