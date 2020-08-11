using System.Collections.Generic;
using PizzaStore.Domain.Models;

namespace PizzaStore.Client.Models
{
  public class StoreViewModel
  {
    public IEnumerable<StoreModel> StoreList { get; set; }
  }
}