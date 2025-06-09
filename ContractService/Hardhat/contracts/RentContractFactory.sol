// SPDX-License-Identifier: MIT
pragma solidity ^0.8.20;

import "./RentContract.sol";
import "@openzeppelin/contracts/proxy/Clones.sol";

contract RentContractFactory {
    address public owner;

    mapping(uint => address) public implementations; // version => logic address
    uint public latestVersion;
    
struct RentInitParams {
    address renter;
    address company;
    address arbiter;
    uint256 deposit;
    uint256 rentAmount;
    uint256 startTime;
    uint256 endTime;
}

    RentContract[] public allContracts;
    mapping(address => RentContract[]) public contractsByRenter;
    mapping(address => RentContract[]) public contractsByCompany;

    event ImplementationAdded(uint version, address implementation);
    event ContractCreated(address contractAddress, address renter, address company, uint version);
    event CloneInitialized(bool success);

    modifier onlyOwner() {
        require(msg.sender == owner, "Only owner");
        _;
    }

    constructor() {
        owner = msg.sender;
    }

    function addImplementation(uint version, address implementation) external onlyOwner {
        require(implementation != address(0), "Invalid implementation address");
        require(implementations[version] == address(0), "Version already exists");

        implementations[version] = implementation;
        if (version > latestVersion) {
            latestVersion = version;
        }

        emit ImplementationAdded(version, implementation);
    }


function createRentContract(
    uint version,
    RentInitParams calldata params
) external returns (address) {
    address implementation = implementations[version];
    require(implementation != address(0), "Unknown version");

    address clone = Clones.clone(implementation);

   (bool success, ) = clone.call(
    abi.encodeWithSignature(
        "initialize(address,address,address,uint256,uint256,uint256,uint256)",
        params.renter,
        params.company,
        params.arbiter,
        params.deposit,
        params.rentAmount,
        params.startTime,
        params.endTime
    )
);

    emit CloneInitialized(success);
    require(success, "Initialization failed");

    RentContract rentContract = RentContract(clone);
    allContracts.push(rentContract);
    contractsByRenter[params.renter].push(rentContract);
    contractsByCompany[params.company].push(rentContract);

    emit ContractCreated(clone, params.renter, params.company, version);
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


