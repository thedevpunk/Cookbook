using Core.Entities;
using MongoDB.Driver;

namespace Infastructure.Data
{
    public class CookbookContext
    {
        private IMongoDatabase _database;

        public CookbookContext(string connectionString)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("Cookbook");
        }

        public IMongoCollection<Recipe> Recipes => _database.GetCollection<Recipe>("Recipes");

        public IMongoCollection<Category> Categories => _database.GetCollection<Category>("Categories");

        public IMongoCollection<Core.Entities.Tag> Tags => _database.GetCollection<Core.Entities.Tag>("Tags");

        public IMongoCollection<Group> Groups => _database.GetCollection<Group>("Groups");
    }
}