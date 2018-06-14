using System;
using Microsoft.AspNetCore.Mvc;

using JSP.Data.Interfaces;
using JSP.Data.Model;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace JSP.Volunteer.Controllers
{
    [Route("api/[controller]")]
    public class SystemController : Controller
    {
        private readonly IVolunteerRepository _volunteerRepository;

        public SystemController(IVolunteerRepository volunteerRepository)
        {
            _volunteerRepository = volunteerRepository;
        }

        // Call an initialization - api/system/init
        [HttpGet("{setting}")]
        public string Get(string setting)
        {
            if (setting == "init")
            {
                _volunteerRepository.RemoveAllVolunteers();
                _volunteerRepository.AddVolunteer(new Data.Model.Volunteer() { Id = "1", MembershipID = "Test note 1", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, UserId = 1 });
                _volunteerRepository.AddVolunteer(new Data.Model.Volunteer() { Id = "2", MembershipID = "Test note 2", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, UserId = 1 });
                _volunteerRepository.AddVolunteer(new Data.Model.Volunteer() { Id = "3", MembershipID = "Test note 3", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, UserId = 2 });
                _volunteerRepository.AddVolunteer(new Data.Model.Volunteer() { Id = "4", MembershipID = "Test note 4", CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now, UserId = 2 });

                return "Database NotesDb was created, and collection 'Notes' was filled with 4 sample items";
            }

            return "Unknown";
        }
    }
}
