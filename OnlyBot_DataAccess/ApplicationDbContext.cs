using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlyBot_Models;

namespace OnlyBot_DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Bot> Bots { get; set; }
        public DbSet<Script> Scripts { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Proxy> Proxies { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var Bots = modelBuilder.Entity<Bot>();
            Bots.HasKey(b => b.Id);
            Bots.Property(b => b.AccountName).IsRequired();
            Bots.Property(b => b.Password).IsRequired();
            Bots.HasMany(b => b.Inventories)
                .WithOne(i => i.Bot)
                .HasForeignKey(i => i.BotId);
            Bots.HasOne(b => b.Script)
                .WithMany(s => s.Bots)
                .HasForeignKey(s => s.ScriptId)
                .OnDelete(DeleteBehavior.SetNull);
            Bots.HasOne(b => b.Proxy)
                .WithMany(p => p.Bots)
                .HasForeignKey(b => b.ProxyId)
                .OnDelete(DeleteBehavior.SetNull);
            Bots.HasOne(b => b.Order)
                .WithMany(o => o.Bots)
                .HasForeignKey(b => b.OrderId)
                .OnDelete(DeleteBehavior.SetNull);
            Bots.ToTable(tb => tb.UseSqlOutputClause(false));

            var Scripts = modelBuilder.Entity<Script>();
            Scripts.HasKey(s => s.Id);
            Scripts.Property(s => s.Name).IsRequired();
            Scripts.Property(s => s.Path).IsRequired();
            Scripts.Property(s => s.Type).IsRequired();
            Scripts.ToTable(tb => tb.UseSqlOutputClause(false));

            var Items = modelBuilder.Entity<Item>();
            Items.HasKey(i => i.Id);
            Items.Property(i => i.ItemId).IsRequired();
            Items.Property(i => i.Name).IsRequired();
            Items.Property(i => i.Level).IsRequired();
            Items.Property(i => i.Quantity).IsRequired();
            Items.Property(i => i.Weight).IsRequired();
            Items.Property(i => i.Type).IsRequired();
            Items.Property(i => i.SubType).IsRequired();
            Items.ToTable(tb => tb.UseSqlOutputClause(false));

            var Inventories = modelBuilder.Entity<Inventory>();
            Inventories.HasKey(i => i.Id);
            Inventories.HasMany(i => i.Items)
                .WithOne(i => i.Inventory)
                .HasForeignKey(i => i.InventoryId);
            Inventories.ToTable(tb => tb.UseSqlOutputClause(false));

            var Proxies = modelBuilder.Entity<Proxy>();
            Proxies.HasKey(p => p.Id);
            Proxies.Property(p => p.Name).IsRequired();
            Proxies.Property(p => p.IP).IsRequired();
            Proxies.Property(p => p.Port).IsRequired();
            Proxies.Property(p => p.SocketType).IsRequired();
            Proxies.Property(p => p.PlanExpirationDate).IsRequired();
            Proxies.ToTable(tb => tb.UseSqlOutputClause(false));

            var Orders = modelBuilder.Entity<Order>();
            Orders.HasKey(o => o.Id);
            Orders.Property(o => o.OrderParams).IsRequired();
            Orders.Property(o => o.Description).IsRequired();
            Orders.Property(o => o.Status).IsRequired();
            Orders.Property(o => o.CreatedAt).IsRequired();
            Orders.ToTable(tb => tb.UseSqlOutputClause(false));

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
        }
    }
}
