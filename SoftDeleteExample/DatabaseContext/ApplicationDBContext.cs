using Microsoft.EntityFrameworkCore;
using SoftDeleteExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoftDeleteExample.DatabaseContext
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookCD> BookCDs { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasQueryFilter(b => !b.IsDeleted);
        }

        public override int SaveChanges()
        {
            OnBeforeSaving();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            foreach (var entry in ChangeTracker.Entries<Book>())
            {
                switch (entry.State)
                {
                    
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                    case EntityState.Modified:
                        if((bool)entry.Property("IsDeleted").CurrentValue!=false)
                        {
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["IsDeleted"] = true;
                        }
                        else
                        {
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["IsDeleted"] = false;
                        }
                        
                        break;
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;
                  
                }
            }
        }
    }

}
