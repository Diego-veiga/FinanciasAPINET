using financias.src.database.Context;
using Microsoft.EntityFrameworkCore;

namespace financiastests.helper
{
    public class RepositoryTestsHelper
    {
        public AppDbContext GetInMemoryAppDbContext()
        {
            DbContextOptions<AppDbContext> options;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("RepositoryTeste");;
            options = builder.Options;
            AppDbContext appDbContext = new AppDbContext(options);
            appDbContext.Database.EnsureDeleted();
            appDbContext.Database.EnsureCreated();
            return appDbContext;
        }
        
    }
}