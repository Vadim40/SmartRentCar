import { Injectable } from '@angular/core';
import { HttpService } from './config/http.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { RentContract } from '../models/rentContract';
import { BrowserProvider, Contract, ethers, JsonRpcProvider, Signer } from 'ethers';
import { abi, contractAddress } from '../models/contractInfo';

declare global {
  interface Window {
    ethereum?: any;
  }
}

@Injectable({
  providedIn: 'root'
})
export class RentContractService extends HttpService {
  private apiUrl = environment.apiUrl + "/RentContract";

  private provider!: BrowserProvider;
  private signer!: Signer;
  private contract!: Contract;
  private abi = abi;
  private contractAddress = contractAddress;
  companyAddress = '0xb885A33C4cbe8E4F25dFa7e99b079eD3E6992b6D';
  

  constructor(private httpClient: HttpClient) {
    super(httpClient);
  
    if (typeof window.ethereum !== "undefined") {
      this.provider = new ethers.BrowserProvider(window.ethereum);
    } else {  
      console.error("❌ MetaMask не найден!");
    }
  }
  
  
  private async initializeSigner() {
    try {
      this.signer = await this.provider.getSigner();
      this.contract = new ethers.Contract(this.contractAddress, this.abi, this.signer);
  
      const address = await this.signer.getAddress();
      console.log("👛 Адрес подключенного кошелька:", address);
    } catch (error) {
      console.error("❌ Ошибка при инициализации подписанта:", error);
    }
  }
  
  async connectWallet() {
    try {
      if (window.ethereum) {
        await window.ethereum.request({ method: 'eth_requestAccounts' });
        this.provider = new ethers.BrowserProvider(window.ethereum); // безопаснее повторно проинициализировать
        await this.initializeSigner();
        console.log("✅ MetaMask подключен!");
      } else {
        console.error("❌ MetaMask не найден!");
      }
    } catch (error) {
      console.error("❌ Ошибка при подключении кошелька:", error);
    }
  }
  
  
  async createRentContract(deposit: number, rentAmount: number, startTime: number, endTime: number, unlockDelayHours: number) {
    try {
      const renter = await this.signer.getAddress(); // Берем адрес арендатора из MetaMask
      const tx = await this.contract['createRentContract'](
        renter, this.companyAddress, deposit, rentAmount, startTime, endTime, unlockDelayHours,
        { value: deposit + rentAmount }
      );
  
      await tx.wait();
      console.log(" Контракт создан:", tx);
    } catch (error) {
      console.error(" Ошибка при создании контракта:", error);
    }
  }
  

  getRentContractsByStatus(statusId: number): Observable<RentContract[]> {
    const url = `${this.apiUrl}/status/${statusId}`;
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
