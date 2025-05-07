import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { HttpService } from './config/http.service';
import { environment } from '../environments/environment';
import { Observable } from 'rxjs';
import { DepositDispute, DisputeUpdate } from '../models/depositDispute';


@Injectable({
  providedIn: 'root'
})
export class DepositService extends HttpService {

  private apiUrl = environment.apiUrl + "/Deposit"
  
  constructor(private httpClient: HttpClient) {
    super(httpClient);
  }

  getDepositDispute(depositDisputeId : number) :Observable<DepositDispute>{
    const url = `${this.apiUrl}/${depositDisputeId}`;
    return this.sendRequest(url, 'GET');
  }
  getDepositDisputeByRenalId(rentalId : number) :Observable<DepositDispute>{
    const url = `${this.apiUrl}/rental/${rentalId}`;
    return this.sendRequest(url, 'GET');
  }
  updateDepositDispute(disputeUpdate: DisputeUpdate ) :Observable<DepositDispute>{
    const url = `${this.apiUrl}/update`;
    return this.sendRequest(url, 'PUT', disputeUpdate);
  }
}
