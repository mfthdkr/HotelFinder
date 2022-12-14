using HotelFinder.DataAccess.Abstract;
using HotelFinder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelFinder.DataAccess.Concrete
{
    public class HotelRepository : IHotelRepository
    {
        
        public List<Hotel> GetAllHotels()
        {
            using(var dbContext= new HotelDbContext())
            {
                return dbContext.Hotels.ToList();
            }
        }
        public Hotel GetHotelById(int id)
        {
            using (var dbContext = new HotelDbContext())
            {
                return dbContext.Hotels.Find(id);
            }
        }
        public void DeleteHotel(int id)
        {
            using (var dbContext = new HotelDbContext())
            {
                var deletedHotel = GetHotelById(id);
                dbContext.Hotels.Remove(deletedHotel);
                dbContext.SaveChanges();
            }
        }
        public Hotel CreateHotel(Hotel hotel)
        {
            using (var dbContext = new HotelDbContext())
            {
                dbContext.Hotels.Add(hotel);
                dbContext.SaveChanges();

                return hotel;
            }
        }

        public Hotel UpdateHotel(Hotel hotel)
        {
            using (var dbContext = new HotelDbContext())
            {
                dbContext.Hotels.Update(hotel);
                dbContext.SaveChanges();

                return hotel;
            }
        }
    }
}
