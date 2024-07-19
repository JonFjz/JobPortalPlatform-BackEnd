namespace JobPortal.Domain.Entities
{
    public class Education
    {
        public int Id { get; set; }
        public string Degree { get; set; }
        public string FieldOfStudy { get; set; }
        public string InstitutionName { get; set; }

        public int JobSeekerId { get; set; }
        public JobSeeker JobSeeker { get; set; }
    }
}
