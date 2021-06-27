using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeInvest.Domain.Services {
    public interface IDataAccess<T> {

        Task<T> CreateAsync(T entity);
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> UpdateAsync(int id, T entity);
        Task DeleteAsync(int id);

    }
}
