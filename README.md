Event Ticket System (ASP.NET Core)

A backend-focused Event Ticket System API built with ASP.NET Core, Entity Framework Core, and ASP.NET Identity.
The project demonstrates clean architecture, authentication, authorization, and real-world business logic for purchasing event tickets.

This project is designed as a learning-focused but production-structured backend service, suitable for integration with a frontend client.

-Features
-Authentication & Authorization
User registration and login using ASP.NET Identity
JWT-based authentication
Role-ready design (User, Admin)
Secure access to protected endpoints using [Authorize]

-Events
Create and manage events
Events contain:
Name
Description
Date & location
Total available tickets
Ticket price
Events cannot be purchased after they occur

-Ticket Purchasing
Authenticated users can purchase tickets for events
Validations include:
User must be authenticated
Event must exist
Event must be in the future
Requested quantity must be available
Ticket purchases:
Store price paid at purchase time (historical accuracy)
Track quantity per purchase
Track purchase timestamp
Availability is computed dynamically from sold tickets (no mutable counters)

-Architecture
Clean separation of concerns:
Controllers
Services
DTOs
Data layer
DTO-based API contracts
AutoMapper used for entity ↔ DTO mapping
Identity integrated directly into the EF Core DbContext

-Tech Stack
.NET 9
ASP.NET Core Web API
Entity Framework Core
SQL Server
ASP.NET Identity
JWT Authentication
AutoMapper
Swagger / OpenAPI

📁 Project Structure (High Level)
EventTicketSystem
│
├── Controllers
├── Services
│   ├── AuthServices
│   ├── EventServices
│   └── TicketServices
├── Models
├── DTOs
├── MappingProfiles
├── Data
└── Program.cs

-Authentication Flow (JWT)
User registers or logs in
API returns a JWT token
Token is sent via HTTP header:
Authorization: Bearer <token>
Protected endpoints use [Authorize]

-Example Endpoints
POST /api/auth/register
POST /api/auth/login
GET /api/auth/me (authenticated)
GET /api/events
POST /api/tickets/purchase (authenticated)

-What This Project Demonstrates
Real-world backend validation logic
Secure authentication flows
Proper use of Identity + JWT together
EF Core relationship modeling
Clean service-based architecture
Defensive programming with meaningful exceptions

-Learning Focus
This project was built as part of a structured backend learning path, focusing on:
Writing maintainable service logic
Understanding authentication and authorization deeply
Avoiding common pitfalls (over-trusting claims, mutable counters, etc.)
Building APIs that are frontend-ready

-Not Included (Yet)
Payment provider integration
Ticket refunds
Admin UI
Concurrency handling (planned extension)

-Future Improvements
Optimistic concurrency for ticket purchases
Admin role-based event management
Pagination & filtering
Integration tests
Frontend client
