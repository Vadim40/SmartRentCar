namespace ContractService.DTOs
{
   public enum RentalStatus
{
    PendingPayment = 1,                // Ожидание оплаты
    PendingConfirmation = 2,          // Ожидание подтверждения
    PendingStart = 3,                 // Ожидание начала аренды
    Active = 4,                       // Активная аренда
    PendingEarlyEnd = 5,              // Ожидание досрочного завершения
    PendingCompletion = 6,            // Ожидание завершения
    PendingArbitration = 7,           // Ожидание решения арбитра
    Canceled = 8,                     // Отменено
    Completed = 9                     // Завершено
}

}
