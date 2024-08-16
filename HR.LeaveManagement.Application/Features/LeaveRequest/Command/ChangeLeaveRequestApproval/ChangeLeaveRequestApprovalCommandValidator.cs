using FluentValidation;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Command.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandValidator: AbstractValidator<ChangeLeaveRequestApprovalCommand>
{
    public ChangeLeaveRequestApprovalCommandValidator()
    {
        RuleFor(p => p.Approved)
            .NotNull()
            .WithMessage("Approval Status cannot be null.");
    }
}