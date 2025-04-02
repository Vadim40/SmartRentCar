import { Car } from "./car";

export interface RentContract {
    rentId: number;
    carId: number;
    car: Car;
    startDate: Date;
    endDate: Date;
    totalCost: number;
    deposit: number;
}

export enum RentContractStatus{
    Active = 1,
    Completed =2
}

