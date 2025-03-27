namespace BitBucket.Data;

public class ServiceRequestInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var session = store.LightweightSession();

        if (await session.Query<ServiceRequest>().AnyAsync())
            return;

        session.Store<ServiceRequest>(GetPreconfiguredServiceRequests());
        await session.SaveChangesAsync();
    }

    public static IEnumerable<ServiceRequest> GetPreconfiguredServiceRequests() => new List<ServiceRequest>()
        {
            new ServiceRequest
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                BuildingCode = "B123",
                Description = "Air conditioning repair",
                CurrentStatus = CurrentStatus.Created,
                CreatedBy = "James",
                CreatedDate = DateTime.UtcNow,
                LastModifiedBy = "James",
                LastModifiedDate = DateTime.UtcNow
            },
            new ServiceRequest
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                BuildingCode = "B456",
                Description = "Plumbing leak fix",
                CurrentStatus = CurrentStatus.InProgress,
                CreatedBy = "Angelo",
                CreatedDate = DateTime.UtcNow.AddDays(-1),
                LastModifiedBy = "Angelo",
                LastModifiedDate = DateTime.UtcNow
            },
            new ServiceRequest
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                BuildingCode = "B789",
                Description = "Electrical wiring issue",
                CurrentStatus = CurrentStatus.Complete,
                CreatedBy = "Sarona",
                CreatedDate = DateTime.UtcNow.AddDays(-3),
                LastModifiedBy = "Sarona",
                LastModifiedDate = DateTime.UtcNow.AddDays(-1)
            }
    };
}