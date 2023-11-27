using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToEat.Models;

namespace ToEat.Data
{
    public class ToEatContext : IdentityDbContext<User>
    {
        public ToEatContext (DbContextOptions<ToEatContext> options)
            : base(options)
        {
        }

        public DbSet<ToEat.Models.BasePromptElement> BasePromptElements { get; set; } = default!;
        public DbSet<ToEat.Models.MetaPromptElement> MetaPromptElements { get; set; } = default!;
        public DbSet<ToEat.Models.OptionalPromptElement> OptionalPromptElements { get; set; } = default!;     
        public DbSet<ToEat.Models.Conversation> Conversations { get; set; } = default!;
        public DbSet<ToEat.Models.Message> Messages { get; set; } = default!;
        public DbSet<ToEat.Models.Inventory> Inventories { get; set; } = default!;
        public DbSet<ToEat.Models.InventoryItem> InventoryItems { get; set; } = default!;
        public DbSet<ToEat.Models.User> Users { get; set; } = default!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("Users");
            // Assuming User has a property named 'Inventory' of type Inventory
            // and Inventory has a property named 'UserId' of type int
            modelBuilder.Entity<User>()
                .HasOne(u => u.Inventory) // User has one Inventory
                .WithOne(i => i.User) // Inventory is associated with one User
                .HasForeignKey<Inventory>(i => i.UserId); // Inventory has foreign key UserId
        }
    }

}
