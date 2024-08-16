using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public record GetLeaveRequestDetailsQuery : IRequest<LeaveRequestDetailsDto>
{
    public int Id { get; set; }
}
