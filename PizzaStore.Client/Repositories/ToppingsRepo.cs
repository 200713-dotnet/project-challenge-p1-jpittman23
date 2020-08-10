
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PizzaStore.Storing;
using PizzaStore.Domain.Models;

namespace PizzaStore.Client.Repositories
{
    public class ToppingsRepo : IToppingsRepo
    {
        private readonly PizzaStoreDBContext _DBContext;

        public ToppingsRepo(PizzaStoreDBContext DBContext)
        {
            _DBContext = DBContext;
        }

        public IEnumerable<ToppingModel> topping => _DBContext.Toppings.Include(x => x.Name); 

        public void Add(ToppingModel topping)
        {
            _DBContext.Toppings.Add(topping);
        }

        public IEnumerable<ToppingModel> GetAll()
        {
            return _DBContext.Toppings.ToList();
        }

        public async Task<IEnumerable<ToppingModel>> GetAllAsync()
        {
            return await _DBContext.Toppings.ToListAsync();
        }

        public ToppingModel GetById(int? id)
        {
            return _DBContext.Toppings.FirstOrDefault(p => p.Id == id);
        }

        public async Task<ToppingModel> GetByIdAsync(int? id)
        {
            return await _DBContext.Toppings.FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Exists(int id)
        {
            return _DBContext.Toppings.Any(p => p.Id == id);
        }

        public void Remove(ToppingModel topping)
        {
            _DBContext.Toppings.Remove(topping);
        }

        public void SaveChanges()
        {
            _DBContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _DBContext.SaveChangesAsync();
        }

        public void Update(ToppingModel topping)
        {
            _DBContext.Toppings.Update(topping);
        }
    }
}