This API serves as an example of Clean Architecture (CA) that is feature-driven. It leverages versioning, custom role-based authorization with JWT, generic controllers and repositories, Dependency Injection and SOLID principles. Its purpose is to show some of my work in one solution. Features are tested with unit and integration tests.
# Types of classes

**Entity:** Corresponds to one or more database tables and is used by the implemented ORM. It inherits from the `Entity` (or `EntityWithUser`) class.

**Model:** Can correspond to an entity. If not, there is no relation to DB. It inherits from the `Model` class. It is used by the controller.

**Service:** Contains specific algorithms and is registered in the DI container using its corresponding interface.

**Provider:** A type of service that returns (gets) data.

**Repository:** A specific service that contains CRUD operations (not necessarily all of them) for entities and optionally models, and is registered in the DI container using its corresponding interface.

**Operation:** In this project, only database operations are used.
# Projects
## Domain
Consists of entities (and their abstractions), enumerations and constants. It has no reference to any other project or external library.
## Application
Contains business logic that is independent of external libraries. It references the `Domain` project.

Important folders:
- `Features`: Functionality of the API. They typically consist of models, services and abstractions of repositories.
- `Common\RepositoryInterfaces`: Interfaces that are implemented by the abstractions of repositories so that they don't have to be written as one fat interface whose methods are the same as elsewhere.
- `Services`: Abstractions of services that are not used as standalone features.
- `Helpers`: Static helper classes and methods. These cannot be mocked in tests.
## Infrastructure
It uses external libraries to implement abstractions defined in the `Application` project, which it references, and interacts with the SQL Server database using Entity Framework (code first). It also handles authentication (ASP<i></i>.NET Core Identity).

Important folders:
- `DatabaseOperations`: Generic classes and interfaces with an entity and optionally a model as type parameter(s). They contain queries such as `INSERT` OR `UPDATE`, written using Entity Framework.
- `Features`: Implementations of repositories.
- `Migrations`: Versions of database schema.
- `Services`: Implementations of services.
## WebApiExample
This is an ASP<i></i>.NET Core Web API project that consists of controllers. It references both `Application` and `Infrastructure` projects in case one of them needs to be replaced by another project.
## Test projects
All test projects are stored in the `Tests` folder. Each project corresponds to one of the CA layers and can contain either unit tests or integration tests. They all use `xUnit` and `FluentAssertions`. If a tested service has a dependency that needs to be mocked, `Moq` is used to do so. If a service needs data from DB but it's not an integration test, the Entity Framework's in-memory database is used with data stored in CSV files as embedded resources.

Integration tests use `Respawn` and for each test a DB is created and deleted.
