
namespace ServiceRequest.API.ServiceRequests.CreateServiceRequest;

public record CreateServiceRequestCommand(string BuildingCode, string Description, string CreatedBy, string LastModifiedBy)
    : ICommand<CreateServiceRequestResult>;
public record CreateServiceRequestResult(Guid id);

public class CreateServiceRequestCommandValidator : AbstractValidator<CreateServiceRequestCommand>
{
    public CreateServiceRequestCommandValidator()
    {
        RuleFor(x => x.BuildingCode).NotEmpty().WithMessage("Building Code is required");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("CreatedBy is required");
        RuleFor(x => x.LastModifiedBy).NotEmpty().WithMessage("LastModifiedBy is required");
    }
}

internal class CreateServiceRequestCommandHandler
    (IDocumentSession session)
    : ICommandHandler<CreateServiceRequestCommand, CreateServiceRequestResult>
{
    public async Task<CreateServiceRequestResult> Handle(CreateServiceRequestCommand command, CancellationToken cancellationToken)
    {
        var serviceRequest = new Service
        {
            Id = Guid.NewGuid(),
            BuildingCode = command.BuildingCode,
            Description = command.Description,
            CurrentStatus = CurrentStatus.Created,
            CreatedBy = command.CreatedBy,
            CreatedDate = DateTime.UtcNow,
            LastModifiedBy = command.LastModifiedBy,
            LastModifiedDate = DateTime.UtcNow,
        };

        session.Store(serviceRequest);
        await session.SaveChangesAsync(cancellationToken);

        return new CreateServiceRequestResult(serviceRequest.Id);
    }
}
