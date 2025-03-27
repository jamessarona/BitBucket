namespace ServiceRequest.API.Abstractions;

public class Entity<T> : IEntity<T>
{
    public T Id { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}