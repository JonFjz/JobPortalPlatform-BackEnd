using System.Text.Json.Serialization;

namespace JobPortal.Domain.Entities
{
    public class JobSeeker
    {
        public int Id { get; set; }
        public string Auth0Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }

        [JsonIgnore]
        public Resume Resume { get; set; }


        public ICollection<Skill> Skills { get; set; }
        public ICollection<WorkExperience> Experiences { get; set; }
        public ICollection<Education> Educations { get; set; }
    }
}
