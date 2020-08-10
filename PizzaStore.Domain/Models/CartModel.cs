namespace PizzaStore.Domain.Models
{
    public class CartModel : AModel
    {
        public int CartId { get; set; }
        public PizzaModel Pizzas { get; set; }
        public int Total { get; set; }
        public string Cartitem { get; set; }
    }
}