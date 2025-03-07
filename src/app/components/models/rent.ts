import { Car } from "./car";

export interface Rent{
    rentId: number;
    car: Car;
    startDate: Date;
    endDate: Date;
    
}

export const RentActive: Rent [] = [
    {
    rentId: 101,
    car: {
        carId: 3,
        carName: "Ford Focus",
        fuelTypeName: "Дизель",
        carClassName: "Хэтчбек",
        carTransmissioName: "Автоматическая",
        carImage: "assets/3.jpg",
        cost: 2800,
        deposit: 14000
    },
    startDate: new Date("2025-04-10"),
    endDate: new Date("2025-04-15")
}
];

export const RentHistory: Rent[] = [
    {
        rentId: 201,
        car: {
            carId: 1,
            carName: "Toyota Camry",
            fuelTypeName: "Бензин",
            carClassName: "Седан",
            carTransmissioName: "Автоматическая",
            carImage: "assets/1.jpg",
            cost: 2800,
            deposit: 14000
        },
        startDate: new Date("2025-03-01"),
        endDate: new Date("2025-03-05")
    },
    {
        rentId: 202,
        car: {
            carId: 2,
            carName: "Honda Accord",
            fuelTypeName: "Бензин",
            carClassName: "Седан",
            carTransmissioName: "Механическая",
            carImage: "assets/2.jpg",
            cost: 2800,
            deposit: 14000
        },
        startDate: new Date("2025-02-10"),
        endDate: new Date("2025-02-15")
    },
    {
        rentId: 203,
        car: {
            carId: 5,
            carName: "BMW 5 Series",
            fuelTypeName: "Бензин",
            carClassName: "Седан",
            carTransmissioName: "Автоматическая",
            carImage: "assets/5.jpg",
            cost: 2800,
            deposit: 14000
        },
        startDate: new Date("2025-01-20"),
        endDate: new Date("2025-01-25")
    },
    {
        rentId: 204,
        car: {
            carId: 4,
            carName: "Hyundai Tucson",
            fuelTypeName: "Газ",
            carClassName: "Кроссовер",
            carTransmissioName: "Автоматическая",
            carImage: "assets/4.jpg",
            cost: 2800,
            deposit: 14000
        },
        startDate: new Date("2025-03-10"),
        endDate: new Date("2025-03-15")
    }
];
