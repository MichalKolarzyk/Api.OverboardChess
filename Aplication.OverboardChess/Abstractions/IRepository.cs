using Domain.OverboardChess.Base;
using System.Linq.Expressions;

namespace Aplication.OverboardChess.Abstractions
{
    public interface IRepository<T> where T : AggregateRoot
    {
        Task<T> InsertAsync(T item);
        Task UpdateAsync(T item);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetListAsync(List<Guid> ids);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression, int take = 10, int skip = 0);
        Task<long> GetCount(Expression<Func<T, bool>> expression);
        Task<bool> Any(Expression<Func<T, bool>> expression);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> expression);
        Task<long> DeleteManyAsync(Expression<Func<T, bool>> expression);
    }

    public static class RepositoryExtensions
    {
        public static async Task<T> GetAsync<T>(this IRepository<T> repository, Guid id)
            where T : AggregateRoot
        {
            return await repository.GetAsync(t => t.Id == id);
        }
    }
}