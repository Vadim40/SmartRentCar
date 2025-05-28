export interface DepositDispute {
    depositDisputeId: number;
    disputeStatusName: string;
    deposit: number;
    depositWithheld: number;
}

export interface DisputeUpdate {
    depositDisputeId: number;
    deposit: number;
    depositWithheld: number;
}

export interface DisputeMessage {
    depositDisputeId: number;
    depositWithheld: number;
    WitheldReason: string;
}

