import { Injectable } from '@angular/core';
import { HttpService } from './config/http.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { FilterToCars } from '../models/filtetToCars';
import { Observable } from 'rxjs';
import { Car, CarBooking } from '../models/car';
import { CarBrand, CarClass, CarFuelType, CarImage, CarTransmissionType } from '../models/carInfo';
import { format } from "date-fns";

@Injectable({
  providedIn: 'root'
})
export class CarService extends HttpService {

  private apiUrl = environment.apiUrl + "/Car";
  constructor(private httpClient: HttpClient) {
    super(httpClient)
  }


  getCars(filter: FilterToCars): Observable<Car[]> {
    let params = new HttpParams();

    if (filter.costMin) {
      params = params.set('costMin', filter.costMin);
    }
    if (filter.costMax) {
      params = params.set('costMax', filter.costMax);
    }
    if (filter.depositMin) {
      params = params.set('depositMin', filter.depositMin);
    }
    if (filter.depositMax) {
      params = params.set('depositMax', filter.depositMax);
    }
    if (filter.carBrands && filter.carBrands?.[0] !=0) {
      filter.carBrands.forEach(carBrand =>{
        params = params.append('carBrands', carBrand);
      })
    }
    if (filter.carClasses && filter.carClasses?.[0] !=0) {
      filter.carClasses.forEach(carClass =>{
        params = params.append('carClasses', carClass);
      })
     
    }
    if (filter.carTransmission && filter.carTransmission != 0) {
      params = params.set('carTransmission', filter.carTransmission.toString());
    }
    if (filter.carFuel && filter.carFuel !=0) {
      params = params.set('carFuel', filter.carFuel.toString());
    }


    if (filter.startDate != null && filter.endDate != null) {
        const startLocal = format(filter.startDate, "yyyy-MM-dd'T'HH:mm:ss");
        const endLocal = format(filter.endDate, "yyyy-MM-dd'T'HH:mm:ss");
    
        params = params.set('startDate', startLocal);
        params = params.set('endDate', endLocal);
    }
    
  

    const url = `${this.apiUrl}/filter`;
    console.log(params)
    return this.sendRequest(url, 'GET', params);
  }

  getCar(carId: number): Observable<Car> {
    const url = `${this.apiUrl}/${carId}`;
    return this.sendRequest(url, 'GET')
  }

  getCarBookings(carId: number): Observable<CarBooking []> {
    const url = `${this.apiUrl}/${carId}/bookings`;
    return this.sendRequest(url, 'GET')
  }


  getFuelTypes(): Observable<CarFuelType[]> {
    const url = `${this.apiUrl}/fuel-types`;
    return this.sendRequest(url, 'GET')
  }
  getTransmissionTypes(): Observable<CarTransmissionType[]> {
    const url = `${this.apiUrl}/transmissions`;
    return this.sendRequest(url, 'GET')
  }
  getCarClasses(): Observable<CarClass[]> {
    const url = `${this.apiUrl}/classes`;
    return this.sendRequest(url, 'GET')
  }
  getBrands(): Observable<CarBrand[]> {
    const url = `${this.apiUrl}/brands`;
    return this.sendRequest(url, 'GET')
  }

  getFirstCarImage(carId: number): Observable<CarImage> {
    const url = `${this.apiUrl}/${carId}/image`;
    return this.sendRequest(url, 'GET')
  }
  getCarImages(carId: number): Observable<CarImage[]> {
    const url = `${this.apiUrl}/${carId}/images`;
    return this.sendRequest(url, 'GET')
  }
}
