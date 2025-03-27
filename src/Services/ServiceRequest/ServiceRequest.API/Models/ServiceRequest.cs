using BitBucket.Abstractions;
using BitBucket.Enums;

namespace BitBucket.Models;

public class ServiceRequest : Entity<Guid>
{
    public Guid Id { get; set; }
    public string BuildingCode { get; set; } = default!;
    public string Description { get; set; } = default!;
    public CurrentStatus CurrentStatus { get; set; } = CurrentStatus.Created;
}