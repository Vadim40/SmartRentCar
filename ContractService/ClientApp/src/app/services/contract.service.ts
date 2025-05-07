import { Injectable } from '@angular/core';
import { HttpService } from './config/http.service';
import { environment } from '../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { FilterToRents } from '../models/filtetToCars';
import { Observable } from 'rxjs';
import { RentalStatus, Rental } from '../models/rental';
import { format } from 'date-fns';

@Injectable({
  providedIn: 'root'
})
export class ContractService extends HttpService {

  private apiUrl = environment.apiUrl + "/Rental"

  constructor(private httpClient: HttpClient) {
    super(httpClient);
  }

  getRentals(filter: FilterToRents): Observable<Rental[]> {
    let params = new HttpParams();

    if (filter.startDate != null && filter.endDate != null) {
      const startLocal = format(filter.startDate, "yyyy-MM-dd'T'HH:mm:ss");
      const endLocal = format(filter.endDate, "yyyy-MM-dd'T'HH:mm:ss");

      params = params.set('startDate', startLocal);
      params = params.set('endDate', endLocal);
    }
    const url = `${this.apiUrl}/filter`;
    return this.sendRequest(url, 'GET', params);
  }

  getRentalStatuses(): Observable<RentalStatus []>{
    const url =`${this.apiUrl}/statuses`;
    return this.sendRequest(url, 'GET');
  } 

  approveRental(rentalId: number): Observable<RentalStatus []>{
    const url =`${this.apiUrl}/approve/${rentalId}`;
    return this.sendRequest(url, 'PUT');
  } 
  disputeRental(rentalId: number): Observable<RentalStatus []>{
    const url =`${this.apiUrl}/dispute/${rentalId}`;
    return this.sendRequest(url, 'PUT');
  } 

}
