using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Storing;

namespace PizzaStore.Client.Menu
{

    public class Menu : ViewComponent
    {
        private readonly PizzaStoreDBContext _DBContext;
        public Menu(PizzaStoreDBContext DBContext)
        {
            _DBContext = DBContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _DBContext.Pizza.OrderBy(c => c.Name).ToListAsync();
            return View(model);
        }
    }
}