using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Id)
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("NEWSEQUENTIALID()");
                entity.Property(x => x.Code).HasMaxLength(50);
                entity.Property(x => x.FullName).HasMaxLength(255);
                entity.Property(x => x.Email).HasMaxLength(255);
                entity.Property(x => x.Phone).HasMaxLength(20);
                entity.Property(x => x.Address).HasMaxLength(500);
            });
        }
    }
}
