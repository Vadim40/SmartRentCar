namespace ContractService.DTOs
{
    public enum DepositDisputeStatus
    {
        PendingResolution = 1, // Ожидание решения 
        DepositRefunded = 2,   // Залог возвращен полностью
        PartialRefunded = 3,   // Залог возвращен частично
        DepositWithheld = 4,   // Залог удержан
    }
}
