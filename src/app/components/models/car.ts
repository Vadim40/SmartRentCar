export interface Car {
    carId: number;
    carName: string; 
    fuelTypeName: string; 
    carClassName: string; 
    carTransmissioName: string; 
    carImages: string[]; 
    cost: number; 
    deposit: number; 
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
        carClassName: "Седан",
        carTransmissioName: "Автоматическая",
        carImages: ["assets/1.jpg", "assets/2.jpg", "assets/3.jpg", "assets/1.jpg"],
        cost: 3000,
        deposit: 15000,
        seats: 5,
        airConditioning: true,
        gps: true,
        modelYear: 2022
    },
    {
        carId: 2,
        carName: "Honda Accord",
        fuelTypeName: "Бензин",
        carClassName: "Седан",
        carTransmissioName: "Механическая",
        carImages: ["assets/2.jpg", "assets/4.jpg", "assets/5.jpg"],
        cost: 2800,
        deposit: 14000,
        seats: 5,
        airConditioning: true,
        gps: false,
        modelYear: 2021
    },
    {
        carId: 3,
        carName: "Ford Focus",
        fuelTypeName: "Дизель",
        carClassName: "Хэтчбек",
        carTransmissioName: "Автоматическая",
        carImages: ["assets/3.jpg", "assets/1.jpg", "assets/4.jpg"],
        cost: 2500,
        deposit: 12000,
        seats: 5,
        airConditioning: true,
        gps: false,
        modelYear: 2020
    },
    {
        carId: 4,
        carName: "Hyundai Tucson",
        fuelTypeName: "Газ",
        carClassName: "Кроссовер",
        carTransmissioName: "Автоматическая",
        carImages: ["assets/4.jpg", "assets/2.jpg", "assets/5.jpg"],
        cost: 4000,
        deposit: 20000,
        seats: 7,
        airConditioning: true,
        gps: true,
        modelYear: 2023
    },
    {
        carId: 5,
        carName: "BMW 5 Series",
        fuelTypeName: "Бензин",
        carClassName: "Седан",
        carTransmissioName: "Автоматическая",
        carImages: ["assets/5.jpg", "assets/3.jpg", "assets/1.jpg"],
        cost: 5000,
        deposit: 25000,
        seats: 5,
        airConditioning: true,
        gps: true,

        modelYear: 2022
    }
];

