namespace BookStoreApi.Domain.Entities.Common;

public abstract  class AuditableEntity : BaseEntity
{
    public DateTime Created { get; set; }
    
    public DateTime? LastModified { get; set; }

    public void SetLastModified()
        => this.LastModified = DateTime.Now;
}