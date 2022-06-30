using MyAdvices.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAdvices.DataAccessLayer.EntityFramework
{
   public class DatabaseContext:DbContext
    {
        public DbSet<AdvicesUser> AdvicesUsers { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Liked> Likes { get; set; }

        public DatabaseContext()
        {
            Database.SetInitializer(new MyInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //FluentAPI
            // base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Note>()
                 .HasMany(n => n.Comments)
                 .WithRequired(c => c.Note)
                 .WillCascadeOnDelete(true);

            modelBuilder.Entity<Note>()
                 .HasMany(n => n.Likes)
                 .WithRequired(c => c.Note)
                 .WillCascadeOnDelete(true);

        }
    }
}
