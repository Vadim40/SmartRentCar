// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

contract RentContract {
    address public renter;
    address public company;

    uint public deposit; // Залог
    uint public rentAmount; // Вся сумма аренды

    uint public startTime;
    uint public endTime;
    uint public unlockTime; // Время, после которого можно вернуть залог

    bool public rentalStarted;
    bool public rentalCompleted;
    bool public depositFrozen;
    bool public depositReturned;

    constructor(
        address _renter,
        address _company,
        uint _deposit,
        uint _rentAmount,
        uint _startTime,
        uint _endTime,
        uint _unlockDelayHours
    ) payable {
        require(msg.value == _deposit + _rentAmount, "Incorrect funds");

        renter = _renter;
        company = _company;
        deposit = _deposit;
        rentAmount = _rentAmount;
        startTime = _startTime;
        endTime = _endTime;
        unlockTime = endTime + (_unlockDelayHours * 1 hours);
    }

    modifier onlyRenter() {
        require(msg.sender == renter, "Only renter");
        _;
    }

    modifier onlyCompany() {
        require(msg.sender == company, "Only company");
        _;
    }

    // Отмена аренды до начала
    function cancelRental() external onlyRenter {
        require(block.timestamp < startTime, "Rental already started");
        require(!rentalStarted, "Rental already marked as started");

        rentalCompleted = true;
        depositReturned = true;

        payable(renter).transfer(deposit + rentAmount);
    }

    // Пометка аренды как начатой (можно вызвать оффчейн или компанией)
    function markRentalStarted() external onlyCompany {
        require(block.timestamp >= startTime, "Too early");
        rentalStarted = true;
    }

    // Завершение аренды — часть суммы уходит компании, залог замораживается
    function finishRentalEarly(uint usedDays, uint pricePerDay) external onlyRenter {
        require(rentalStarted && !rentalCompleted, "Invalid state");
        rentalCompleted = true;

        uint usedAmount = usedDays * pricePerDay;
        require(usedAmount <= rentAmount, "Invalid amount");

        payable(company).transfer(usedAmount);
        // Остаток пользователю
        uint refund = rentAmount - usedAmount;
        if (refund > 0) {
            payable(renter).transfer(refund);
        }
        // Депозит ждет своего срока для возврата
    }

    // Компания удерживает депозит или его часть
    function freezeDeposit() external onlyCompany {
        require(rentalCompleted && !depositReturned, "Not eligible");
        depositFrozen = true;
    }

    function releaseDeposit(uint amountToCompany) external onlyCompany {
        require(depositFrozen && !depositReturned, "Cannot release");

        depositReturned = true;
        if (amountToCompany > 0) {
            require(amountToCompany <= deposit, "Too much");
            payable(company).transfer(amountToCompany);
        }

        if (deposit > amountToCompany) {
            payable(renter).transfer(deposit - amountToCompany);
        }
    }

    // Автоматический возврат депозита после задержки
    function returnDepositIfNoDispute() external {
        require(rentalCompleted && !depositReturned && block.timestamp >= unlockTime, "Not yet or frozen");
        require(!depositFrozen, "Deposit frozen");

        depositReturned = true;
        payable(renter).transfer(deposit);
    }
}
