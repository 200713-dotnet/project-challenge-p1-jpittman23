using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PizzaStore.Domain.Factories;
using PizzaStore.Domain.Models;
using PizzaStore.Storing;

namespace PizzaStore.Client
{
    public class Seed
    {
        public static void Initialize(PizzaStoreDBContext DBContext, IServiceProvider serviceProvider)
        {
            DBContext.Database.EnsureCreated();

            var mnger = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var User = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (DBContext.Pizza.Any())
            {
                return;
            }

            ClearDatabase(DBContext);
            CreateAdminRole(DBContext, mnger, User);
            SeedDB(DBContext, mnger, User);
        }
        private static void CreateAdminRole(PizzaStoreDBContext DBContext, RoleManager<IdentityRole> _mnger, UserManager<IdentityUser> _User)
        {
            bool roleExists = _mnger.RoleExistsAsync("Admin").Result;
            if (roleExists)
            {
                return;
            }

            var role = new IdentityRole()
            {
                Name = "Admin"
            };
            _mnger.CreateAsync(role).Wait();

            var user = new IdentityUser()
            {
                UserName = "admin",
                Email = "admin@default.com"
            };

            string adminPassword = "Password123";
            var userResult = _User.CreateAsync(user, adminPassword).Result;

            if (userResult.Succeeded)
            {
                _User.AddToRoleAsync(user, "Admin").Wait();
            }
        }

        private static void SeedDB(PizzaStoreDBContext DBContext, RoleManager<IdentityRole> _mnger, UserManager<IdentityUser> _User)
        {
            var Pizza1 = new PizzaModel { Name = "Cheese", crust = new CrustModel() { Name = "Regular" }, size = new SizeModel() { Name = "Medium" }, Price = 10.00M, Description= "Signature Cheese Pizza with our secret 2 blend cheese", Image = "http://placecorgi.com/250" };
            var Pizza2 = new PizzaModel { Name = "Pepperoni", crust = new CrustModel() { Name = "Regular" }, size = new SizeModel() { Name = "Medium" }, Price = 10.00M, Description= "Peppernoi Pizza with our secret All natural pepperoni", Image = "http://placecorgi.com/250"  };
            var Pizza3 = new PizzaModel { Name = "Sausage", crust = new CrustModel() { Name = "Regular" }, size = new SizeModel() { Name = "Medium" }, Price = 10.00M,Description= "Sausage Pizza with spicy italian sausage", Image = "http://placecorgi.com/250"  };
            var Pizza4 = new PizzaModel { Name = "Hawaiian", crust = new CrustModel() { Name = "Regular" }, size = new SizeModel() { Name = "Medium" }, Price = 15.00M, Description= "Hawaiian Pizza with fresh pinaple", Image = "http://placecorgi.com/250"  };

            var Pizzas = new List<PizzaModel>()
            {
                Pizza1, Pizza2, Pizza3, Pizza4
            };

            var top1 = new ToppingModel { Name = "Cheese" };
            var top2 = new ToppingModel { Name = "Pepperoni" };
            var top3 = new ToppingModel { Name = "sausage"};
            var top4 = new ToppingModel { Name = "Ham"};
            var top5 = new ToppingModel { Name = "Pineapple"};

            var Toppings = new List<ToppingModel>()
            {
                top1,top2,top3,top4,top5
            };

            var pizzaFact = new List<PizzaFactory>()
            {
                new PizzaFactory { Pizza = Pizza1, Topping = top1},
                new PizzaFactory { Pizza = Pizza1, Topping = top1},
                new PizzaFactory { Pizza = Pizza2, Topping = top1},
                new PizzaFactory { Pizza = Pizza2, Topping = top2},
                new PizzaFactory { Pizza = Pizza3, Topping = top1},
                new PizzaFactory { Pizza = Pizza3, Topping = top3},
                new PizzaFactory { Pizza = Pizza4, Topping = top1},
                new PizzaFactory { Pizza = Pizza4, Topping = top4},
                new PizzaFactory { Pizza = Pizza4, Topping = top5},
            };

            var order1 = new OrderModel
            {
                FName = "Jordan",
                LName = "Pittman",
                Address = "123 OverThere",
                PlaceTime = DateTime.Now.AddDays(-2),
                OTotal = 30.00M,
            };

            var orders = new List<OrderModel>()
            {
                order1
            };

            var Odetail = new List<ODetail>()
            {
                new ODetail { Order = order1, Pizza=Pizza1, Total=2, Cost= Pizza1.Price}
            };

            DBContext.Pizza.AddRange(Pizzas);
            DBContext.order.AddRange(orders);
            DBContext.Toppings.AddRange(Toppings);
            DBContext.PizzaToppings.AddRange(pizzaFact);
            DBContext.Odetail.AddRange(Odetail);

            DBContext.SaveChanges();
        }


        private static void ClearDatabase(PizzaStoreDBContext _DBContext)
        {
            var pizzaTopping = _DBContext.PizzaToppings.ToList();
            _DBContext.PizzaToppings.RemoveRange(pizzaTopping);

            var topping = _DBContext.Toppings.ToList();
            _DBContext.Toppings.RemoveRange(topping);

            var Cart = _DBContext.cart.ToList();
            _DBContext.cart.RemoveRange(Cart);

            var orders = _DBContext.order.ToList();
            _DBContext.order.RemoveRange(orders);

            var pizzas = _DBContext.Pizza.ToList();
            _DBContext.Pizza.RemoveRange(pizzas);

            _DBContext.SaveChanges();
        }
    }
}