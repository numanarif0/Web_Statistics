using Microsoft.Extensions.Options;
using MongoDB.Driver;
using YokIstatistikWeb.Models;

namespace YokIstatistikWeb.Models // namespace'i projene göre ayarla
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Universite> Universiteler =>
            _database.GetCollection<Universite>("universiteler"); // koleksiyon adın buysa
    }
}
