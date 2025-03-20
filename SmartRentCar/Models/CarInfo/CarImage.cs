namespace SmartRentCar.Models.CarInfo
{
    public class CarImage
    {
        public int CarImageId { get; set; }
        public int CarId { get; set; }
     
        public byte[] ImageData { get; set; }
    }
}
