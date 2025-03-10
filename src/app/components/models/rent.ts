import { Car, cars } from "./car";

export interface Rent{
    rentId: number;
    car: Car;
    startDate: Date;
    endDate: Date;
    
}
const carsList: Car [] = cars;
export const RentActive: Rent [] = [
    {
    rentId: 101,
    car: cars[2],
    startDate: new Date("2025-04-10"),
    endDate: new Date("2025-04-15")
}
];

export const RentHistory: Rent[] = [
    {
        rentId: 201,
        car: cars[0],
        startDate: new Date("2025-03-01"),
        endDate: new Date("2025-03-05")
    },
    {
        rentId: 202,
        car: cars[1],
        startDate: new Date("2025-02-10"),
        endDate: new Date("2025-02-15")
    },
    {
        rentId: 203,
        car: cars[3],
        startDate: new Date("2025-01-20"),
        endDate: new Date("2025-01-25")
    },
    {
        rentId: 204,
        car: cars[4],
        startDate: new Date("2025-03-10"),
        endDate: new Date("2025-03-15")
    }
];
