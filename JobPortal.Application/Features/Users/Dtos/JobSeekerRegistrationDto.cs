﻿namespace JobPortal.Application.Features.Users.Dtos
{
    public class JobSeekerRegistrationDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
