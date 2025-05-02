import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { debounceTime, Subject, Subscription } from 'rxjs';
import { CarBrand, CarClass } from 'src/app/models/carInfo';
import { FilterToRents } from 'src/app/models/filtetToCars';
import { RentContract, rentContracts } from 'src/app/models/rentContract';


@Component({
  selector: 'app-rent-list',
  templateUrl: './rent-list.component.html',
  styleUrls: ['./rent-list.component.css']
})
export class RentListComponent {

  private filterSubject = new Subject<FilterToRents>();
  filter: FilterToRents = {
    carClasses: [0],
    rentStatuses: [0]
  };
  carBrands: CarBrand[] = [];
  carClasses: CarClass[] = [];

  selectedCarClasses: number[] = [];
  selectedCarBrands: number[] = [];
  startDate: Date | undefined = undefined;
  endDate: Date | undefined = undefined;
  rentContracts : any
  selectedContract: RentContract | null = null;
  depositToHold: number = 0;
  holdReasonMessage: string= 'причина ' ;
  isHoldingDeposit = 0;
  constructor(
  ) {

  }
  private filterSubscription!: Subscription;



  ngOnInit() {
    this.filterSubscription = this.filterSubject.pipe(
      debounceTime(1000)
    ).subscribe(updatedFilter => {
    
    });
    this.getRents()
  }
  getRents(){
    this.rentContracts = rentContracts;

  }
  ngOnDestroy(): void {
    this.filterSubscription.unsubscribe();
  }

  holdDeposit(){
    this.isHoldingDeposit =1;
  }
  updateFilter() {
    this.filterSubject.next(this.filter);
  }

  openPopup(contract: RentContract) {
    console.log(contract)
    this.selectedContract = contract;
  }

  closePopup() {
    this.selectedContract = null;
    this.isHoldingDeposit = 0;
    
  }

  approveInspection() {
    if(this.selectedContract)
    this.selectedContract.contractStatusId = 7;
  }

  initiateDepositDispute() {
  
  }

  sendMessage() {
    
  }

  refundDeposit() {
   
  }
  stopPropagation(event: Event) {
    event.stopPropagation();
  }
  onCarClassSelectionChange(event: any) {
    if (this.filter.carClasses?.includes(0) && this.selectedCarClasses.length > 0 && !this.selectedCarClasses.includes(0)) {
      this.filter.carClasses = [0];
      this.selectedCarClasses = [0];
    } else {
      this.filter.carClasses = this.filter.carClasses?.filter((value: number) => value !== 0);
      this.selectedCarClasses = this.filter.carClasses || []
    }
    this.updateFilter();
  }

  onRentStatusSelectionChange(event: any) {

    if (this.filter.rentStatuses?.includes(0) && this.selectedCarBrands.length > 0 && !this.selectedCarBrands.includes(0)) {
      this.filter.rentStatuses = [0];
      this.selectedCarBrands = [0];
    }
    else {
      this.filter.rentStatuses = this.filter.rentStatuses?.filter((value: number) => value !== 0);
      this.selectedCarBrands = this.filter.rentStatuses || []
    }
    this.updateFilter();
  }
  onStartDateChange(event: Date) {
    this.startDate = event;
    this.checkDateAndUpdateFilter();
  }


  onEndDateChange(event: Date) {
    this.endDate = event;
    this.checkDateAndUpdateFilter();

  }
  private checkDateAndUpdateFilter() {
    if (this.startDate != null && this.endDate != null) {
      this.filter.startDate = this.startDate;
      this.filter.endDate = this.endDate;
      console.log(this.filter);
      this.updateFilter();
    }
  }
  onSearchChange(event: any) {
    this.filter.carName = event.value
    this.updateFilter();
  }

}
