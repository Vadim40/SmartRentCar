import { CarImage } from "./carInfo";

export interface Car {
    carId: number;
    carName: string; 
    fuelTypeName: string; 
    className: string; 
    transmissionTypeName: string; 
    carImages: CarImage[]; 
    costPerDay: number; 
    depositAmount: number; 
    seats: number; 
    airConditioning: boolean; 
    gps: boolean; 
    modelYear: number; 
    bookings?: CarBooking[];
}

export interface CarBooking {
    startDate: Date; 
    endDate: Date;   
}

