namespace JobPortal.Application.Features.Reviews.Dtos
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string JobSeekerFirstName { get; set; }
        public string JobSeekerLastName { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
