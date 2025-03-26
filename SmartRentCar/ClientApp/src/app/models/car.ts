import { CarImage } from "./carInfo";

export interface Car {
    carId: number;
    carName: string; 
    fuelTypeName: string; 
    className: string; 
    transmissioName: string; 
    carImages?: CarImage[]; 
    costPerDay: number; 
    depositAmount: number; 
    seats: number; 
    airConditioning: boolean; 
    gps: boolean; 
    modelYear: number; 
    bookings?: BookingPeriod[];
}

export interface BookingPeriod {
    startDate: Date; 
    endDate: Date;   
}

export const cars: Car[] = [
    {
        carId: 1,
        carName: "Toyota Camry",
        fuelTypeName: "Бензин",
        className: "Седан",
        transmissioName: "Автоматическая",
        costPerDay: 3000,
        depositAmount: 15000,
        seats: 5,
        airConditioning: true,
        gps: true,
        modelYear: 2022,
        bookings: [
            { startDate: new Date(2025, 2, 10), endDate: new Date(2025, 2, 15) },
            { startDate: new Date(2025, 2, 20), endDate: new Date(2025, 2, 25) }
        ]
    },
    {
        carId: 2,
        carName: "Honda Accord",
        fuelTypeName: "Бензин",
        className: "Седан",
        transmissioName: "Механическая",
        costPerDay: 2800,
        depositAmount: 14000,
        seats: 5,
        airConditioning: true,
        gps: false,
        modelYear: 2021,
        bookings: [
            { startDate: new Date(2025, 2, 12), endDate: new Date(2025, 2, 18) },
            { startDate: new Date(2025, 2, 22), endDate: new Date(2025, 2, 24) }
        ]
    },
    {
        carId: 3,
        carName: "Ford Focus",
        fuelTypeName: "Дизель",
        className: "Хэтчбек",
        transmissioName: "Автоматическая",
        costPerDay: 2500,
        depositAmount: 12000,
        seats: 5,
        airConditioning: true,
        gps: false,
        modelYear: 2020,
        bookings: [
            { startDate: new Date(2025, 2, 14), endDate: new Date(2025, 2, 16) },
            { startDate: new Date(2025, 2, 21), endDate: new Date(2025, 2, 23) }
        ]
    },
    {
        carId: 4,
        carName: "Hyundai Tucson",
        fuelTypeName: "Газ",
        className: "Кроссовер",
        transmissioName: "Автоматическая",
        costPerDay: 4000,
        depositAmount: 20000,
        seats: 7,
        airConditioning: true,
        gps: true,
        modelYear: 2023,
        bookings: [
            { startDate: new Date(2025, 2, 11), endDate: new Date(2025, 2, 17) },
            { startDate: new Date(2025, 2, 26), endDate: new Date(2025, 2, 28) }
        ]
    },
    {
        carId: 5,
        carName: "BMW 5 Series",
        fuelTypeName: "Бензин",
        className: "Седан",
        transmissioName: "Автоматическая",
        costPerDay: 5000,
        depositAmount: 25000,
        seats: 5,
        airConditioning: true,
        gps: true,
        modelYear: 2022,
        bookings: [
            { startDate: new Date(2025, 2, 13), endDate: new Date(2025, 2, 19) },
            { startDate: new Date(2025, 2, 22), endDate: new Date(2025, 2, 27) }
        ]
    }
];
