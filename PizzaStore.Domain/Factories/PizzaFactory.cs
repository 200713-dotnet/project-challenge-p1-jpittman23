using System.Collections.Generic;
using PizzaStore.Domain.Models;

namespace PizzaStore.Domain.Factories
{
    public class PizzaFactory
    {
        public int Id { get; set; }
        public int PizzaId { get; set; }
        public int ToppingId { get; set; }

        public virtual ToppingModel Topping { get; set; }
        public virtual PizzaModel Pizza { get; set; }
    }
}