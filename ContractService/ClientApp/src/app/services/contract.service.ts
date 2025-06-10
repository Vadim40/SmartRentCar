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

  private apiUrl = environment.apiUrl + "/rental"; // обычно пишем с маленькой буквы

  constructor(private httpClient: HttpClient) {
    super(httpClient);
  }

  getRentals(filter: FilterToRents): Observable<Rental[]> {
    let params = new HttpParams();

    if (filter.startDate && filter.endDate) {
      const startLocal = format(filter.startDate, "yyyy-MM-dd'T'HH:mm:ss");
      const endLocal = format(filter.endDate, "yyyy-MM-dd'T'HH:mm:ss");

      params = params.set('startDate', startLocal);
      params = params.set('endDate', endLocal);
    }

    if (filter.carName) {
      params = params.set('carName', filter.carName);
    }

    if (filter.rentalStatuses && filter.rentalStatuses[0] !== 0) {
      filter.rentalStatuses.forEach(status => {
        params = params.append('rentalStatuses', status);
      });
    }

    const url = `${this.apiUrl}/filter`;
    return this.sendRequest(url, 'GET', params);
  }

  getRentalStatuses(): Observable<RentalStatus[]> {
    const url = `${this.apiUrl}/statuses`;
    return this.sendRequest(url, 'GET');
  }

  approveRental(rentalId: number): Observable<void> {
    // Подтверждение аренды — лучше делать через status или action
    const url = `${this.apiUrl}/${rentalId}/approve`;
    return this.sendRequest(url, 'PUT');
  }

  disputeRental(rentalId: number): Observable<void> {
    const url = `${this.apiUrl}/${rentalId}/dispute`;
    return this.sendRequest(url, 'PUT');
  }

  confirmStart(rentalId: number): Observable<void> {
    const url = `${this.apiUrl}/${rentalId}/confirm-start`;
    return this.sendRequest(url, 'PUT');
  }
  confirmEarlyEnd(rentalId: number): Observable<void> {
    const url = `${this.apiUrl}/${rentalId}/confirm-early-end`;
    return this.sendRequest(url, 'PUT');
  }
  
  confirmCompletion(rentalId: number): Observable<void> {
    const url = `${this.apiUrl}/${rentalId}/confirm-completion`;
    return this.sendRequest(url, 'PUT');
  }
  
  sendToArbitration(rentalId: number): Observable<void> {
    const url = `${this.apiUrl}/${rentalId}/send-to-arbitration`;
    return this.sendRequest(url, 'PUT');
  }
  
 
  

}
