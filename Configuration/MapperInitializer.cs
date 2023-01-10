using AutoMapper;
using Hotel_Listings.Data;
using Hotel_Listings.Model;

namespace Hotel_Listings.Configuration
{
    public class MapperInitializer:Profile
    {
        public MapperInitializer()
        {
            CreateMap<Hotel, HotelDTO>().ReverseMap();
            CreateMap<Country, CountryDTO>().ReverseMap();
            CreateMap<Hotel, CreateHotelDTO>().ReverseMap();
            CreateMap<Country, CreateCountryDTO>().ReverseMap();
            //CreateMap<LoginDTO, UserDTO>().ReverseMap();
            //CreateMap<ApiUser, UserDTO>().ReverseMap();
        }
    }
}
