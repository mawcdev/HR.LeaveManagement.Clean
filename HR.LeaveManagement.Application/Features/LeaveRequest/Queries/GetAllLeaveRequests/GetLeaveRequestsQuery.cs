using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

public record GetLeaveRequestsQuery : IRequest<List<LeaveRequestDto>>;
