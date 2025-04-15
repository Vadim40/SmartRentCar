// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

contract RentContract {
    address public renter;
    address public company;

    uint public deposit;
    uint public rentAmount;

    uint public startTime;
    uint public endTime;
    uint public unlockTime;

    bool public rentalStarted;
    bool public rentalCompleted;
    bool public depositFrozen;
    bool public depositReturned;
    bool public fundsDeposited;

    bool public initialized;

    constructor() {
        initialized = false;
    }

    function initialize(
        address _renter,
        address _company,
        uint _deposit,
        uint _rentAmount,
        uint _startTime,
        uint _endTime,
        uint _unlockDelayHours
    ) external {
        require(!initialized, "Already initialized");

        renter = _renter;
        company = _company;
        deposit = _deposit;
        rentAmount = _rentAmount;
        startTime = _startTime;
        endTime = _endTime;
        unlockTime = _endTime + (_unlockDelayHours * 1 hours);

        initialized = true;
    }

    modifier onlyRenter() {
        require(msg.sender == renter, "Only renter");
        _;
    }

    modifier onlyCompany() {
        require(msg.sender == company, "Only company");
        _;
    }

    function depositFunds() external payable onlyRenter {
        require(initialized, "Not initialized");
        require(!fundsDeposited, "Funds already deposited");
        require(msg.value == deposit + rentAmount, "Incorrect amount");

        fundsDeposited = true;
    }

    function cancelRental() external onlyRenter {
        require(block.timestamp < startTime, "Rental already started");
        require(!rentalStarted, "Already started");
        require(fundsDeposited, "No funds");

        rentalCompleted = true;
        depositReturned = true;

        payable(renter).transfer(deposit + rentAmount);
    }

    function markRentalStarted() external onlyCompany {
        require(block.timestamp >= startTime, "Too early");
        require(fundsDeposited, "Funds not deposited");
        rentalStarted = true;
    }

    function finishRentalEarly(uint usedDays, uint pricePerDay) external onlyRenter {
        require(rentalStarted && !rentalCompleted, "Invalid state");

        rentalCompleted = true;

        uint usedAmount = usedDays * pricePerDay;
        require(usedAmount <= rentAmount, "Too much");

        payable(company).transfer(usedAmount);

        uint refund = rentAmount - usedAmount;
        if (refund > 0) {
            payable(renter).transfer(refund);
        }
    }

    function finalizeRental() external onlyCompany {
        require(rentalStarted && !rentalCompleted, "Invalid state");

        rentalCompleted = true;
        payable(company).transfer(rentAmount);
    }

    function freezeDeposit() external onlyCompany {
        require(rentalCompleted && !depositReturned, "Not eligible");
        depositFrozen = true;
    }

    function releaseDeposit(uint amountToCompany) external onlyCompany {
        require(depositFrozen && !depositReturned, "Not allowed");

        depositReturned = true;

        if (amountToCompany > 0) {
            require(amountToCompany <= deposit, "Too much");
            payable(company).transfer(amountToCompany);
        }

        if (deposit > amountToCompany) {
            payable(renter).transfer(deposit - amountToCompany);
        }
    }

    function returnDepositIfNoDispute() external {
        require(rentalCompleted, "Rental not completed");
        require(!depositReturned, "Already returned");
        require(!depositFrozen, "Deposit frozen");
        require(block.timestamp >= unlockTime, "Too early");

        depositReturned = true;
        payable(renter).transfer(deposit);
    }
}
