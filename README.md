This API serves as an example of Clean Architecture that is feature-driven. It leverages versioning, role-based authorization, generic controllers and repositories, Dependency Injection and SOLID principles. It also includes some of my old refactored work. Features are tested using unit tests.
# Types of classes

**Entity:** Corresponds to one or more database tables and is used by the implemented ORM. It inherits from the `Entity` (or `EntityWithUser`) class.

**Model:** Can correspond to an entity. In that case, it is mapped to it with `AutoMapper`. If not, there is no relation to DB. It inherits from the `Model` class. It is used by the controller. For entity-related models, there are three types of models:

- Input model: Model used to create or update a database row. Its name ends with `InputModel`.
- Output model: Model used to get a database row. Its name ends with `OutputModel`.
- Two-way model: Input and output model in one. Its name ends with `Model`.

**Service:** Contains specific algorithms and is registered in the DI container using its corresponding interface.

**Repository:** A specific service that contains CRUD operations (not necessarily all of them) for entities and optionally models, and is registered in the DI container using its corresponding interface.
# Projects
## Domain
Consists of entities (and their abstractions), enumerations and constants. It has no reference to another project or external library.
## Application
Contains business logic that is independent of external libraries. It references the `Domain` project.

Important folders:
- `Features`: Functionality of the API. They typically consist of models, services and abstractions of repositories.
- `Services`: Abstractions of services that are not used as standalone features.
- `GenericRepositories`: Abstractions of repositories that contain CRUD operations and can be used universally by simple entities (i.e., entities without one-to-many or many-to-many relationships).
- `Helpers`: Static helper classes and methods.
## Infrastructure
It uses external libraries to implement abstractions defined in the `Application` project, which it references, and interacts with the SQL Server database using Entity Framework (code first). It also handles authentication (ASP<i></i>.NET Core Identity) and mapping using `AutoMapper`.
## WebApiExample
This is an ASP<i></i>.NET Core Web API project that consists of controllers. These can inherit from generic controllers. It references both `Application` and `Infrastructure` projects in case one of them needs to be replaced by another project.
## UnitTests
This test project uses `MSTest` and `FluentAssertions` to test implementations of most services. If a service has a dependency that needs to be mocked, `Moq` is used to do so. If the dependency is a repository, Enitity Framework's in-memory database is used with data stored in CSV files as embedded resources.
