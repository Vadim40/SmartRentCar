namespace ContractService.DTOs
{
    public enum RentalStatus
    {
        PendingConfirmation = 1,  // Ожидание подтверждения
        PendingStart = 2,         // Ожидание начала аренды (оплачено, но не началось)
        Active = 3,               // Активный
        PendingInspection = 4,     // Ожидание инспекции (проверка автомобиля после возврата)
        PengingUserResponse = 5,    //Ожидание ответа от клиента
        PendingResolution = 6,     // Ожидание решения по возврату (обнаружены проблемы)
        Canceled = 7,             // Отменённый
        Completed = 8             // Завершенный
    }
}
