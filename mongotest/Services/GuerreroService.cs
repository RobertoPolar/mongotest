using Microsoft.Extensions.Options;
using MongoDB.Driver;
using mongotest.Models;

namespace mongotest.Services
{
    public class GuerreroService
    {
        private readonly IMongoCollection<Guerrero> _guerreroCollection;

        public GuerreroService(IOptions<ConexionSettings> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
            bookStoreDatabaseSettings.Value.Server);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.Database);

            _guerreroCollection = mongoDatabase.GetCollection<Guerrero>(
                bookStoreDatabaseSettings.Value.Colection);
        }

        public List<GuerreroList> Get()
        {
            var guerreros = _guerreroCollection.Find(x => true).ToList();

            List<GuerreroList> result = new List<GuerreroList>();

            foreach(var guerrero in guerreros)
            {
                result.Add(new GuerreroList(guerrero));
            }

            return result;
        }

        public void Create(Guerrero guerrero)
        {
            _guerreroCollection.InsertOne(guerrero);
        }

        public Guerrero GetByID(string id)
        {
            return _guerreroCollection.Find(x => x.Id == id).FirstOrDefault();
        }

        public void Delete(string id)
        {
            _guerreroCollection.DeleteOne(x => x.Id == id);
        }

        public void Update(Guerrero guerrero)
        {
            _guerreroCollection.ReplaceOne(x => x.Id == guerrero.Id, guerrero);
        }
    }
}
