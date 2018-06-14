using Microsoft.Extensions.Options;
using MongoDB.Driver;
using JSP.Data.Model;

namespace JSP.Data.Context
{
    public class VolunteerContext
    {
        private readonly IMongoDatabase _database = null;

        public VolunteerContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Volunteer> Volunteers
        {
            get
            {
                return _database.GetCollection<Volunteer>("Volunteer");
            }
        }

        public IMongoCollection<Qualification> Qualifications
        {
            get
            {
                return _database.GetCollection<Qualification>("Qualification");
            }
        }
    }
}
