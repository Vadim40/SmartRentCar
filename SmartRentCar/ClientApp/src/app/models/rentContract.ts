import Decimal from "decimal.js";
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
    totalCost: Decimal;
    deposit: Decimal;
    companyId: number;
}

export enum RentContractStatus {
    PendingPayment = 1,          // Ожидание оплаты
    PendingConfirmation = 2,     // Ожидание подтверждения
    Active = 3,                 // Активная аренда
    PendingEarlyEnd = 4,        // Ожидание досрочного завершения
    PendingCompletion = 5,      // Ожидание завершения
    PendingArbitration = 6,     // Ожидание решения арбитра
    Canceled = 7,               // Отменено
    Completed = 8
}


export interface RentContractUpdate {
    rentContractId: number;
    contractStatusId?: number;
    contractAddress?: string;

}

