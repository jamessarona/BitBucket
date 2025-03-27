namespace ServiceRequest.API.ServiceRequests.GetServiceRequests;

public record GetServiceRequestsRequest();
public record GetServiceRequestsResponse(IEnumerable<Service> ServiceRequest);

public class GetServiceRequestsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/servicerequest", async ([AsParameters] GetServiceRequestsRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetServiceRequestsQuery>();
            var result = await sender.Send(query);
            var response = result.Adapt<GetServiceRequestsResponse>();

            if (!response.ServiceRequest.Any())
                return Results.NoContent();

            return Results.Ok(response);
        })
        .WithName("Read all service requests")
        .Produces<GetServiceRequestsResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Read all service requests")
        .WithDescription("Read all service requests");
    }
}
