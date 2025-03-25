export interface Car {
    carId: number;
    carName: string; 
    fuelTypeName: string; 
    className: string; 
    transmissioName: string; 
    carImages: string[]; 
    costPerDay: number; 
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
        className: "Седан",
        transmissioName: "Автоматическая",
        carImages: ["assets/1.jpg", "assets/2.jpg", "assets/3.jpg", "assets/1.jpg"],
        costPerDay: 3000,
        deposit: 15000,
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
        carImages: ["assets/2.jpg", "assets/4.jpg", "assets/5.jpg"],
        costPerDay: 2800,
        deposit: 14000,
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
        carImages: ["assets/3.jpg", "assets/1.jpg", "assets/4.jpg"],
        costPerDay: 2500,
        deposit: 12000,
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
        carImages: ["assets/4.jpg", "assets/2.jpg", "assets/5.jpg"],
        costPerDay: 4000,
        deposit: 20000,
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
        carImages: ["assets/5.jpg", "assets/3.jpg", "assets/1.jpg"],
        costPerDay: 5000,
        deposit: 25000,
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
