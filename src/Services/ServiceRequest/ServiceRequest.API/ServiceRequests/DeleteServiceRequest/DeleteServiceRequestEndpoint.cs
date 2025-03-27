namespace ServiceRequest.API.ServiceRequests.DeleteServiceRequest;

public record DeleteServiceRequestResponse(bool IsSuccess);

public class DeleteServiceRequestEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/servicerequest/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteServiceRequestCommand(id));

            var response = result.Adapt<DeleteServiceRequestResponse>();

            return Results.Ok(response);
        })
        .WithName("DeleteServiceRequest")
        .Produces<DeleteServiceRequestResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Delete service request based on id")
        .WithDescription("delete service request based on id");
    }
}