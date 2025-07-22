using MongoDB.Driver;
using Microsoft.Extensions.Options;
using YokIstatistikWeb.Models;

namespace YokIstatistikWeb.Services
{
    public class UniversiteService
    {
        private readonly IMongoCollection<Universite> _collection;

        public UniversiteService(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<Universite>(settings.Value.CollectionName);
        }

        public List<Universite> GetAll()
        {
            return _collection.Find(_ => true).ToList();
        }
    }
}
