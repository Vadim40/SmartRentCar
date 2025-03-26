import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpService } from './config/http.service';

@Injectable({
  providedIn: 'root'
})

// а нужны ли мне компании чтобы сделать диплом, нет, только получается не будет 3 стороны, но  я не строю бизнес модель.
export class CompanyService extends HttpService {

    private apiUrl = environment.apiUrl + "/Company";
    constructor(private httpClient: HttpClient) {
      super(httpClient)
    }


}
