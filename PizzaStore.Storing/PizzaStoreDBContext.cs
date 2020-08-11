using PizzaStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Domain.Factories;

namespace PizzaStore.Storing
{
    public class PizzaStoreDBContext : DbContext
    {
        public PizzaStoreDBContext (DbContextOptions<PizzaStoreDBContext> options) : base(options)
        {

        }
        public DbSet<PizzaModel> Pizza{ get; set; }
        public DbSet<CrustModel> Crust { get; set; }
        public DbSet<ToppingModel> Toppings { get; set; }
        public DbSet<PizzaFactory> PizzaToppings { get; set;}
        public DbSet<SizeModel> Size { get; set; }
        public DbSet<OrderModel> order { get; set; }
        public DbSet<StoreModel> store { get; set; }
        public DbSet<ODetail> Odetail { get; set; }
        public DbSet<CartModel> cart { get; set; }
    }
}