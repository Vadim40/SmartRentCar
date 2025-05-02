import { Injectable } from '@angular/core';
import { HttpService } from './config/http.service';
import { environment } from '../environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { FilterToRents } from '../models/filtetToCars';
import { Observable } from 'rxjs';
import { RentContract } from '../models/rentContract';
import { format } from 'date-fns';

@Injectable({
  providedIn: 'root'
})
export class ContractService extends HttpService {

  private apiUrl = environment.apiUrl + "/ToDo"

  constructor(private httpClient: HttpClient) {
    super(httpClient);
  }

  getRentContracts(filter: FilterToRents): Observable<RentContract[]> {
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
}
