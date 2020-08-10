using System;
using System.Collections.Generic;

namespace PizzaStore.Domain.Models
{
    public class OrderModel : AModel
    {
        public int OrderId { get; set; }
        public List<OrderModel> orderdet { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Address { get; set; }
        public decimal OTotal { get; set; }
        public string UserId { get; set; }
        public DateTime PlaceTime { get; set; }
    }
}