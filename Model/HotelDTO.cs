namespace Hotel_Listings.Model
{
    public class HotelDTO:CreateCountryDTO
    {
        public int Id { get; set; }
        public CountryDTO Country { get; set; }
    }

    public class CreateHotelDTO
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }
    }
}
