namespace SmartRentCar.DTO
{
    public enum RentContractStatus
    {
        PendingPayment = 1,                // Ожидание оплаты
        PendingConfirmation = 2,          // Ожидание подтверждения
        Active = 3,                       // Активная аренда
        PendingEarlyEnd = 4,              // Ожидание досрочного завершения
        PendingCompletion = 5,            // Ожидание завершения
        PendingArbitration = 6,           // Ожидание решения арбитра
        Canceled = 7,                     // Отменено
        Completed = 8                     // Завершено
    }
}
