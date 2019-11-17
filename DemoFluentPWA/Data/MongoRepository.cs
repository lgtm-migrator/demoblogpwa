using DemoFluentPWA.ServiceModel;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoFluentPWA.Data
{
    public class MongoRepository<T> where T : class
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
            var findfluent = await itemCollection.FindAsync(Builders<T>.Filter.Empty).ConfigureAwait(false);
            return findfluent.ToEnumerable();
        }

        public async Task<T> FindByIdAsync(string id)
        {
            var FindAsync = await itemCollection.FindAsync(Builders<T>.Filter.Eq("_id", id));
            return await FindAsync.FirstAsync().ConfigureAwait(false);
        }
    }
}
