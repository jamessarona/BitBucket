using BuildingBlocks.Exceptions;

namespace ServiceRequest.API.ServiceRequests.GetServiceRequestById;

public record GetServiceRequestByIdQuery(Guid Id) : IQuery<GetServiceRequestByIdResult>;
public record GetServiceRequestByIdResult(Service ServiceRequest);

internal class GetServiceRequestByIdQueryHandler
    (IDocumentSession session)
    : IQueryHandler<GetServiceRequestByIdQuery, GetServiceRequestByIdResult>
{
    public async Task<GetServiceRequestByIdResult> Handle(GetServiceRequestByIdQuery query, CancellationToken cancellationToken)
    {
        var serviceRequest = await session.LoadAsync<Service>(query.Id, cancellationToken);
        
        if (serviceRequest is null)
            throw new NotFoundException("Service Request Id not found");

        return new GetServiceRequestByIdResult(serviceRequest);
    }
}
