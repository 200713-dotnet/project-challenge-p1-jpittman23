using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaStore.Domain.Models;

namespace PizzaStore.Client.Repositories
{
    public interface IToppingsRepo
    {
        IEnumerable<ToppingModel> topping { get; }

        ToppingModel GetById(int? id);
        Task<ToppingModel> GetByIdAsync(int? id);

        bool Exists(int id);

        IEnumerable<ToppingModel> GetAll();
        Task<IEnumerable<ToppingModel>> GetAllAsync();

        void Add(ToppingModel topping);
        void Update(ToppingModel topping);
        void Remove(ToppingModel topping);

        void SaveChanges();
        Task SaveChangesAsync();
    }
}