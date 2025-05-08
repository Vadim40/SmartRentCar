import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { debounceTime, Subject, Subscription } from 'rxjs';
import { CarBrand, CarClass } from 'src/app/models/carInfo';
import { DepositDispute } from 'src/app/models/depositDispute';
import { FilterToRents } from 'src/app/models/filtetToCars';
import { Rental, RentalStatus} from 'src/app/models/rental';
import { ContractService } from 'src/app/services/contract.service';
import { DepositService } from 'src/app/services/deposit.service';


@Component({
  selector: 'app-rent-list',
  templateUrl: './rent-list.component.html',
  styleUrls: ['./rent-list.component.css']
})
export class RentListComponent {

  private filterSubject = new Subject<FilterToRents>();
  filter: FilterToRents = {
    rentalStatuses: [0]
  };

  rentalStatuses: RentalStatus [] = []
  selectedRentalStatuses: number [] = []
  startDate: Date | undefined = undefined;
  endDate: Date | undefined = undefined;
  rentals: Rental [] = []
   selectedRental: Rental | null = null;
  depositToHold: number = 0;
  holdReasonMessage: string = 'причина ';
  isHoldingDeposit = 0;

  constructor
  (
    private contractService: ContractService,
    private depositService: DepositService
  ) {

  }
  private filterSubscription!: Subscription;



  ngOnInit() {
    this.filterSubscription = this.filterSubject.pipe(
      debounceTime(1000)
    ).subscribe(updatedFilter => {
      this.getRents(updatedFilter)
    });
    this.getRents(this.filter)
    this.getRentalStatuses()
  }
  getRents(filter: FilterToRents) {
    this.contractService.getRentals(filter).subscribe({
      next: (rentals: Rental[]) => {
        this.rentals = rentals;
        this.rentals.forEach(rental => {
          this.depositService.getDepositDisputeByRenalId(rental.rentalId).subscribe({
            next: (dispute: DepositDispute) =>{
                rental.depositDispute = dispute;
            }
          })
          
        });
      },
      error: (error ) =>{
        console.error('Ошибка загрузки аренд', error)
      }
    })

  }
  getRentalStatuses(){
    this.contractService.getRentalStatuses().subscribe({
      next: (statuses: RentalStatus[]) =>{
        this.rentalStatuses = statuses;
      },
      error: (error ) =>{
        console.error('Ошибка загрузки статусов', error)
      }
    })
  }
  ngOnDestroy(): void {
    this.filterSubscription.unsubscribe();
  }

  holdDeposit() {
    this.isHoldingDeposit = 1;
  }
  updateFilter() {
    this.filterSubject.next(this.filter);
  }

  openPopup(contract: Rental) {
    console.log(contract)
    this. selectedRental = contract;
  }

  closePopup() {
    this. selectedRental = null;
    this.isHoldingDeposit = 0;

  }

  approveInspection() {
    if (this. selectedRental)
      this. selectedRental.rentalStatusId = 7;
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


  onRentStatusSelectionChange(event: any) {

    if (this.filter.rentalStatuses?.includes(0) && this.rentalStatuses.length > 0 && !this.selectedRentalStatuses.includes(0)) {
      this.filter.rentalStatuses = [0];
      this.selectedRentalStatuses= [0];
    }
    else {
      this.filter.rentalStatuses = this.filter.rentalStatuses?.filter((value: number) => value !== 0);
      this.selectedRentalStatuses = this.filter.rentalStatuses || []
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
