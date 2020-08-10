using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaStore.Domain.Factories;

namespace PizzaStore.Client.Repositories
{
    public interface IPizzaToppingRepo
    {
        IEnumerable<PizzaFactory> PizzaTopping { get; }

        PizzaFactory GetById(int? id);
        Task<PizzaFactory> GetByIdAsync(int? id);

        bool Exists(int id);

        IEnumerable<PizzaFactory> GetAll();
        Task<IEnumerable<PizzaFactory>> GetAllAsync();

        void Add(PizzaFactory PizzaToppings);
        void Update(PizzaFactory PizzaToppings);
        void Remove(PizzaFactory PizzaToppings);

        void SaveChanges();
        Task SaveChangesAsync();
    }
}