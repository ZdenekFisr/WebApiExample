This API serves as an example of my own feature-driven architecture, using authentication, generic controllers and repositories, Entity Framework (code first), AutoMapper, Dependency Injection and SOLID principles. It also includes some of my old refactored work.


Types of classes:


Entity: Corresponds to one or more DB tables and is used by Entity Framework. It inherits from the "Entity" (or "EntityWithUser") class. If it doesn't have a corresponding model, it is used by the controller directly.

Model: Can correspond to an entity. In that case, it is mapped to it with AutoMapper. If not, there is no relation to DB. It inherits from the "Model" class. It is used by the controller.

Service: Contains specific algorithms and is registered in the DI container using its corresponding interface.

Repository: A specific service that contains CRUD operations (not necessarily all of them) for entities and optionally models, and is registered in the DI container using its corresponding interface.


Folder structure:


EntityInterfaces: Interfaces intended to be implemented by entities. They contain additional DB columns.

Enums: Enumerations that can be shared across various features.

Extensions: Extension methods.

Features: Folders with features that are independent of each other. Each feature consists of one or more controllers and optionally entities, models, services and repositories.

GeneralServices: Services that can be used by one or more features.

GenericControllers: Controllers that can be inherited from by other controllers and contain CRUD operations for the type parameter.

GenericRepositories: Same as GenericControllers, but with repositories.

Helpers: Static helper methods.

Migrations: Entity Framework migrations.

RepositoryInterfaces: Interfaces with CRUD operations that can be implemented by repositories. Each interface has one method.
