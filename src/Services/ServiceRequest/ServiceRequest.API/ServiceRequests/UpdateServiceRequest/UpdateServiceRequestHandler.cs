using BuildingBlocks.Exceptions;
using OpenTelemetry.Trace;

namespace ServiceRequest.API.ServiceRequests.UpdateServiceRequest;

public record UpdateServiceRequestCommand(Guid Id, string BuildingCode, string Description, string CurrentStatus, string LastModifiedBy)
    : ICommand<UpdateServiceRequestResult>;
public record UpdateServiceRequestResult(bool IsSuccess);

public class UpdateServiceRequestCommandValidator : AbstractValidator<UpdateServiceRequestCommand>
{
    public UpdateServiceRequestCommandValidator()
    {
        RuleFor(command => command.Id).NotEmpty().WithMessage("Service Request ID is required");
        RuleFor(x => x.BuildingCode).NotEmpty().WithMessage("Building Code is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.CurrentStatus).NotEmpty().WithMessage("CurrentStatus is required");
        RuleFor(x => x.LastModifiedBy).NotEmpty().WithMessage("LastModifiedBy is required");
    }
}

internal class UpdateServiceRequestCommandHandler
    (IDocumentSession session)
    : ICommandHandler<UpdateServiceRequestCommand, UpdateServiceRequestResult>
{
    public async Task<UpdateServiceRequestResult> Handle(UpdateServiceRequestCommand command, CancellationToken cancellationToken)
    {
        var serviceRequest = await session.LoadAsync<Service>(command.Id, cancellationToken);

        if (serviceRequest is null)
            throw new NotFoundException($"Id not found {command.Id}");

        serviceRequest.Description = command.BuildingCode;
        serviceRequest.Description = command.Description;
        serviceRequest.CurrentStatus = (CurrentStatus)Enum.Parse(typeof(CurrentStatus), command.CurrentStatus);
        serviceRequest.LastModifiedBy = command.LastModifiedBy;
        serviceRequest.LastModifiedDate = DateTime.UtcNow;

        session.Update(serviceRequest);
        await session.SaveChangesAsync(cancellationToken);

        return new UpdateServiceRequestResult(true);
    }
}