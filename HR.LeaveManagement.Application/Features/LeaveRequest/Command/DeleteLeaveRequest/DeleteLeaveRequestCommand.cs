using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Command.DeleteLeaveRequest;

public class DeleteLeaveRequestCommand : IRequest<Unit>
{
    public int Id { get; set; }
}
