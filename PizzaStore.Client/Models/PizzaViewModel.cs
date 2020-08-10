using System.Collections.Generic;
using PizzaStore.Domain.Models;

namespace PizzaStore.Client.Models
{
  public class PizzaViewModel
  {
    public IEnumerable<PizzaModel> PizzaList { get; set; }
  }
}