export interface DepositDispute {
    depositDisputeId: number;
    disputeStatusName: number;
    deposit: number;
    depositWithheld: number;
}

export interface DisputeUpdate {
    depositDisputeId: number;
    deposit: number;
    depositWithheld: number;
}