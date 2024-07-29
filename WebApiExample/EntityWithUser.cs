namespace WebApiExample
{
    public abstract class EntityWithUser : Entity
    {
        public required string UserId { get; set; }
    }
}
