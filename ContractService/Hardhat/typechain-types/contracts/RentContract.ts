/* Autogenerated file. Do not edit manually. */
/* tslint:disable */
/* eslint-disable */
import type {
  BaseContract,
  BigNumberish,
  BytesLike,
  FunctionFragment,
  Result,
  Interface,
  AddressLike,
  ContractRunner,
  ContractMethod,
  Listener,
} from "ethers";
import type {
  TypedContractEvent,
  TypedDeferredTopicFilter,
  TypedEventLog,
  TypedListener,
  TypedContractMethod,
} from "../common";

export interface RentContractInterface extends Interface {
  getFunction(
    nameOrSignature:
      | "cancelRental"
      | "company"
      | "deposit"
      | "depositFrozen"
      | "depositFunds"
      | "depositReturned"
      | "endTime"
      | "finalizeRental"
      | "finishRentalEarly"
      | "freezeDeposit"
      | "fundsDeposited"
      | "initialize"
      | "initialized"
      | "markRentalStarted"
      | "releaseDeposit"
      | "rentAmount"
      | "rentalCompleted"
      | "rentalStarted"
      | "renter"
      | "returnDepositIfNoDispute"
      | "startTime"
      | "unlockTime"
  ): FunctionFragment;

  encodeFunctionData(
    functionFragment: "cancelRental",
    values?: undefined
  ): string;
  encodeFunctionData(functionFragment: "company", values?: undefined): string;
  encodeFunctionData(functionFragment: "deposit", values?: undefined): string;
  encodeFunctionData(
    functionFragment: "depositFrozen",
    values?: undefined
  ): string;
  encodeFunctionData(
    functionFragment: "depositFunds",
    values?: undefined
  ): string;
  encodeFunctionData(
    functionFragment: "depositReturned",
    values?: undefined
  ): string;
  encodeFunctionData(functionFragment: "endTime", values?: undefined): string;
  encodeFunctionData(
    functionFragment: "finalizeRental",
    values?: undefined
  ): string;
  encodeFunctionData(
    functionFragment: "finishRentalEarly",
    values: [BigNumberish, BigNumberish]
  ): string;
  encodeFunctionData(
    functionFragment: "freezeDeposit",
    values?: undefined
  ): string;
  encodeFunctionData(
    functionFragment: "fundsDeposited",
    values?: undefined
  ): string;
  encodeFunctionData(
    functionFragment: "initialize",
    values: [
      AddressLike,
      AddressLike,
      BigNumberish,
      BigNumberish,
      BigNumberish,
      BigNumberish,
      BigNumberish
    ]
  ): string;
  encodeFunctionData(
    functionFragment: "initialized",
    values?: undefined
  ): string;
  encodeFunctionData(
    functionFragment: "markRentalStarted",
    values?: undefined
  ): string;
  encodeFunctionData(
    functionFragment: "releaseDeposit",
    values: [BigNumberish]
  ): string;
  encodeFunctionData(
    functionFragment: "rentAmount",
    values?: undefined
  ): string;
  encodeFunctionData(
    functionFragment: "rentalCompleted",
    values?: undefined
  ): string;
  encodeFunctionData(
    functionFragment: "rentalStarted",
    values?: undefined
  ): string;
  encodeFunctionData(functionFragment: "renter", values?: undefined): string;
  encodeFunctionData(
    functionFragment: "returnDepositIfNoDispute",
    values?: undefined
  ): string;
  encodeFunctionData(functionFragment: "startTime", values?: undefined): string;
  encodeFunctionData(
    functionFragment: "unlockTime",
    values?: undefined
  ): string;

  decodeFunctionResult(
    functionFragment: "cancelRental",
    data: BytesLike
  ): Result;
  decodeFunctionResult(functionFragment: "company", data: BytesLike): Result;
  decodeFunctionResult(functionFragment: "deposit", data: BytesLike): Result;
  decodeFunctionResult(
    functionFragment: "depositFrozen",
    data: BytesLike
  ): Result;
  decodeFunctionResult(
    functionFragment: "depositFunds",
    data: BytesLike
  ): Result;
  decodeFunctionResult(
    functionFragment: "depositReturned",
    data: BytesLike
  ): Result;
  decodeFunctionResult(functionFragment: "endTime", data: BytesLike): Result;
  decodeFunctionResult(
    functionFragment: "finalizeRental",
    data: BytesLike
  ): Result;
  decodeFunctionResult(
    functionFragment: "finishRentalEarly",
    data: BytesLike
  ): Result;
  decodeFunctionResult(
    functionFragment: "freezeDeposit",
    data: BytesLike
  ): Result;
  decodeFunctionResult(
    functionFragment: "fundsDeposited",
    data: BytesLike
  ): Result;
  decodeFunctionResult(functionFragment: "initialize", data: BytesLike): Result;
  decodeFunctionResult(
    functionFragment: "initialized",
    data: BytesLike
  ): Result;
  decodeFunctionResult(
    functionFragment: "markRentalStarted",
    data: BytesLike
  ): Result;
  decodeFunctionResult(
    functionFragment: "releaseDeposit",
    data: BytesLike
  ): Result;
  decodeFunctionResult(functionFragment: "rentAmount", data: BytesLike): Result;
  decodeFunctionResult(
    functionFragment: "rentalCompleted",
    data: BytesLike
  ): Result;
  decodeFunctionResult(
    functionFragment: "rentalStarted",
    data: BytesLike
  ): Result;
  decodeFunctionResult(functionFragment: "renter", data: BytesLike): Result;
  decodeFunctionResult(
    functionFragment: "returnDepositIfNoDispute",
    data: BytesLike
  ): Result;
  decodeFunctionResult(functionFragment: "startTime", data: BytesLike): Result;
  decodeFunctionResult(functionFragment: "unlockTime", data: BytesLike): Result;
}

export interface RentContract extends BaseContract {
  connect(runner?: ContractRunner | null): RentContract;
  waitForDeployment(): Promise<this>;

  interface: RentContractInterface;

  queryFilter<TCEvent extends TypedContractEvent>(
    event: TCEvent,
    fromBlockOrBlockhash?: string | number | undefined,
    toBlock?: string | number | undefined
  ): Promise<Array<TypedEventLog<TCEvent>>>;
  queryFilter<TCEvent extends TypedContractEvent>(
    filter: TypedDeferredTopicFilter<TCEvent>,
    fromBlockOrBlockhash?: string | number | undefined,
    toBlock?: string | number | undefined
  ): Promise<Array<TypedEventLog<TCEvent>>>;

  on<TCEvent extends TypedContractEvent>(
    event: TCEvent,
    listener: TypedListener<TCEvent>
  ): Promise<this>;
  on<TCEvent extends TypedContractEvent>(
    filter: TypedDeferredTopicFilter<TCEvent>,
    listener: TypedListener<TCEvent>
  ): Promise<this>;

  once<TCEvent extends TypedContractEvent>(
    event: TCEvent,
    listener: TypedListener<TCEvent>
  ): Promise<this>;
  once<TCEvent extends TypedContractEvent>(
    filter: TypedDeferredTopicFilter<TCEvent>,
    listener: TypedListener<TCEvent>
  ): Promise<this>;

  listeners<TCEvent extends TypedContractEvent>(
    event: TCEvent
  ): Promise<Array<TypedListener<TCEvent>>>;
  listeners(eventName?: string): Promise<Array<Listener>>;
  removeAllListeners<TCEvent extends TypedContractEvent>(
    event?: TCEvent
  ): Promise<this>;

  cancelRental: TypedContractMethod<[], [void], "nonpayable">;

  company: TypedContractMethod<[], [string], "view">;

  deposit: TypedContractMethod<[], [bigint], "view">;

  depositFrozen: TypedContractMethod<[], [boolean], "view">;

  depositFunds: TypedContractMethod<[], [void], "payable">;

  depositReturned: TypedContractMethod<[], [boolean], "view">;

  endTime: TypedContractMethod<[], [bigint], "view">;

  finalizeRental: TypedContractMethod<[], [void], "nonpayable">;

  finishRentalEarly: TypedContractMethod<
    [usedDays: BigNumberish, pricePerDay: BigNumberish],
    [void],
    "nonpayable"
  >;

  freezeDeposit: TypedContractMethod<[], [void], "nonpayable">;

  fundsDeposited: TypedContractMethod<[], [boolean], "view">;

  initialize: TypedContractMethod<
    [
      _renter: AddressLike,
      _company: AddressLike,
      _deposit: BigNumberish,
      _rentAmount: BigNumberish,
      _startTime: BigNumberish,
      _endTime: BigNumberish,
      _unlockDelayHours: BigNumberish
    ],
    [void],
    "nonpayable"
  >;

  initialized: TypedContractMethod<[], [boolean], "view">;

  markRentalStarted: TypedContractMethod<[], [void], "nonpayable">;

  releaseDeposit: TypedContractMethod<
    [amountToCompany: BigNumberish],
    [void],
    "nonpayable"
  >;

  rentAmount: TypedContractMethod<[], [bigint], "view">;

  rentalCompleted: TypedContractMethod<[], [boolean], "view">;

  rentalStarted: TypedContractMethod<[], [boolean], "view">;

  renter: TypedContractMethod<[], [string], "view">;

  returnDepositIfNoDispute: TypedContractMethod<[], [void], "nonpayable">;

  startTime: TypedContractMethod<[], [bigint], "view">;

  unlockTime: TypedContractMethod<[], [bigint], "view">;

  getFunction<T extends ContractMethod = ContractMethod>(
    key: string | FunctionFragment
  ): T;

  getFunction(
    nameOrSignature: "cancelRental"
  ): TypedContractMethod<[], [void], "nonpayable">;
  getFunction(
    nameOrSignature: "company"
  ): TypedContractMethod<[], [string], "view">;
  getFunction(
    nameOrSignature: "deposit"
  ): TypedContractMethod<[], [bigint], "view">;
  getFunction(
    nameOrSignature: "depositFrozen"
  ): TypedContractMethod<[], [boolean], "view">;
  getFunction(
    nameOrSignature: "depositFunds"
  ): TypedContractMethod<[], [void], "payable">;
  getFunction(
    nameOrSignature: "depositReturned"
  ): TypedContractMethod<[], [boolean], "view">;
  getFunction(
    nameOrSignature: "endTime"
  ): TypedContractMethod<[], [bigint], "view">;
  getFunction(
    nameOrSignature: "finalizeRental"
  ): TypedContractMethod<[], [void], "nonpayable">;
  getFunction(
    nameOrSignature: "finishRentalEarly"
  ): TypedContractMethod<
    [usedDays: BigNumberish, pricePerDay: BigNumberish],
    [void],
    "nonpayable"
  >;
  getFunction(
    nameOrSignature: "freezeDeposit"
  ): TypedContractMethod<[], [void], "nonpayable">;
  getFunction(
    nameOrSignature: "fundsDeposited"
  ): TypedContractMethod<[], [boolean], "view">;
  getFunction(
    nameOrSignature: "initialize"
  ): TypedContractMethod<
    [
      _renter: AddressLike,
      _company: AddressLike,
      _deposit: BigNumberish,
      _rentAmount: BigNumberish,
      _startTime: BigNumberish,
      _endTime: BigNumberish,
      _unlockDelayHours: BigNumberish
    ],
    [void],
    "nonpayable"
  >;
  getFunction(
    nameOrSignature: "initialized"
  ): TypedContractMethod<[], [boolean], "view">;
  getFunction(
    nameOrSignature: "markRentalStarted"
  ): TypedContractMethod<[], [void], "nonpayable">;
  getFunction(
    nameOrSignature: "releaseDeposit"
  ): TypedContractMethod<[amountToCompany: BigNumberish], [void], "nonpayable">;
  getFunction(
    nameOrSignature: "rentAmount"
  ): TypedContractMethod<[], [bigint], "view">;
  getFunction(
    nameOrSignature: "rentalCompleted"
  ): TypedContractMethod<[], [boolean], "view">;
  getFunction(
    nameOrSignature: "rentalStarted"
  ): TypedContractMethod<[], [boolean], "view">;
  getFunction(
    nameOrSignature: "renter"
  ): TypedContractMethod<[], [string], "view">;
  getFunction(
    nameOrSignature: "returnDepositIfNoDispute"
  ): TypedContractMethod<[], [void], "nonpayable">;
  getFunction(
    nameOrSignature: "startTime"
  ): TypedContractMethod<[], [bigint], "view">;
  getFunction(
    nameOrSignature: "unlockTime"
  ): TypedContractMethod<[], [bigint], "view">;

  filters: {};
}
