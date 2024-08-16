using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Command.CancelLeaveRequest;

public class CancelLeaveRequestCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
