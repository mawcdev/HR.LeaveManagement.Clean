using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public record GetLeaveAllocationDetailsQuery : IRequest<LeaveAllocationDetailsDto>
{
    public int Id { get; set; }
}
