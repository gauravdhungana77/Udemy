using HotelListing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Model
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
