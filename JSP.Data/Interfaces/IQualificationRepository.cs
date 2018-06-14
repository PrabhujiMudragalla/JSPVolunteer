using System.Collections.Generic;
using System.Threading.Tasks;
using JSP.Data.Model;
using MongoDB.Driver;

namespace JSP.Data.Interfaces
{
    public interface IQualificationRepository
    {
        Task<IEnumerable<Qualification>> GetAllQualifications();
        Task<Qualification> GetQualification(string id);

        // add new note document
        Task AddQualification(Qualification item);

        // remove a single document / note
        Task<bool> RemoveQualification(string id);

        // update just a single document / note
        Task<bool> UpdateQualification(string id, Data.Model.Qualification body);

        // demo interface - full document update
        Task<bool> UpdateQualificationDocument(string id, Data.Model.Qualification body);

        // should be used with high cautious, only in relation with demo setup
        Task<bool> RemoveAllQualifications();
    }
}
