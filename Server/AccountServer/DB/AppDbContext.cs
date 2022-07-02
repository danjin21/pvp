using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AccountServer.DB
{

    public class AppDbContext : DbContext
    {
        public DbSet<AccountDb> Accounts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // 인덱싱을 건다
            builder.Entity<AccountDb>()
                .HasIndex(a => a.AccountName)
                .IsUnique();

        }
    }
}
