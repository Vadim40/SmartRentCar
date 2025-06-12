import Decimal from "decimal.js";
import { CarImage } from "./carInfo";

export interface Car {
    carId: number;
    companyId: number;
    carName: string; 
    fuelTypeName: string; 
    className: string; 
    transmissionTypeName: string; 
    carImages: CarImage[]; 
    costPerDay: Decimal; 
    depositAmount: Decimal; 
    bookings?: CarBooking[];
}

export interface CarBooking {
    startDate: Date; 
    endDate: Date;   
}

