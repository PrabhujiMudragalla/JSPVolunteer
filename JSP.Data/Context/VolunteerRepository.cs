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
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly VolunteerContext _context = null;

        public VolunteerRepository(IOptions<Settings> settings)
        {
            _context = new VolunteerContext(settings);
        }

        public async Task<IEnumerable<Volunteer>> GetAllVolunteers()
        {
            try
            {
                return await _context.Volunteers.Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<Volunteer> GetVolunteer(string id)
        {
            var filter = Builders<Volunteer>.Filter.Eq("Id", id);

            try
            {
                return await _context.Volunteers
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task AddVolunteer(Volunteer item)
        {
            try
            {
                await _context.Volunteers.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> RemoveVolunteer(string id)
        {
            try
            {
                DeleteResult actionResult = await _context.Volunteers.DeleteOneAsync(
                     Builders<Volunteer>.Filter.Eq("Id", id));

                return actionResult.IsAcknowledged 
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateVolunteer(string id, Data.Model.Volunteer value)
        {
            var filter = Builders<Volunteer>.Filter.Eq(s => s.Id, id);
            var update = Builders<Volunteer>.Update
                            .Set(s => s.MembershipID, value.MembershipID)
                            .Set(s => s.MobileNumber, value.MobileNumber)
                            .Set(s => s.Qualification, value.Qualification)
                            .Set(s => s.ResumeUrl, value.ResumeUrl)
                            .Set(s => s.Accomplishments, value.Accomplishments)
                            .Set(s => s.AreaOfExpertise, value.AreaOfExpertise)
                            .Set(s => s.AreaOfSpecialisation, value.AreaOfSpecialisation)
                            .Set(s => s.Constituency, value.Constituency)
                            .Set(s => s.CoreSkills, value.CoreSkills)
                            .Set(s => s.UpdatedOn, value.UpdatedOn)
                            .Set(s => s.District, value.District)
                            .Set(s => s.Email, value.Email)
                            .Set(s => s.LikeToSpend, value.LikeToSpend)
                            .CurrentDate(s => s.UpdatedOn);

            try
            {
                UpdateResult actionResult = await _context.Volunteers.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> UpdateNote(string id, Volunteer item)
        {
            try
            {
                ReplaceOneResult actionResult = await _context.Volunteers
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
        public async Task<bool> UpdateVolunteerDocument(string id, Data.Model.Volunteer value)
        {
            var item = await GetVolunteer(id) ?? new Volunteer();
            item.MembershipID = value.MembershipID;
            item.MobileNumber = value.MobileNumber;
            item.Qualification = value.Qualification;
            item.ResumeUrl = value.ResumeUrl;
            item.Accomplishments = value.Accomplishments;
            item.AreaOfExpertise = value.AreaOfExpertise;
            item.AreaOfSpecialisation = value.AreaOfSpecialisation;
            item.Constituency = value.Constituency;
            item.CoreSkills = value.CoreSkills;
            item.UpdatedOn = value.UpdatedOn;
            item.District = value.District;
            item.Email = value.Email;
            item.LikeToSpend = value.LikeToSpend;
            item.UpdatedOn = DateTime.Now;

            return await UpdateNote(id, item);
        }

        public async Task<bool> RemoveAllVolunteers()
        {
            try
            {
                DeleteResult actionResult = await _context.Volunteers.DeleteManyAsync(new BsonDocument());

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
