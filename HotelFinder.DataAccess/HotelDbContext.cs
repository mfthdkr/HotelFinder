using HotelFinder.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace HotelFinder.DataAccess
{
    public class HotelDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer($"Server=DESKTOP-KVPF9GU;Database=HotelDb;Trusted_Connection=True;Connect Timeout=30;MultipleActiveResultSets=True;");
        }
        public DbSet<Hotel> Hotels { get; set; }
    }
}
