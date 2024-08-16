using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Command.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public bool Approved { get; set; }
}
