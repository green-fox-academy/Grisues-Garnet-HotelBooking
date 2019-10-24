using Microsoft.EntityFrameworkCore;

namespace IdentiyCheckTest.TestUtils
{
    class TestDbOptions
    {
        public static DbContextOptions<ApplicationDbContext> Get()
        {
            return new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "blog-testdb")
                .Options;
        }
    }
}