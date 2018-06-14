using System.Collections.Generic;
using System.Threading.Tasks;
using JSP.Data.Model;
using MongoDB.Driver;

namespace JSP.Data.Interfaces
{
    public interface IVolunteerRepository
    {
        Task<IEnumerable<Volunteer>> GetAllVolunteers();
        Task<Volunteer> GetVolunteer(string id);

        // add new note document
        Task AddVolunteer(Volunteer item);

        // remove a single document / note
        Task<bool> RemoveVolunteer(string id);

        // update just a single document / note
        Task<bool> UpdateVolunteer(string id, Data.Model.Volunteer body);

        // demo interface - full document update
        Task<bool> UpdateVolunteerDocument(string id, Data.Model.Volunteer body);

        // should be used with high cautious, only in relation with demo setup
        Task<bool> RemoveAllVolunteers();
    }
}
