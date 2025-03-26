import { Injectable } from '@angular/core';
import { HttpService } from './config/http.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { RentContract } from '../models/rentContract';

@Injectable({
  providedIn: 'root'
})
export class RentContractService extends HttpService {
  private apiUrl = environment.apiUrl + "/RentContract";

  constructor(private httpClient: HttpClient) {
    super(httpClient);
  }

  getRentContractsByStatus(userId: number, statusId: number): Observable<RentContract[]> {
    const url = `${this.apiUrl}/status/${userId}/${statusId}`;
    return this.sendRequest(url, 'GET');
  }

  saveRentContract(contract: RentContract): Observable<number> {
    const url = `${this.apiUrl}`;
    return this.sendRequest(url, 'POST', contract);
  }

  updateRentContract(contract: RentContract): Observable<void> {
    const url = `${this.apiUrl}`;
    return this.sendRequest(url, 'PUT', contract);
  }

  deleteRentContractById(contractId: number): Observable<void> {
    const url = `${this.apiUrl}/${contractId}`;
    return this.sendRequest(url, 'DELETE');
  }
}
