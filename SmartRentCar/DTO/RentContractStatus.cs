namespace SmartRentCar.DTO
{
    public enum RentContractStatus
    {
        PendingConfirmation = 1,  // Ожидание подтверждения
        PendingStart = 2,         // Ожидание начала аренды (оплачено, но не началось)
        Active = 3,               // Активный
        PendingInspection = 4,     // Ожидание инспекции (проверка автомобиля после возврата)
        PendingResolution = 5,     // Ожидание решения по возврату (обнаружены проблемы)
        Canceled = 6,             // Отменённый
        Completed = 7             // Завершенный
    }
}
