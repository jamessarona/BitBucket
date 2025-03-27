namespace ServiceRequest.API.Models;

public class Service : Entity<Guid>
{
    public Guid Id { get; set; }
    public string BuildingCode { get; set; } = default!;
    public string Description { get; set; } = default!;
    public CurrentStatus CurrentStatus { get; set; } = CurrentStatus.Created;
}