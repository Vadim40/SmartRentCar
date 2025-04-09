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

    bool public initialized;

    address public factory;

    constructor() {
        factory = msg.sender;
    }

    function initialize(
        address _renter,
        address _company,
        uint _deposit,
        uint _rentAmount,
        uint _startTime,
        uint _endTime,
        uint _unlockDelayHours
    ) external payable {
        require(!initialized, "Already initialized");
        initialized = true;

        renter = _renter;
        company = _company;
        deposit = _deposit;
        rentAmount = _rentAmount;
        startTime = _startTime;
        endTime = _endTime;
        
        uint delay = _unlockDelayHours > 0 ? _unlockDelayHours : 6;
        unlockTime = endTime + delay * 1 hours;

        require(msg.value == deposit + rentAmount, "Incorrect value");
    }
}
