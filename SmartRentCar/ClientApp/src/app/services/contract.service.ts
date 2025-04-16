import { Injectable } from '@angular/core';
import { HttpService } from './config/http.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { RentContract, RentContractUpdate, RentContractСreate } from '../models/rentContract';
import { BrowserProvider, Contract, ethers, formatEther, JsonRpcProvider, LogDescription, parseUnits, Signer, TransactionResponse } from 'ethers';
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
  companyAddress = '0x7462A2FBF684f72AA35b89e72618Cc8622EB94a1';
  

  constructor(private httpClient: HttpClient) {
    super(httpClient);
  
    if (typeof window.ethereum !== "undefined") {
      this.provider = new ethers.BrowserProvider(window.ethereum);
    } else {  
      console.error(" MetaMask не найден!");
    }
  }
  
  
  private async initializeSigner() {
    try {
      this.signer = await this.provider.getSigner();
      this.contract = new ethers.Contract(this.contractAddress, this.abi, this.signer);
  
      const address = await this.signer.getAddress();
      console.log(" Адрес подключенного кошелька:", address);
    } catch (error) {
      console.error(" Ошибка при инициализации подписанта:", error);
    }
  }
  
  async connectWallet() {
    try {
      if (window.ethereum) {
        await window.ethereum.request({ method: 'eth_requestAccounts' });
        this.provider = new ethers.BrowserProvider(window.ethereum); 
        await this.initializeSigner();
        console.log(" MetaMask подключен!");
      } else {
        console.error(" MetaMask не найден!");
      }
    } catch (error) {
      console.error(" Ошибка при подключении кошелька:", error);
    }
  }
  
  async createRentContract (
    deposit: number,
    rentAmount: number,
    startTime: number,
    endTime: number,
    unlockDelayHours: number
  ): Promise<string> {
    try {
      if (!this.signer || !this.contract) {
        await this.connectWallet();
      }
  
      const renter = await this.signer.getAddress();
  
      const depositSmall = deposit / 10000;
    const rentAmountSmall = rentAmount / 10000;

    // Переводим уменьшенные суммы в wei (для использования в контракте)
    const depositWei = ethers.parseEther(depositSmall.toString());
    const rentAmountWei = ethers.parseEther(rentAmountSmall.toString());
  
      // Вызываем метод БЕЗ передачи value
      const tx = await this.contract['createRentContract'](
        renter,
        this.companyAddress,
        depositWei,
        rentAmountWei,
        startTime,
        endTime,
        unlockDelayHours,
        { gasLimit: parseUnits("50000", "wei") }
      );
  
      console.log(" ⏳ Транзакция отправлена:", tx.hash);
      const receipt = await tx.wait();
      console.log(" ✅ Контракт создан! Хеш:", receipt?.hash);
  
      const event = receipt?.logs?.find((log: LogDescription) => log.fragment?.name === "ContractCreated");
      const newContractAddress = event?.args?.contractAddress;
      if (newContractAddress) {
        console.log(" 📝 Новый контракт создан по адресу:", newContractAddress);
      }
  
      return newContractAddress;
    } catch (error) {
      console.error(" ❌ Ошибка при создании контракта:", error);
      throw error;
    }
  }
  
  


  getRentContractsByStatus(statusId: number): Observable<RentContract[]> {
    const url = `${this.apiUrl}/status/${statusId}`;
    return this.sendRequest(url, 'GET');
  }

  saveRentContract(contract: RentContractСreate): Observable<number> {
    const url = `${this.apiUrl}`;
    return this.sendRequest(url, 'POST', contract);
  }

  updateRentContract(contract: RentContractUpdate): Observable<void> {
    const url = `${this.apiUrl}`;
    return this.sendRequest(url, 'PUT', contract);
  }

  deleteRentContractById(contractId: number): Observable<void> {
    const url = `${this.apiUrl}/${contractId}`;
    return this.sendRequest(url, 'DELETE');
  }
}
