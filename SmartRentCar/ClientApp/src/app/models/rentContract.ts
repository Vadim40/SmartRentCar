import { Car } from "./car";

export interface RentContract {
    rentContractId?: number;
    carId: number;
    contractStatusId: number;
    statusName: string;
    car: Car;
    startDate: Date;
    endDate: Date;
    totalCost: number;
    deposit: number;
    contractAddress?: string;
}

export interface RentContractCreate {
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

