export interface CarBrand {
    carBrandId: number;
    name: string;
}

export interface CarClass {
    carClassId: number;
    name: string;
}
export interface CarFuelType {
    carFuelTypeId: number;
    name: string;
}
export interface CarTransmission {
    carTransmissionId: number;
    name: string;
}
export interface CarImage {
    carImageId: number;
    carId: number;
    imageDate: Blob;
}
