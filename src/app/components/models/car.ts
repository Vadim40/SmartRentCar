export interface Car {
    carId: number;
    carName: string;
    fuelTypeName: string;
    carClassName: string;
    carTransmissioName: string;
    carImage: string;
    cost: number;
    deposit: number;
}

export const cars: Car[] = [
    {
        carId: 1,
        carName: "Toyota Camry",
        fuelTypeName: "Бензин",
        carClassName: "Седан",
        carTransmissioName: "Автоматическая",
        carImage: "assets/1.jpg",
        cost: 3000,
        deposit: 15000
    },
    {
        carId: 2,
        carName: "Honda Accord",
        fuelTypeName: "Бензин",
        carClassName: "Седан",
        carTransmissioName: "Механическая",
        carImage: "assets/2.jpg",
        cost: 2800,
        deposit: 14000
    },
    {
        carId: 3,
        carName: "Ford Focus",
        fuelTypeName: "Дизель",
        carClassName: "Хэтчбек",
        carTransmissioName: "Автоматическая",
        carImage: "assets/3.jpg",
        cost: 2500,
        deposit: 12000
    },
    {
        carId: 4,
        carName: "Hyundai Tucson",
        fuelTypeName: "Газ",
        carClassName: "Кроссовер",
        carTransmissioName: "Автоматическая",
        carImage: "assets/4.jpg",
        cost: 4000,
        deposit: 20000
    },
    {
        carId: 5,
        carName: "BMW 5 Series",
        fuelTypeName: "Бензин",
        carClassName: "Седан",
        carTransmissioName: "Автоматическая",
        carImage: "assets/5.jpg",
        cost: 5000,
        deposit: 25000
    }
];
