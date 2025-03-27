

namespace ServiceRequest.API.ServiceRequests;

public record GetServiceRequestsQuery() : IQuery<GetServiceRequestsResult>;
public record GetServiceRequestsResult(IEnumerable<Service> ServiceRequests);

internal class GetServiceRequestsQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetServiceRequestsQuery, GetServiceRequestsResult>
{
    public async Task<GetServiceRequestsResult> Handle(GetServiceRequestsQuery query, CancellationToken cancellationToken)
    {
        var serviceRequests = await session.Query<Service>().ToListAsync(cancellationToken);

        return new GetServiceRequestsResult(serviceRequests);
    }
}