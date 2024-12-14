using Domain.Common;

namespace Infrastructure.UnitTests.DatabaseOperationsTests
{
    public class TestSoftDeletableEntity : EntityWithUserBase, ISoftDeletable
    {
        public bool IsDeleted { get; set; }
        public DateTimeOffset? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
    }
}
