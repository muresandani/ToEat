using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToEat.Models;

namespace ToEat.Data
{
    public class ToEatContext : DbContext
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
    }
}
