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
    public class QualificationsController : Controller
    {
        private readonly IQualificationRepository _qualificationRepository;

        public QualificationsController(IQualificationRepository qualificationRepository)
        {
            _qualificationRepository = qualificationRepository;
        }

        [NoCache]
        [HttpGet]
        public Task<IEnumerable<Data.Model.Qualification>> Get()
        {
            return GetQualificationInternal();
        }

        private async Task<IEnumerable<Data.Model.Qualification>> GetQualificationInternal()
        {
            return await _qualificationRepository.GetAllQualifications();
        }

        // GET api/Qualifications/5
        [HttpGet("{id}")]
        public Task<Data.Model.Qualification> Get(string id)
        {
            return GetQualificationByIdInternal(id);
        }

        private async Task<Data.Model.Qualification> GetQualificationByIdInternal(string id)
        {
            return await _qualificationRepository.GetQualification(id) ?? new Data.Model.Qualification();
        }

        // POST api/Qualifications
        [HttpPost]
        public void Post([FromBody]Data.Model.Qualification value)
        {
            value.CreatedOn = DateTime.Now;
            value.UpdatedOn = DateTime.Now;
            _qualificationRepository.AddQualification(value);
        }

        // PUT api/Qualifications/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]Data.Model.Qualification value)
        {
            _qualificationRepository.UpdateQualificationDocument(id, value);
        }

        // DELETE api/Qualifications/23243423
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _qualificationRepository.RemoveQualification(id);
        }
    }
}
