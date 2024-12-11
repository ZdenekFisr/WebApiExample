namespace Domain.Common
{
    /// <summary>
    /// An abstract class that every entity must inherit from.
    /// </summary>
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
    }
}
