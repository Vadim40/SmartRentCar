import { Injectable } from '@angular/core';
import { HttpService } from './config/http.service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CarService extends HttpService {

  private apiUrl = environment.apiUrl + "/Car";
  constructor( private httpClient : HttpClient) {
    super(httpClient)
   }

 
   
}
