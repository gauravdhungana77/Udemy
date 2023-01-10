namespace Hotel_Listings.Model
{
    public class CountryDTO:CreateCountryDTO
    {
        public int Id { get; set; }
        public virtual IList<HotelDTO> Hotels { get; set; }
    }
    public class CreateCountryDTO
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}
