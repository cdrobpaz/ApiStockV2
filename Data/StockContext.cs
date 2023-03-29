using ApiStockV2.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiStockV2.Data
{
    public class StockContext:DbContext
    {
        public StockContext(DbContextOptions<StockContext> options): base(options) { }
        public DbSet<Stock> Stock { get; set; }
    }
}
