This API serves as an example of my own feature-driven architecture, using versioning, role-based authorization, generic controllers and repositories, Entity Framework (code first), AutoMapper, Dependency Injection and SOLID principles. It also includes some of my old refactored work. Features are tested with unit tests (MSTest) using mock objects (Moq) and EF's in-memory database.

## Types of classes

**Entity:** Corresponds to one or more DB tables and is used by Entity Framework. It inherits from the "Entity" (or "EntityWithUser") class. If it doesn't have a corresponding model, it is used by the controller directly.

**Model:** Can correspond to an entity. In that case, it is mapped to it with AutoMapper. If not, there is no relation to DB. It inherits from the "Model" class. It is used by the controller.

**Service:** Contains specific algorithms and is registered in the DI container using its corresponding interface.

**Repository:** A specific service that contains CRUD operations (not necessarily all of them) for entities and optionally models, and is registered in the DI container using its corresponding interface.

## Folder structure

**EntityInterfaces:** Interfaces intended to be implemented by entities. They contain additional DB columns.

**Enums:** Enumerations that can be shared across various features.

**Extensions:** Extension methods.

**Features:** Folders that represent features. Each feature consists of one or more controllers and optionally entities, models, services or repositories, and is independent of other features. Features are versioned, other parts of the API can be versioned as well if needed.

**GenericControllers:** Controllers that can be inherited from by other controllers and contain CRUD operations for the type parameter.

**GenericRepositories:** Repositories that can be inherited from by other repositories and contain CRUD operations for the type parameter. Each repository interface needs at least one type parameter as the model, implementations also need corresponding entity that is mapped to the model and vice-versa. This way, the class that uses a repository through its interface is independent of entities and those can be easily replaced or changed without affecting the calling class.

**Helpers:** Static helper methods.

**Migrations:** Entity Framework migrations.

**RepositoryInterfaces:** Interfaces with CRUD operations that can be implemented by repositories. Each interface has one method.

**SharedServices:** Services that can be used by one or more features.
