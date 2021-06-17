using Comeinda.Data.Tables;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Comeinda.Data
{
    public class ComeindaDbContext : IdentityDbContext<CastomUser>
    {
        public DbSet<CategoryEventTable> CategoryEvents { get; set; }
        public DbSet<EventTable> Events { get; set; }
        public DbSet<FileTable> Files { get; set; }
        public DbSet<TicketSetsTable> TicketSets { get; set; }
        public DbSet<TicketTable> Tickets { get; set; }
        public new DbSet<CastomUser> Users { get; set; }

        public ComeindaDbContext(DbContextOptions<ComeindaDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
