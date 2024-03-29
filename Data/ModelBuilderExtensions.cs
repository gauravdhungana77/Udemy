﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed (this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "China",
                    ShortName = "CH"
                },
                 new Country
                 {
                     Id = 2,
                     Name = "Nepal",
                     ShortName = "NEP"
                 }
                );
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Rato Vale",
                    Address = "Nepal",
                    CountryId = 2
                },
                 new Hotel
                 {
                     Id = 2,
                     Name = "Rato Vale2",
                     Address = "China",
                     CountryId = 1
                 }
                );

        }
    }
}
