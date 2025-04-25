

export interface RentContract {
    rentContractId?: number;
    carId?: number;
    carName?: string;
    contractStatusId: number;
    contractStatusName: string;
    depositStatusId: number;
    depostitStatusName: string;
    startDate: Date;
    endDate: Date;
    totalCost: number;
    deposit: number;
    contractAddress?: string;
}

export interface RentContractСreate {
    carId: number;
    startDate: Date;
    endDate: Date;
    totalCost: number;
    deposit: number;
}

export enum RentContractStatus {
    PendingConfirmation = 1,  // Ожидание подтверждения
    PendingStart = 2,         // Ожидание начала аренды (оплачено, но не началось)
    Active = 3,               // Активный
    PendingInspection = 4,     // Ожидание инспекции (проверка автомобиля после возврата)
    PendingResolution = 5,     // Ожидание решения по возврату (обнаружены проблемы)
    Canceled = 6,             // Отменённый
    Completed = 7             // Завершенный
}


export interface RentContractUpdate {
    rentContractId: number;
    contractStatusId?: number;
    contractAddress?: string;
    
}

export const rentContracts: RentContract[] = [
    {
      rentContractId: 1,
      carId: 101,
      carName: "Tesla Model S",
      contractStatusId: 1,
      contractStatusName: "Ожидание подтверждения",
      depositStatusId: 1,
      depostitStatusName: "Ожидание оплаты",
      startDate: new Date("2025-04-01"),
      endDate: new Date("2025-04-10"),
      totalCost: 500,
      deposit: 100,
      contractAddress: "0x1A2b3C4D5E6F7890ABCDEF1234567890abcdef12"
    },
    {
      rentContractId: 2,
      carId: 202,
      carName: "BMW X7",
      contractStatusId: 2,
      contractStatusName: "Ожидание начала аренды",
      depositStatusId: 2,
      depostitStatusName: "Оплачено",
      startDate: new Date("2025-05-01"),
      endDate: new Date("2025-05-15"),
      totalCost: 800,
      deposit: 150,
      contractAddress: "0x2B3c4D5E6F7890ABCDEF1234567890abcdef34"
    },
    {
      rentContractId: 3,
      carId: 303,
      carName: "Audi Q8",
      contractStatusId: 3,
      contractStatusName: "Активный",
      depositStatusId: 3,
      depostitStatusName: "Возвращён",
      startDate: new Date("2025-03-10"),
      endDate: new Date("2025-03-20"),
      totalCost: 700,
      deposit: 200,
      contractAddress: "0x3C4D5E6F7890ABCDEF1234567890abcdef56"
    },
    {
      rentContractId: 4,
      carId: 404,
      carName: "Mercedes-Benz GLE",
      contractStatusId: 4,
      contractStatusName: "Ожидание инспекции",
      depositStatusId: 4,
      depostitStatusName: "На проверке",
      startDate: new Date("2025-02-15"),
      endDate: new Date("2025-02-25"),
      totalCost: 900,
      deposit: 250,
      contractAddress: "0x4D5E6F7890ABCDEF1234567890abcdef78"
    },
    {
      rentContractId: 5,
      carId: 505,
      carName: "Volkswagen Touareg",
      contractStatusId: 5,
      contractStatusName: "Ожидание решения по возврату",
      depositStatusId: 5,
      depostitStatusName: "Не подлежит возврату",
      startDate: new Date("2025-01-05"),
      endDate: new Date("2025-01-20"),
      totalCost: 600,
      deposit: 120,
      contractAddress: "0x5E6F7890ABCDEF1234567890abcdef90"
    }
];
