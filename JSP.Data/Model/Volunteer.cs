using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JSP.Data.Model
{
    public class Volunteer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string MembershipID { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string MobileNumber { get; set; } = string.Empty;

        public string District { get; set; } = string.Empty;

        public string Constituency { get; set; } = string.Empty;

        public string Qualification { get; set; } = string.Empty;

        public string AreaOfSpecialisation { get; set; } = string.Empty;

        public string AreaOfExpertise { get; set; } = string.Empty;

        public string CoreSkills { get; set; } = string.Empty;

        public string LikeToSpend { get; set; } = string.Empty;

        public string ResumeUrl { get; set; } = string.Empty;

        public string Accomplishments { get; set; } = string.Empty;

        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int UserId { get; set; } = 0;
    }
}
