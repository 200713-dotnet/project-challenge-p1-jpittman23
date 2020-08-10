using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PizzaStore.Domain.Models;

namespace PizzaStore.Client.Repositories
{
    public interface IPizzaRepo
    {
        IEnumerable<PizzaModel> Pizzas { get; }

        PizzaModel GetById(int? id);
        Task<PizzaModel> GetByIdAsync(int? id);

        PizzaModel GetByIdIncluded(int? id);
        Task<PizzaModel> GetByIdIncludedAsync(int? id);

        bool Exists(int id);

        IEnumerable<PizzaModel> GetAll();
        Task<IEnumerable<PizzaModel>> GetAllAsync();

        IEnumerable<PizzaModel> GetAllIncluded();
        Task<IEnumerable<PizzaModel>> GetAllIncludedAsync();

        void Add(PizzaModel pizza);
        void Update(PizzaModel pizza);
        void Remove(PizzaModel pizza);

        void SaveChanges();
        Task SaveChangesAsync();

    }
}