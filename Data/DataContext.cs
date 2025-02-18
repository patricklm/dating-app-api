using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;
public class DataContext(DbContextOptions<DataContext> options):DbContext(options)
{
    public DbSet<AppUser> Users => Set<AppUser>();
}
