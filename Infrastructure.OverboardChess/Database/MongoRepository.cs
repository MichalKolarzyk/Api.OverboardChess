using Aplication.OverboardChess.Abstractions;
using Domain.OverboardChess.Base;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infrastructure.OverboardChess.Database
{
    public class MongoRepository<T> : IRepository<T> 
        where T : AggregateRoot
    {
        protected readonly IMongoCollection<T> _mongoCollection;

        public MongoRepository(MongoDatabase database)
        {
            _mongoCollection = database.GetCollection<T>();
        }

        public async Task<T> InsertAsync(T item)
        {
            await _mongoCollection.InsertOneAsync(item);
            return item;
        }

        public async Task UpdateAsync(T item)
        {
            await _mongoCollection.ReplaceOneAsync((itemToReplace) => itemToReplace.Id == item.Id, item);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _mongoCollection.Find(expression).FirstOrDefaultAsync();
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression, int take = 10, int skip = 0)
        {
            return await _mongoCollection.Find(expression).Limit(take).Skip(skip).ToListAsync();
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression)
        {
            return await _mongoCollection.Find(expression).ToListAsync();
        }

        public async Task<List<T>> GetListAsync(List<Guid> ids)
        {
            var filterDef = new FilterDefinitionBuilder<T>();
            var filter = filterDef.In(x => x.Id, ids);
            return await _mongoCollection.Find(filter).ToListAsync();
        }

        public async Task<long> GetCount(Expression<Func<T, bool>> expression)
        {
            return await _mongoCollection.Find(expression).CountDocumentsAsync();
        }

        public async Task<bool> Any(Expression<Func<T, bool>> expression)
        {
            return await _mongoCollection.Find<T>(expression).FirstOrDefaultAsync() != null;
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            var reuslt = await _mongoCollection.DeleteOneAsync<T>(expression);
            return reuslt.DeletedCount > 0;
        }

        public async Task<long> DeleteManyAsync(Expression<Func<T, bool>> expression)
        {
            var result = await _mongoCollection.DeleteManyAsync<T>(expression);
            return result.DeletedCount;
        }
    }
}
