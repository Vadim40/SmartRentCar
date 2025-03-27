import { Car } from "./car";

export interface RentContract {
    rentId: number;
    car: Car;
    startDate: Date;
    endDate: Date;
    cost: number;
    deposit: number;
}

