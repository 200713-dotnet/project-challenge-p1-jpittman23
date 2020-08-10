using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStore.Domain.Models
{
    public class ODetail
    {
        public int ODetailId { get; set; }
        public int OId { get; set; }
        public int PizzaId { get; set; }
        public int Total { get; set; }
        public decimal Cost { get; set; }
        public virtual PizzaModel Pizza { get; set; }
        public virtual OrderModel Order { get; set; }
    }
}