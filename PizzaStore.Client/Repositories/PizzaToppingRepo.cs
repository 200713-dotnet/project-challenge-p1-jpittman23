using Microsoft.EntityFrameworkCore;
using PizzaStore.Domain.Factories;
using PizzaStore.Storing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStore.Client.Repositories
{
    public class PizzaToppingRepo : IPizzaToppingRepo
    {
        private readonly PizzaStoreDBContext _DBContext;

        public PizzaToppingRepo(PizzaStoreDBContext DBContext)
        {
            _DBContext = DBContext;
        }

        public IEnumerable<PizzaFactory> PizzaTopping => _DBContext.PizzaToppings.Include(x => x.Pizza).Include(x => x.Topping);

        public void Add(PizzaFactory PizzaTopping)
        {
            _DBContext.PizzaToppings.Add(PizzaTopping);
        }

        public IEnumerable<PizzaFactory> GetAll()
        {
            return _DBContext.PizzaToppings.ToList();
        }

        public async Task<IEnumerable<PizzaFactory>> GetAllAsync()
        {
            return await _DBContext.PizzaToppings.ToListAsync();
        }

        public PizzaFactory GetById(int? id)
        {
            return _DBContext.PizzaToppings.FirstOrDefault(p => p.Id == id);
        }

        public async Task<PizzaFactory> GetByIdAsync(int? id)
        {
            return await _DBContext.PizzaToppings.FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Exists(int id)
        {
            return _DBContext.PizzaToppings.Any(p => p.Id == id);
        }

        public void Remove(PizzaFactory PizzaTopping)
        {
            _DBContext.PizzaToppings.Remove(PizzaTopping);
        }

        public void SaveChanges()
        {
            _DBContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _DBContext.SaveChangesAsync();
        }

        public void Update(PizzaFactory PizzaTopping)
        {
            _DBContext.PizzaToppings.Update(PizzaTopping);
        }
    }
}