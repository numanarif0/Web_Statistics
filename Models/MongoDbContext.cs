using Microsoft.Extensions.Options;
using MongoDB.Driver;
using YokIstatistikWeb.Models;

namespace YokIstatistikWeb.Models
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<Universite> GetCollectionForYear(string year)
        {
            string collectionName = $"YOI_ogretim_elemani_akademik_gorev_sayilari_{year}";
            return _database.GetCollection<Universite>(collectionName);
        }

        // Default collection for backward compatibility
        public IMongoCollection<Universite> Universiteler =>
            _database.GetCollection<Universite>("YOI_ogretim_elemani_akademik_gorev_sayilari_2024_2025");
    }
}