namespace ServiceRequest.API.Models;

public class Service
{
    public Guid Id { get; set; }
    public string BuildingCode { get; set; } = default!;
    public string Description { get; set; } = default!;
    public CurrentStatus CurrentStatus { get; set; } = CurrentStatus.Created;
    public string? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}