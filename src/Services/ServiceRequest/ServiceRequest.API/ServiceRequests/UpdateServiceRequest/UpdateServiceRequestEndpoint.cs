namespace ServiceRequest.API.ServiceRequests.UpdateServiceRequest;

public record UpdateServiceRequestRequest(Guid Id, string BuildingCode, string Description, string CurrentStatus, string LastModifiedBy);
public record UpdateServiceRequestResponse(bool isSuccess);

public class UpdateServiceRequestEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/servicerequest",
            async (UpdateServiceRequestRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateServiceRequestCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateServiceRequestResponse>();

                return Results.Ok(response);
            })
            .WithName("UpdateServiceRequest")
            .Produces<UpdateServiceRequestResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Update service request based on id")
            .WithDescription("Update service request based on id");
    }
}