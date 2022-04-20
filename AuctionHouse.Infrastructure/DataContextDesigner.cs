using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AuctionHouse.Infrastructure
{
    public class DataContextDesigner : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>()
                .UseNpgsql("Host=auctionhouse.postgresql;Port=5432;Username=dba;Password=1234;Database=auction_house;Pooling=true;");

            return new DataContext(optionsBuilder.Options);
        }
    }
}
