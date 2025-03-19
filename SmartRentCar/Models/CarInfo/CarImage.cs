namespace SmartRentCar.Models.CarInfo
{
    public class CarImage
    {
        public int CarImageId { get; set; }
        public string CarId { get; set; }
     
        public byte[] ImageData { get; set; }
    }
}
