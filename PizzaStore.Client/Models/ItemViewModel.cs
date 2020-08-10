using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStore.Client.Models
{
    public class ItemViewModel
    {
        public CartViewModel Cart { get; set; }
        public decimal CartTotal { get; set; }
    }
}