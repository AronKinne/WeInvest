using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeInvest.Utilities.Services {
    public interface IDataService<T> {

        Task<T> CreateAsync(T entity);
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);

    }
}
