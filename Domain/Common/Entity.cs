namespace Domain.Common
{
    /// <summary>
    /// An abstract class that every entity must inherit from.
    /// </summary>
    public abstract class Entity
    {
        public Guid Id { get; set; }
    }
}
