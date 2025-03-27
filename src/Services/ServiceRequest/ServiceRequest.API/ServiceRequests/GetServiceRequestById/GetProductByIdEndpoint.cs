
namespace ServiceRequest.API.ServiceRequests.GetServiceRequestById;

public record GetServiceRequestByIdResponse(Service ServiceRequest);

public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/servicerequest/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetServiceRequestByIdQuery(id));

            var response = result.Adapt<GetServiceRequestByIdResponse>();

            return Results.Ok(response);
        })
        .WithName("GetServiceRequestById")
        .Produces<GetServiceRequestByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Read service request by id")
        .WithDescription("Read service request by id");
    }
}
