namespace ServiceRequest.API.ServiceRequests.DeleteServiceRequest;

public record DeleteServiceRequestCommand(Guid Id) : ICommand<DeleteServiceRequestResult>;
public record DeleteServiceRequestResult(bool IsSuccess);

public class DeleteServiceRequestCommandValidator : AbstractValidator<DeleteServiceRequestCommand>
{
    public DeleteServiceRequestCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Service Request ID is required");
    }
}

internal class DeleteServiceRequestCommandHandler
    (IDocumentSession session)
    : ICommandHandler<DeleteServiceRequestCommand, DeleteServiceRequestResult>
{
    public async Task<DeleteServiceRequestResult> Handle(DeleteServiceRequestCommand command, CancellationToken cancellationToken)
    {
        session.Delete<Service>(command.Id);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteServiceRequestResult(true);
    }
}