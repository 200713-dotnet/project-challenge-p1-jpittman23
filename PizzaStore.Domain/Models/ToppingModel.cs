using System.Collections.Generic;
using PizzaStore.Domain.Factories;

namespace PizzaStore.Domain.Models
{
    public class ToppingModel : AModel
    {
        public ToppingModel()
        {
            PizzaFactories = new HashSet<PizzaFactory>();
        }

        public virtual ICollection<PizzaFactory> PizzaFactories { get; set; }
    }
}