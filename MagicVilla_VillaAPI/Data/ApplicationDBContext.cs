using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDBContext:DbContext
    {
        public DbSet<Villa> Villas { get; set; } //Villas is the table name in the database
    }
}
