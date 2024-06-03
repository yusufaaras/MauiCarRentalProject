using ApiOgrenmeProjesi.Core;

namespace ApiOgrenmeProjesi.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<ProductEntity> productEntities { get; set; }
        public DbSet<Car> carEntites { get; set; }
        public DbSet<Customer> customerEntities { get; set; }
        public DbSet<Rental> rentalEntities { get; set; }
        public DbSet<Return> returnEntities { get; set; }
        public DbSet<User> userEntities { get; set; }


    }
}
