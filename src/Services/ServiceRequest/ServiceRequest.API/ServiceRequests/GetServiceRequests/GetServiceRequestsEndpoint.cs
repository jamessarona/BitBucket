namespace ServiceRequest.API.ServiceRequests.GetServiceRequests;

public record GetServiceRequestsResponse(IEnumerable<Service> ServiceRequest);

public class GetServiceRequestsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/servicerequest", async (ISender sender) =>
        {
            var result = await sender.Send(new GetServiceRequestsQuery());
            var response = result.Adapt<GetServiceRequestsResponse>();

            return Results.Ok(response);
        })
        .WithName("GetServiceRequests")
        .Produces<GetServiceRequestsResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Read all service requests")
        .WithDescription("Read all service requests");
    }
}
