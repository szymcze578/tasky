# Tasky
 
Tasky is a web application for managing tasks, built as a personal project. A user creates an account, logs in, and creates boards that hold tasks. A board has one of two types: **List**, a simple checklist, and **Kanban**, where tasks move between columns that correspond to statuses. A task has a title, description, status, estimate, due date, completion date, position on the board and, optionally, a responsible user. Each user sees only their own boards, and an attempt to access someone else's board returns a 404 response.
 
The project consists of a client application (SPA) and a REST API that communicate over HTTP using JSON. The API contract is described in OpenAPI, and the TypeScript types in the frontend are generated from it, so a change to the backend model is immediately visible to the frontend compiler.
 
## Technologies
 
**Backend**
 
- .NET and ASP.NET Core Web API
- Entity Framework Core with PostgreSQL (Npgsql)
- JWT authentication, password hashing with the ASP.NET Core Identity `PasswordHasher`, rate limiting on the login endpoint
- Swagger / OpenAPI (Swashbuckle)
- xUnit

**Frontend**
 
- Vue 3, TypeScript and Vite
- Pinia (application state) and Vue Router
- Tailwind CSS 4
- `openapi-typescript` (type generation from OpenAPI) and `openapi-fetch` (HTTP client)
- Vitest

**Infrastructure and tooling**
 
- PostgreSQL
- GitHub Actions (backend build and tests)
- draw.io (diagrams)
## Architecture
 
The backend consists of four projects arranged according to Clean Architecture. Dependencies point inward: API depends on Application, Application depends on Domain, and Domain depends on nothing. Infrastructure depends on Application because it implements its interfaces.
 
<p align="center">
  <img src="Resources/Tasky%20System%20Architecture.png" alt="System architecture">
</p>

 
- **Tasky.Domain**: entities, a value object, business rules and domain exceptions. No external dependencies.
- **Tasky.Application**: application services (use cases), DTOs, mapping and interfaces (repositories, password hashing, token generator, `ICurrentUser`). No CQRS or MediatR, because plain services are enough for simple CRUD.
- **Tasky.Infrastructure**: EF Core (`DbContext`, entity configurations, migrations), repositories, password hashing and JWT tokens. Only this layer knows about PostgreSQL.
- **Tasky.API**: controllers, middleware that turns exceptions into `ProblemDetails` responses, JWT, CORS, rate limiting and dependency registration in `Program.cs`. The dashed arrow in the diagram means that the API references Infrastructure only to register implementations in DI.
Endpoints are organized around resources: `/api/auth`, `/api/users/me`, `/api/boards` and `/api/boards/{boardId}/tasks`. The user's identity comes from the token, and accessing another user's board results in a 404 response.
 
## DDD approach
 
The model uses the tactical patterns of Domain-Driven Design within a single bounded context (task management), without domain events.
 
- **Aggregates.** The root of the main aggregate is `BoardEntity`, which owns its tasks: the `_tasks` collection is private, and adding, changing the status of and removing tasks goes through board methods (`Create` and `ChangeStatus` in `TaskEntity` are `internal`). The second aggregate is `UserEntity`.
- **Invariants.** Business rules live in the entities, not in controllers or services. For example, on a List board a task can only have the status `ToDo` or `Completed`. An invalid state is rejected with a domain exception.
- **Factories and encapsulation.** Entities are created through static `Create` methods, have private constructors and setters, and their state changes through named operations (`Rename`, `ChangeEmail`, `Deactivate`).
- **Value object.** `Address` validates its fields in the constructor and is changed by replacing the whole object. In the database it maps to the `Address_*` columns (`OwnsOne`).
- **Repositories.** One per aggregate, returning it in full. The interfaces live in Application, so the domain does not depend on EF Core.
## Database schema
 
The database runs on PostgreSQL, and the schema is maintained with EF Core migrations. The model consists of three tables: `Users`, `Boards` and `BoardTasks`.
 
 <p align="center">
  <img src="Resources/Tasky%20DB%20diagram.png" alt="Database schema" width="700">
</p>
 

**Relationships**
 
- A user owns many boards (`Boards.OwnerId` → `Users.UserId`). Deleting a user who has boards is blocked (`Restrict`).
- A board contains many tasks (`BoardTasks.BoardId` → `Boards.BoardId`). Deleting a board deletes its tasks (`Cascade`).
- A user can be responsible for many tasks (`BoardTasks.ResponsibleId` → `Users.UserId`). The relationship is optional, and deleting a user detaches them from their tasks (`SetNull`).