using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

using JSP.Data.Interfaces;
using JSP.Data.Model;
using MongoDB.Bson;

namespace JSP.Data.Context
{
    public class QualificationRepository : IQualificationRepository
    {
        private readonly VolunteerContext _context = null;

        public QualificationRepository(IOptions<Settings> settings)
        {
            _context = new VolunteerContext(settings);
        }

        public async Task<IEnumerable<Qualification>> GetAllQualifications()
        {
            try
            {
                return await _context.Qualifications.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Qualification> GetQualification(string id)
        {
            var filter = Builders<Qualification>.Filter.Eq("Id", id);

            try
            {
                return await _context.Qualifications
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task AddQualification(Qualification item)
        {
            try
            {
                await _context.Qualifications.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveQualification(string id)
        {
            try
            {
                DeleteResult actionResult = await _context.Qualifications.DeleteOneAsync(
                     Builders<Qualification>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged 
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateQualification(string id, Data.Model.Qualification value)
        {
            var filter = Builders<Qualification>.Filter.Eq(s => s.Id, id);
            var update = Builders<Qualification>.Update
                            .Set(s => s.Name, value.Name)
                            .CurrentDate(s => s.UpdatedOn);

            try
            {
                UpdateResult actionResult = await _context.Qualifications.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> Update(string id, Qualification item)
        {
            try
            {
                ReplaceOneResult actionResult = await _context.Qualifications
                                                .ReplaceOneAsync(n => n.Id.Equals(id)
                                                                , item
                                                                , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // Demo function - full document update
        public async Task<bool> UpdateQualificationDocument(string id, Data.Model.Qualification value)
        {
            var item = await GetQualification(id) ?? new Qualification();
            item.Name = value.Name;
            item.UpdatedOn = DateTime.Now;

            return await Update(id, item);
        }

        public async Task<bool> RemoveAllQualifications()
        {
            try
            {
                DeleteResult actionResult = await _context.Qualifications.DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }
}
