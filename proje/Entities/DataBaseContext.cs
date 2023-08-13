using Microsoft.EntityFrameworkCore;
namespace proje.Entities
{
    public class DataBaseContext:DbContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {

        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Seller> Sellers { get; set;}
        public DbSet<Customer> Customers { get;set; }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Land> Lands { get; set; }
        public DbSet<RentHouse> RentHouses { get; set; }
        public DbSet<SalesHouse> SalesHouses { get; set;}
        public DbSet<Garden> Gardens { get; set;}
    }
}
