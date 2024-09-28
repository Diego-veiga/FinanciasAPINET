using System.Text.Json;
using financias.src.database.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace financiastests.helper
{
    public class RepositoryTestsHelper
    {
        
        public AppDbContext GetInMemoryAppDbContext(string? DatabaseName= null)
        {
            DbContextOptions<AppDbContext> options;
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase(DatabaseName??"RepositoryTestes");;
            options = builder.Options;
            AppDbContext appDbContext = new AppDbContext(options);
            appDbContext.Database.EnsureDeleted();
            appDbContext.Database.EnsureCreated();
            return appDbContext;
        }
        
    }
}