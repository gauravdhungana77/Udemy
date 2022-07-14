using HotelListing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Model
{
    public class HotelDTO:CreateHotelDTO
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
