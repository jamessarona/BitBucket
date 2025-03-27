
namespace ServiceRequest.API.ServiceRequests.CreateServiceRequest;

public record CreateServiceRequest(string BuildingCode, string Description, string CreatedBy, string LastModifiedBy);
public record CreateServiceRequestResponse(Guid Id);

public class CreateServiceRequestEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/servicerequest",
            async (CreateServiceRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateServiceRequestCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateServiceRequestResponse>();

                return Results.Ok($"Created service request with {response.Id}");
            })
        .WithName("CreateServiceRequest")
        .Produces<CreateServiceRequestResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Created service request")
        .WithDescription("Created service request");
    }
}