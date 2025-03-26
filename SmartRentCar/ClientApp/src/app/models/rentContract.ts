import { Car, cars } from "./car";

export interface RentContract {
    rentId: number;
    car: Car;
    startDate: Date;
    endDate: Date;
    cost: number;
    deposit: number;
}

export const RentActive: RentContract[] = [
    {
        rentId: 101,
        car: cars[2],
        startDate: new Date("2025-04-10"),
        endDate: new Date("2025-04-15"),
        cost: 500, 
        deposit: 20000 
    }
];

export const RentHistory: RentContract[] = [
    {
        rentId: 201,
        car: cars[0],
        startDate: new Date("2025-03-01"),
        endDate: new Date("2025-03-05"),
        cost: 300,
        deposit: 15000
    },
    {
        rentId: 202,
        car: cars[1],
        startDate: new Date("2025-02-10"),
        endDate: new Date("2025-02-15"),
        cost: 450,
        deposit: 20000
    },
    {
        rentId: 203,
        car: cars[3],
        startDate: new Date("2025-01-20"),
        endDate: new Date("2025-01-25"),
        cost: 400,
        deposit: 18000
    },
    {
        rentId: 204,
        car: cars[4],
        startDate: new Date("2025-03-10"),
        endDate: new Date("2025-03-15"),
        cost: 350,
        deposit: 17000
    }
];
