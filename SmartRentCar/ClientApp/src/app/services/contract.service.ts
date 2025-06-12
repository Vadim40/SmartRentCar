import { Injectable } from '@angular/core';
import { HttpService } from './config/http.service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map, Observable } from 'rxjs';
import { RentContract, RentContractUpdate, RentContractCreate } from '../models/rentContract';
import { BrowserProvider, Contract, ethers, LogDescription, Signer } from 'ethers';
import { ARBITER_ADDRESS, COMPANY_ADDRESS, CONTRACT_ABI, CONTRACT_FACTORY_ABI, CONTRACT_FACTORY_ADDRESS, CONTRACT_VERSION } from '../models/contractInfo';
import Decimal from 'decimal.js';

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
    deposit: Decimal,
    rentAmount: Decimal,
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

      const depositDecimal = deposit instanceof Decimal ? deposit : new Decimal(deposit);
      const rentAmountDecimal = rentAmount instanceof Decimal ? rentAmount : new Decimal(rentAmount);
      
      const depositSmall = depositDecimal.div(10000);
      const rentAmountSmall = rentAmountDecimal.div(10000);
      
      

      console.log("депозит", rentAmountSmall); 

      const depositWei = ethers.parseEther(depositSmall.toString());     
      const rentAmountWei = ethers.parseEther(rentAmountSmall.toString()); 

      const sum = depositWei + rentAmountWei; 
      console.log("Сумма в ETH:", ethers.formatEther(sum)); 

      
      const tx = await this.contractFactory['createRentContract'](
        CONTRACT_VERSION, 
        {
          renter: renter, 
          company: this.companyAddress, 
          arbiter: ARBITER_ADDRESS,
          deposit: depositWei, 
          rentAmount: rentAmountWei, 
          startTime: startTime, 
          endTime: endTime,  
          unlockDelayHours: unlockDelayHours 
        },
        { gasLimit: 500_000 }
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


      const depositSmall = deposit / 10000;        
      const rentAmountSmall = rentAmount / 10000;  

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

  async cancelRent(contractAddress: string) {
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
  async confirmStart(contractAddress: string): Promise<void> {
    try {
      this.contractAddress = contractAddress;
      if (!this.contract) {
        await this.connectWallet();
        await this.initializeSigner();
        this.contract = new ethers.Contract(this.contractAddress, this.contractAbi, this.signer);
      }
  
      const tx = await this.contract['renterConfirmStart']({
        gasLimit: 300_000
      });
      await tx.wait();
      console.log("Арендатор подтвердил начало аренды:", tx.hash);
    } catch (error) {
      console.error("Ошибка в renterConfirmStart:", error);
      throw error;
    }
  }

  async finishRentalEarly(contractAddress: string): Promise<void> {
    try {
      this.contractAddress = contractAddress;
      if (!this.contract) {
        await this.connectWallet();
        await this.initializeSigner();
        this.contract = new ethers.Contract(this.contractAddress, this.contractAbi, this.signer);
      }
  
      const tx = await this.contract['finishRentalEarly']({
        gasLimit: 300_000
      });
      await tx.wait();
      console.log("Арендатор запросил досрочное завершение аренды:", tx.hash);
    } catch (error) {
      console.error("Ошибка в finishRentalEarly:", error);
      throw error;
    }
  }
  async approveCompletion(contractAddress: string): Promise<void> {
    try {
      this.contractAddress = contractAddress;
      if (!this.contract) {
        await this.connectWallet();
        await this.initializeSigner();
        this.contract = new ethers.Contract(this.contractAddress, this.contractAbi, this.signer);
      }
  
      const tx = await this.contract['renterApproveCompletion']({
        gasLimit: 300_000
      });
      await tx.wait();
      console.log("Арендатор подтвердил завершение аренды:", tx.hash);
    } catch (error) {
      console.error("Ошибка в renterApproveCompletion:", error);
      throw error;
    }
  }
  async raiseDispute(contractAddress: string): Promise<void> {
    try {
      this.contractAddress = contractAddress;
      if (!this.contract) {
        await this.connectWallet();
        await this.initializeSigner();
        this.contract = new ethers.Contract(this.contractAddress, this.contractAbi, this.signer);
      }
  
      const tx = await this.contract['raiseDispute']({
        gasLimit: 300_000
      });
      await tx.wait();
      console.log("Спор поднят:", tx.hash);
    } catch (error) {
      console.error("Ошибка в raiseDispute:", error);
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
  saveRentContract(rentContract: RentContractCreate): Observable<number> {
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
