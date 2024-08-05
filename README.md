Job Posting Management System
A comprehensive system inspired by platforms like LinkedIn, Indeed, and Glassdoor, designed to manage job postings, user profiles, applications, and more.

Table of Contents
Introduction
Features
Technologies Used
Architecture
Installation
Configuration
Usage
API Documentation
Contributing
License
Contact


Introduction
This project is a job posting management system that allows users to manage job postings, user profiles, applications, and more. It is designed to provide advanced search and filtering, resume uploads, notifications, employer reviews, job recommendations, analytics, bookmarking jobs, interview scheduling, content moderation, payment integration, and real-time updates.
Features:
Job
Posting Management
User
Profiles
Application
Management
Advanced
Search and Filters
Resume
Uploads
Notifications
Employer
Reviews
Job
Recommendations


Technologies Used
Backend:
     .NET, C#, ASP.NET Core
Database:
     mssql, Redis, Elasticsearch
Authentication:
     JWT, auth0 
API
     Documentation: Swashbuckle.AspNetCore (Swagger)
Messaging:
     RabbitMQ
Mapping:
     AutoMapper
Scheduler:
     Google Calendar API
Containerization:
     Docker


Architecture
The project follows a clean architecture structure with the following layers:
Application
Common
Domain
Infrastructure
Persistence
API
It uses CQRS (Command Query Responsibility Segregation) for handling requests and actions.


Installation
To install the project, follow these steps:
Clone the repository:
bash
Copy code
git clone https://github.com/JonFjz/JobPortalPlatform-BackEnd.git
Navigate to the project directory:
bash
Copy code
cd job-posting-management-system
Build the project:
bash
Copy code
dotnet build
Run the Docker containers:
bash
Copy code
docker-compose up -d
Configuration
Database: Configure your PostgreSQL connection strings in the appsettings.json file.
Redis: Ensure Redis is running on localhost:6379.
Elasticsearch: Set up Elasticsearch for data storage and indexing.
Google Calendar API: Configure API keys and OAuth credentials for Google Calendar.
RabbitMQ: Configure RabbitMQ for messaging.
Usage
Start the application:
bash
Copy code
dotnet run
Access the API documentation at:
text
Copy code
http://34.159.188.181:8080/swagger/index.html
API Documentation
API endpoints and their usage are documented using Swagger. You can access the Swagger UI at:
text
Copy code
http://34.159.188.181:8080/swagger/index.html
Contributing
Contributions are welcome! Please follow these steps:
Fork the repository.
Create a new branch:
bash
Copy code
git checkout -b feature/your-feature
Commit your changes:
bash
Copy code
git commit -m 'Add your feature'
Push to the branch:
bash
Copy code
git push origin feature/your-feature
Open a pull request.
