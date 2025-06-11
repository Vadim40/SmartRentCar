import { CarImage } from "./carInfo";

export interface Car {
    carId: number;
    carName: string; 
    fuelTypeName: string; 
    className: string; 
    transmissionTypeName: string; 
    carImages: CarImage[]; 
    costPerDay: bigint; 
    depositAmount: bigint; 
    bookings?: CarBooking[];
}

export interface CarBooking {
    startDate: Date; 
    endDate: Date;   
}

