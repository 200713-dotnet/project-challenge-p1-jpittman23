using System.Collections.Generic;
using PizzaStore.Domain.Factories;

namespace PizzaStore.Domain.Models
{
    public class PizzaModel : AModel
    {
        public PizzaModel()
        {
            PizzaToppings = new HashSet<PizzaFactory>();
        }
        public virtual CrustModel crust { get; set; }
        public virtual SizeModel size { get; set; }
        public virtual ICollection<PizzaFactory> PizzaToppings { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}