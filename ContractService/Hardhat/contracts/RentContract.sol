// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

contract RentContract {
    address public renter;
    address public company;
    address public arbiter;

    uint256 public deposit;
    uint256 public rentAmount;

    uint256 public startTime;
    uint256 public endTime;
    bool public companyConfirmedStart;
    bool public renterConfirmedStart;

    bool public rentalStarted;
    bool public fundsDeposited;
    bool public initialized;
    bool public completed;

    bool public renterApprovedCompletion;
    bool public companyApprovedCompletion;

    bool public earlyTerminationRequested;
    bool public companyApprovedEarlyTermination;
    uint256 public usedAmount;

    bool public disputeRaised;

    event RentalCompleted(address indexed renter, address indexed company, uint256 paidToCompany, uint256 refundToRenter);
    event RentalCancelled(address indexed renter, uint256 refundAmount);
    event DepositDisputeResolved(uint256 toCompany, uint256 toRenter);
    event RentalStarted(address renter, address company);
    event DisputeRaised(address indexed by);

    modifier onlyRenter() {
        require(msg.sender == renter, "Only renter");
        _;
    }

    modifier onlyCompany() {
        require(msg.sender == company, "Only company");
        _;
    }

    modifier onlyArbiter() {
        require(msg.sender == arbiter, "Only arbiter");
        _;
    }

    function initialize(
        address _renter,
        address _company,
        address _arbiter,
        uint256 _deposit,
        uint256 _rentAmount,
        uint256 _startTime,
        uint256 _endTime
    ) external {
        require(!initialized, "Already initialized");

        renter = _renter;
        company = _company;
        arbiter = _arbiter;

        deposit = _deposit;
        rentAmount = _rentAmount;
        startTime = _startTime;
        endTime = _endTime;

        initialized = true;
    }

    function depositFunds() external payable onlyRenter {
        require(initialized, "Not initialized");
        require(!fundsDeposited, "Funds already deposited");

        uint256 requiredAmount = deposit + rentAmount;
        require(msg.value >= requiredAmount, "Insufficient funds sent");

        uint256 refund = msg.value - requiredAmount;
        fundsDeposited = true;

        if (refund > 0) {
            (bool sent, ) = renter.call{value: refund}("");
            require(sent, "Refund failed");
        }
    }

    function cancelRental() external onlyRenter {
        require(!rentalStarted, "Already started");
        require(fundsDeposited, "No funds deposited");

        uint256 refund = deposit + rentAmount;
        fundsDeposited = false;

        (bool sent, ) = renter.call{value: refund}("");
        require(sent, "Refund failed");

        emit RentalCancelled(renter, refund);
    }

    function companyConfirmStart() external onlyCompany {
        require(block.timestamp >= startTime, "Too early");
        require(fundsDeposited, "Funds not deposited");
        companyConfirmedStart = true;
        _checkRentalStarted();
    }

    function renterConfirmStart() external onlyRenter {
        require(block.timestamp >= startTime, "Too early");
        require(fundsDeposited, "Funds not deposited");
        renterConfirmedStart = true;
        _checkRentalStarted();
    }

    function _checkRentalStarted() internal {
        if (companyConfirmedStart && renterConfirmedStart) {
            rentalStarted = true;
            emit RentalStarted(renter, company);
        }
    }

    function finishRentalEarly() external onlyRenter {
        require(rentalStarted, "Rental not started");
        require(!earlyTerminationRequested, "Already requested");
        require(!renterApprovedCompletion, "Already approved");
        require(block.timestamp >= startTime, "Too early");

        earlyTerminationRequested = true;
        renterApprovedCompletion = true;

        uint256 totalDays = (endTime - startTime) / 1 days;
        require(totalDays > 0, "Invalid rental period");

        uint256 pricePerDay = rentAmount / totalDays;
        uint256 usedTime = block.timestamp > endTime ? endTime - startTime : block.timestamp - startTime;
        uint256 usedDays = (usedTime + 1 days - 1) / 1 days;

        usedAmount = usedDays * pricePerDay;
        require(usedAmount <= rentAmount, "Overcharged");
    }

    function companyApproveEarlyTermination() external onlyCompany {
        require(earlyTerminationRequested, "Not requested");
        require(!companyApprovedEarlyTermination, "Already approved");
        companyApprovedEarlyTermination = true;
    }

    function renterApproveCompletion() external onlyRenter {
        require(rentalStarted, "Not started");
        require(!completed, "Already completed");
        renterApprovedCompletion = true;
        _checkCompletion();
    }

    function companyApproveCompletion() external onlyCompany {
        require(rentalStarted, "Not started");
        require(!completed, "Already completed");
        companyApprovedCompletion = true;
        _checkCompletion();
    }

    function _checkCompletion() internal {
        if (renterApprovedCompletion && companyApprovedCompletion) {
            completed = true;

            uint256 toCompany;
            uint256 toRenter;

            if (earlyTerminationRequested) {
                require(companyApprovedEarlyTermination, "Company must approve");

                toCompany = usedAmount;
                toRenter = rentAmount - usedAmount;

                if (toCompany > 0) {
                    (bool sent, ) = company.call{value: toCompany}("");
                    require(sent, "Payment to company failed");
                }

                if (toRenter > 0) {
                    (bool sent, ) = renter.call{value: toRenter}("");
                    require(sent, "Refund to renter failed");
                }
            } else {
                toCompany = rentAmount;
                (bool sent, ) = company.call{value: toCompany}("");
                require(sent, "Payment to company failed");
            }

            (bool depositSent, ) = renter.call{value: deposit}("");
            require(depositSent, "Deposit refund failed");

            fundsDeposited = false;

            emit RentalCompleted(renter, company, toCompany, toRenter + deposit);
        }
    }

    function raiseDispute() external {
        require(msg.sender == renter || msg.sender == company, "Unauthorized");
        require(rentalStarted, "Not started");
        require(!disputeRaised, "Already raised");

        disputeRaised = true;

        emit DisputeRaised(msg.sender);
    }

    function resolveDepositDispute(uint256 amountToCompany) external onlyArbiter {
        require(disputeRaised, "No dispute");
        require(fundsDeposited, "No funds");
        require(amountToCompany <= deposit, "Invalid amount");

        uint256 rentToCompany = rentAmount;
        if (earlyTerminationRequested) {
            rentToCompany = usedAmount;
        }

        if (rentToCompany > 0) {
            (bool sentRent, ) = company.call{value: rentToCompany}("");
            require(sentRent, "Rent transfer failed");
        }

        if (amountToCompany > 0) {
            (bool sentDeposit, ) = company.call{value: amountToCompany}("");
            require(sentDeposit, "Deposit transfer failed");
        }

        uint256 refundToRenter = deposit - amountToCompany;
        if (refundToRenter > 0) {
            (bool sentRefund, ) = renter.call{value: refundToRenter}("");
            require(sentRefund, "Refund to renter failed");
        }

        fundsDeposited = false;

        emit DepositDisputeResolved(amountToCompany, refundToRenter);
    }

    receive() external payable {}
}
