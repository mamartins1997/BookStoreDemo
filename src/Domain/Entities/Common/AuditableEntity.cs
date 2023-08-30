namespace BookStoreApi.Domain.Entities.Common;

public abstract  class AuditableEntity : BaseEntity
{
    public DateTimeOffset Created { get; set; }
    
    public DateTimeOffset LastModified { get; set; }

    public void SetLastModified()
        => this.LastModified = DateTime.Now;
}