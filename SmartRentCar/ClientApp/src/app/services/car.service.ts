import { Injectable } from '@angular/core';
import { HttpService } from './config/http.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { FilterToCars } from '../models/filtetToCars';
import { Observable } from 'rxjs';
import { Car } from '../models/car';
import { CarBrand, CarClass, CarFuelType, CarImage, CarTransmissionType } from '../models/carInfo';

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
      params = params.set('costMin', filter.costMin.toString());
    }
    if (filter.costMax) {
      params = params.set('costMax', filter.costMax.toString());
    }
    if (filter.depositMin) {
      params = params.set('depositMin', filter.depositMin.toString());
    }
    if (filter.depositMax) {
      params = params.set('depositMax', filter.depositMax.toString());
    }
    if (filter.carBrands && filter.carBrands?.[0] !=0) {
      params = params.set('carBrands', filter.carBrands.join(','));
    }
    if (filter.carClasses && filter.carClasses?.[0] !=0) {
      params = params.set('carClasses', filter.carClasses.join(','));
    }
    if (filter.carTransmission && filter.carTransmission != 0) {
      params = params.set('carTransimissionType', filter.carTransmission.toString());
    }
    if (filter.carFuel && filter.carFuel !=0) {
      params = params.set('carFuel', filter.carFuel.toString());
    }

    const url = `${this.apiUrl}/filter`;

    return this.sendRequest(url, 'GET', params);
  }

  getCar(carId: number): Observable<Car> {
    const url = `${this.apiUrl}/${carId}`;
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
