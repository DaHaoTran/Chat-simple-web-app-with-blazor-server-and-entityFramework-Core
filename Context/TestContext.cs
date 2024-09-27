using ChatSimple.Data;
using Microsoft.EntityFrameworkCore;

namespace ChatSimple.Context
{
    public class TestContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Message> Messages { get; set; }

        public TestContext(DbContextOptions<TestContext> options) : base(options) { }
    }
}
