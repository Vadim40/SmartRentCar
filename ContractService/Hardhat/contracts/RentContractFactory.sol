// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import "./RentContract.sol";

contract RentContractFactory {
    address public owner;

    // Все созданные контракты
    RentContract[] public allContracts;

    // Индексы контрактов по арендатору и компании
    mapping(address => RentContract[]) public contractsByRenter;
    mapping(address => RentContract[]) public contractsByCompany;

    event ContractCreated(address contractAddress, address renter, address company);

    constructor() {
        owner = msg.sender;
    }

    function createRentContract(
        address _renter,
        address _company,
        uint _deposit,
        uint _rentAmount,
        uint _startTime,
        uint _endTime,
        uint _unlockDelayHours
    ) external payable returns (address) {
        require(msg.value == _deposit + _rentAmount, "Incorrect value");

        RentContract rentContract = (new RentContract){value: msg.value}(
            _renter,
            _company,
            _deposit,
            _rentAmount,
            _startTime,
            _endTime,
            _unlockDelayHours
        );

        allContracts.push(rentContract);
        contractsByRenter[_renter].push(rentContract);
        contractsByCompany[_company].push(rentContract);

        emit ContractCreated(address(rentContract), _renter, _company);
        return address(rentContract);
    }

    function getContractsByRenter(address renter) external view returns (RentContract[] memory) {
        return contractsByRenter[renter];
    }

    function getContractsByCompany(address company) external view returns (RentContract[] memory) {
        return contractsByCompany[company];
    }

    function getAllContracts() external view returns (RentContract[] memory) {
        return allContracts;
    }
}
