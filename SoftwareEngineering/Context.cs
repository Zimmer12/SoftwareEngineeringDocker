using Microsoft.EntityFrameworkCore;
using SoftwareEngineering.Entities;

namespace SoftwareEngineering
{
    public class Context: DbContext
    {
        public DbSet<KundeDb> Kunden { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase("SE_Login");
        }
    }
}
