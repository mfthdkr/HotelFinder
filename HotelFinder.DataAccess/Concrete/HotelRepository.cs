using HotelFinder.DataAccess.Abstract;
using HotelFinder.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelFinder.DataAccess.Concrete
{
    public class HotelRepository : IHotelRepository
    {

        public async Task<List<Hotel>> GetAllHotels()
        {
            using (var dbContext = new HotelDbContext())
            {
                return await dbContext.Hotels.ToListAsync();
            }
        }
        public async Task<Hotel> GetHotelById(int id)
        {
            using (var dbContext = new HotelDbContext())
            {
                return await dbContext.Hotels.FindAsync(id);
            }
        }
        public async Task<Hotel> GetHotelByName(string name)
        {
            using (var dbContext = new HotelDbContext())
            {
                return await dbContext.Hotels.FirstOrDefaultAsync(h => h.Name.ToLower() == name.ToLower());
            }
        }
        public async Task DeleteHotel(int id)
        {
            using (var dbContext = new HotelDbContext())
            {
                var deletedHotel = await GetHotelById(id);
                dbContext.Hotels.Remove(deletedHotel);
                await  dbContext.SaveChangesAsync();
            }
        }
        public async Task<Hotel> CreateHotel(Hotel hotel)
        {
            using (var dbContext = new HotelDbContext())
            {
                dbContext.Hotels.Add(hotel);
                await dbContext.SaveChangesAsync();

                return hotel;
            }
        }

        public async Task<Hotel> UpdateHotel(Hotel hotel)
        {
            using (var dbContext = new HotelDbContext())
            {
                dbContext.Hotels.Update(hotel);
                await dbContext.SaveChangesAsync();

                return hotel;
            }
        }
    }
}
