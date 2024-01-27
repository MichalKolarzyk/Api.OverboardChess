using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Infrastructure.OverboardChess.Database
{
    public class MongoDatabase
    {
        private readonly IOptions<MongoDbSettings> _settings;
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;

        static MongoDatabase()
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeSerializer(BsonType.DateTime));
        }

        public MongoDatabase(IOptions<MongoDbSettings> settings)
        {
            _settings = settings;
            _client = new MongoClient(settings.Value.ConnectionString);
            _database = _client.GetDatabase(_settings.Value.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>()
        {
            return _database.GetCollection<T>(typeof(T).Name);
        }
    }
}
