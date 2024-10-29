namespace Domain.Common
{
    /// <summary>
    /// An abstract class that every entity that is bound to a user must inherit from. Warning: add foreign key from <see cref="UserId"/> to the user table.
    /// </summary>
    public abstract class EntityWithUser : Entity
    {
        public required string UserId { get; set; }
    }
}
