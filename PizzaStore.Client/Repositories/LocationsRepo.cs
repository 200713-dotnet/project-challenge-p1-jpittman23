using Microsoft.EntityFrameworkCore;
using PizzaStore.Domain.Models;
using PizzaStore.Storing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaStore.Client.Repositories
{
    public class LocationsRepo
    {
        private readonly PizzaStoreDBContext _DBContext;

        public LocationsRepo(PizzaStoreDBContext DBContext)
        {
            _DBContext = DBContext;
        }
        public IEnumerable<StoreModel> Stores => _DBContext.store.Include(p => p.Name ).Include( p => p.Description );

        public void Add(StoreModel store)
        {
            _DBContext.Add(store);
        }

        public IEnumerable<StoreModel> GetAll()
        {
            return _DBContext.store.ToList();
        }

        public async Task<IEnumerable<StoreModel>> GetAllAsync()
        {
            return await _DBContext.store.ToListAsync();
        }

        public async Task<IEnumerable<StoreModel>> GetAllIncludedAsync()
        {
            return await _DBContext.store.Include(p => p.Name).Include(p => p.Description).ToListAsync();
        }

        public IEnumerable<StoreModel> GetAllIncluded()
        {
            return _DBContext.store.Include(p => p.Name).Include(p => p.Description).ToList();
        }

        public StoreModel GetById(int? id)
        {
            return _DBContext.store.FirstOrDefault(p => p.Id == id);
        }

        public async Task<StoreModel> GetByIdAsync(int? id)
        {
            return await _DBContext.store.FirstOrDefaultAsync(p => p.Id == id);
        }

        public StoreModel GetByIdIncluded(int? id)
        {
            return _DBContext.store.Include(p => p.Name).Include(p => p.Description).FirstOrDefault(p => p.Id == id);
        }

        public async Task<StoreModel> GetByIdIncludedAsync(int? id)
        {
            return await _DBContext.store.Include(p => p.Name).Include(p => p.Description).FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Exists(int id)
        {
            return _DBContext.store.Any(p => p.Id == id);
        }

        public void Remove(StoreModel store)
        {
            _DBContext.Remove(store);
        }

        public async Task SaveChangesAsync()
        {
            await _DBContext.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _DBContext.SaveChanges();
        }

        public void Update(StoreModel store)
        {
            _DBContext.Update(store);
        }

    }
}