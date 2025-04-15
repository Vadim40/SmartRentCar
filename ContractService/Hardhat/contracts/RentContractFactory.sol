// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import "./RentContract.sol";
import "@openzeppelin/contracts/proxy/Clones.sol";

contract RentContractFactory {
    address public owner;
    address public implementation;

    RentContract[] public allContracts;
    mapping(address => RentContract[]) public contractsByRenter;
    mapping(address => RentContract[]) public contractsByCompany;

    event ContractCreated(address contractAddress, address renter, address company);
    event FactoryDeployed(address deployer, address implementationAddress);
    event CloneCreated(address cloneAddress);
    event InitCalled(bool success);

    constructor(address _implementation) {
        owner = msg.sender;
        implementation = _implementation;

        emit FactoryDeployed(owner, implementation);
    }

    function createRentContract(
        address _renter,
        address _company,
        uint _deposit,
        uint _rentAmount,
        uint _startTime,
        uint _endTime,
        uint _unlockDelayHours
    ) external returns (address) {
        address clone = Clones.clone(implementation);
        emit CloneCreated(clone);

        (bool success, ) = clone.call(
            abi.encodeWithSignature(
                "initialize(address,address,uint256,uint256,uint256,uint256,uint256)",
                _renter,
                _company,
                _deposit,
                _rentAmount,
                _startTime,
                _endTime,
                _unlockDelayHours
            )
        );

        emit InitCalled(success);
        require(success, "Initialization failed");

        RentContract rentContract = RentContract(clone);
        allContracts.push(rentContract);
        contractsByRenter[_renter].push(rentContract);
        contractsByCompany[_company].push(rentContract);

        emit ContractCreated(clone, _renter, _company);
        return clone;
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
