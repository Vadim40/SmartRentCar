import { Injectable } from '@angular/core';
import { HttpService } from './config/http.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map, Observable } from 'rxjs';
import { RentContract, RentContractUpdate, RentContractСreate } from '../models/rentContract';
import { BrowserProvider, Contract, ethers, formatEther, JsonRpcProvider, LogDescription, parseUnits, Signer, TransactionResponse } from 'ethers';
import { COMPANY_ADDRESS, CONTRACT_ABI, CONTRACT_FACTORY_ABI, CONTRACT_FACTORY_ADDRESS } from '../models/contractInfo';

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
  private contractFactory!: Contract;
  private contractFactoryAbi = CONTRACT_FACTORY_ABI;
  private contractFactoryAddress = CONTRACT_FACTORY_ADDRESS;
  private contract!: Contract;
  private contractAbi = CONTRACT_ABI;
  private contractAddress = '';

  private companyAddress = COMPANY_ADDRESS;


  constructor(private httpClient: HttpClient) {
    super(httpClient);

    if (typeof window.ethereum !== "undefined") {
      this.provider = new ethers.BrowserProvider(window.ethereum);
    } else {
      console.error(" MetaMask не найден!");
    }
  }


   async initializeSigner() {
    try {
      this.signer = await this.provider.getSigner();
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
      } else {
        console.error(" MetaMask не найден!");
      }
    } catch (error) {
      console.error(" Ошибка при подключении кошелька:", error);
    }
  }

  async createRentContract(
    deposit: number,
    rentAmount: number,
    startTime: number,
    endTime: number,
    unlockDelayHours: number
  ): Promise<string> {
    try {
      if (!this.contractFactory) {
        await this.connectWallet();
        await this.initializeSigner();
        this.contractFactory = new ethers.Contract(this.contractFactoryAddress, this.contractFactoryAbi, this.signer);
      }

      const renter = await this.signer.getAddress();

      const depositSmall = deposit / 10000000;
      const rentAmountSmall = rentAmount / 10000000;

      const depositWei = ethers.parseEther(depositSmall.toString());
      const rentAmountWei = ethers.parseEther(rentAmountSmall.toString());

      //  метод БЕЗ передачи value
      const tx = await this.contractFactory['createRentContract'](
        renter,
        this.companyAddress,
        depositWei,
        rentAmountWei,
        startTime,
        endTime,
        unlockDelayHours,
        {gasLimit: 500000 }
      );

      console.log("  Транзакция отправлена:", tx.hash);
      const receipt = await tx.wait();
      console.log(" Хеш:", receipt?.hash);

      const event = receipt?.logs?.find((log: LogDescription) => log.fragment?.name === "ContractCreated");
      console.log(event?.args)
      const newContractAddress = event?.args?.contractAddress;
      if (newContractAddress) {
        console.log("  Адрес:", newContractAddress);
      }

      return newContractAddress;
    } catch (error) {
      console.error("  Ошибка при создании контракта:", error);
      throw error;
    }
  }


  async depositFunds(contractAddress: string, deposit: number, rentAmount: number) {
    try {
      this.contractAddress = contractAddress;
      if (!this.contract) {
        await this.connectWallet();
        await this.initializeSigner();
        this.contract = new ethers.Contract(this.contractAddress, this.contractAbi, this.signer);
      }


      const depositSmall = deposit / 10000000;
      const rentAmountSmall = rentAmount / 10000000;

      const depositWei = ethers.parseEther(depositSmall.toString());
      const rentAmountWei = ethers.parseEther(rentAmountSmall.toString());
      console.log(depositWei + rentAmountWei);
      const tx = await this.contract['depositFunds']({
        value: depositWei + rentAmountWei, 
        gasLimit: 500000 
    });

    } catch (error) {
      console.error("  Ошибка при создании контракта:", error);
      throw error;
    }

  }

  async cancelRent(contractAddress: string){
    try {
      this.contractAddress = contractAddress;
      if (!this.contract) {
        await this.connectWallet();
        await this.initializeSigner();
        this.contract = new ethers.Contract(this.contractAddress, this.contractAbi, this.signer);
      }
      const tx = await this.contract['cancelRental']({
        gasLimit: 500000 
    });
    } catch (error) {
      console.error("  Ошибка при создании контракта:", error);
      throw error;
    }
  }


  getRentContractsActive(): Observable<RentContract[]> {
    const url = `${this.apiUrl}/active`;
    return this.sendRequest(url, 'GET');
  }
  getRentContractsCompleted(): Observable<RentContract[]> {
    const url = `${this.apiUrl}/completed`;
    return this.sendRequest(url, 'GET');
  }
  saveRentContract(rentContract: RentContractСreate): Observable<number> {
    const url = `${this.apiUrl}`;
    return this.sendRequest<{ rentId: number }>(url, 'POST', rentContract).pipe(
      map(response => response.rentId)
    );
  }

  updateRentContract(rentContract: RentContractUpdate): Observable<void> {
    const url = `${this.apiUrl}`;
    return this.sendRequest(url, 'PUT', rentContract);
  }

  deleteRentContractById(contractId: number): Observable<void> {
    const url = `${this.apiUrl}/${contractId}`;
    return this.sendRequest(url, 'DELETE');
  }
}
