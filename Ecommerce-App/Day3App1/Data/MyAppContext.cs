using Microsoft.EntityFrameworkCore;
using Models;

namespace  Data;

public class MyAppContext:DbContext
{
    public MyAppContext(DbContextOptions<MyAppContext> options):base(options)
    {
        
    }
    public DbSet<Products> Products { get; set; }
    public DbSet<Categories> Categories { get; set; }

}
