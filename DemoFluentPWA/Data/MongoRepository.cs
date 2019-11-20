using DemoFluentPWA.Models;
using DemoFluentPWA.ServiceModel;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoFluentPWA.Data
{
    public class MongoRepository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> itemCollection;

        public MongoRepository(MongoConnectionSettings settings)
        {
            IMongoClient client = new MongoClient(settings.ConnectionString);
            IMongoDatabase db = client.GetDatabase(settings.Database);
            itemCollection = db.GetCollection<T>(settings.CollectionName);
        }

        public async Task InsertOneAsync(T item) => await itemCollection.InsertOneAsync(item).ConfigureAwait(false);

        public async Task InsertManyAsync(IEnumerable<T> items) => await itemCollection.InsertManyAsync(items).ConfigureAwait(false);

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var items = itemCollection.Find(Builders<T>.Filter.Empty);
            return await Task.FromResult<IEnumerable<T>>(items.ToEnumerable()).ConfigureAwait(false);
        }

        public async Task<T> FindByIdAsync(string id)
        {
            var FindAsync = await itemCollection.FindAsync(Builders<T>.Filter.Eq("_id", id));
            return await FindAsync.FirstAsync().ConfigureAwait(false);
        }

        public async Task<bool> DeleteOneAsync(string id)
        {
            var deleteOp = await itemCollection.DeleteOneAsync(Builders<T>.Filter.Eq("_id", id)).ConfigureAwait(false);
            return deleteOp.IsAcknowledged;
        }
    }

    public class MongoItemRepo : MongoRepository<ItemModel>
    {
        public MongoItemRepo(MongoConnectionSettings settings) : base(settings) { }
    }

    public class MongoPushRepo : MongoRepository<Devices>
    {
        public MongoPushRepo(MongoConnectionSettings settings) : base(settings) { }
    }
}
