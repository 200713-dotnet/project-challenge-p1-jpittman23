using Microsoft.EntityFrameworkCore;
using PizzaStore.Domain.Models;
using PizzaStore.Storing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStore.Client.Repositories
{
    public class PizzaRepo
    {
        private readonly PizzaStoreDBContext _DBContext;

        public PizzaRepo(PizzaStoreDBContext DBContext)
        {
            _DBContext = DBContext;
        }
        public IEnumerable<PizzaModel> Pizzas => _DBContext.Pizza.Include(p => p.crust ).Include( p => p.size ).Include( p => p.PizzaToppings);

        public void Add(PizzaModel pizza)
        {
            _DBContext.Add(pizza);
        }

        public IEnumerable<PizzaModel> GetAll()
        {
            return _DBContext.Pizza.ToList();
        }

        public async Task<IEnumerable<PizzaModel>> GetAllAsync()
        {
            return await _DBContext.Pizza.ToListAsync();
        }

        public async Task<IEnumerable<PizzaModel>> GetAllIncludedAsync()
        {
            return await _DBContext.Pizza.Include(p => p.size).Include(p => p.crust).Include(p => p.PizzaToppings).ToListAsync();
        }

        public IEnumerable<PizzaModel> GetAllIncluded()
        {
            return _DBContext.Pizza.Include(p => p.size).Include(p => p.crust).Include(p => p.PizzaToppings).ToList();
        }

        public PizzaModel GetById(int? id)
        {
            return _DBContext.Pizza.FirstOrDefault(p => p.Id == id);
        }

        public async Task<PizzaModel> GetByIdAsync(int? id)
        {
            return await _DBContext.Pizza.FirstOrDefaultAsync(p => p.Id == id);
        }

        public PizzaModel GetByIdIncluded(int? id)
        {
            return _DBContext.Pizza.Include(p => p.size).Include(p => p.crust).Include(p => p.PizzaToppings).FirstOrDefault(p => p.Id == id);
        }

        public async Task<PizzaModel> GetByIdIncludedAsync(int? id)
        {
            return await _DBContext.Pizza.Include(p => p.size).Include(p => p.crust).Include(p => p.PizzaToppings).FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Exists(int id)
        {
            return _DBContext.Pizza.Any(p => p.Id == id);
        }

        public void Remove(PizzaModel pizza)
        {
            _DBContext.Remove(pizza);
        }

        public async Task SaveChangesAsync()
        {
            await _DBContext.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _DBContext.SaveChanges();
        }

        public void Update(PizzaModel pizza)
        {
            _DBContext.Update(pizza);
        }

    }
}