using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using JSP.Data.Interfaces;
using JSP.Data.Model;
using JSP.Volunteer.Infrastructure;
using System;
using System.Collections.Generic;

namespace JSP.Volunteer.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class VolunteersController : Controller
    {
        private readonly IVolunteerRepository _volunteerRepository;

        public VolunteersController(IVolunteerRepository volunteerRepository)
        {
            _volunteerRepository = volunteerRepository;
        }

        [NoCache]
        [HttpGet]
        public Task<IEnumerable<Data.Model.Volunteer>> Get()
        {
            return GetVolunteerInternal();
        }

        private async Task<IEnumerable<Data.Model.Volunteer>> GetVolunteerInternal()
        {
            return await _volunteerRepository.GetAllVolunteers();
        }

        // GET api/volunteers/5
        [HttpGet("{id}")]
        public Task<Data.Model.Volunteer> Get(string id)
        {
            return GetVolunteerByIdInternal(id);
        }

        private async Task<Data.Model.Volunteer> GetVolunteerByIdInternal(string id)
        {
            return await _volunteerRepository.GetVolunteer(id) ?? new Data.Model.Volunteer();
        }

        // POST api/volunteers
        [HttpPost]
        public void Post([FromBody]Data.Model.Volunteer value)
        {
            value.CreatedOn = DateTime.Now;
            value.UpdatedOn = DateTime.Now;
            _volunteerRepository.AddVolunteer(value);
        }

        // PUT api/volunteers/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]Data.Model.Volunteer value)
        {
            _volunteerRepository.UpdateVolunteerDocument(id, value);
        }

        // DELETE api/volunteers/23243423
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _volunteerRepository.RemoveVolunteer(id);
        }
    }
}
