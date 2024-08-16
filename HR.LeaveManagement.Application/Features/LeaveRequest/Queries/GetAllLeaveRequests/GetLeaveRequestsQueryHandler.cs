using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

public class GetLeaveRequestsQueryHandler : IRequestHandler<GetLeaveRequestsQuery, List<LeaveRequestDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IAppLogger<GetLeaveRequestsQueryHandler> _logger;
    public GetLeaveRequestsQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository, IAppLogger<GetLeaveRequestsQueryHandler> logger)
    {
        _mapper = mapper;
        _leaveRequestRepository = leaveRequestRepository;
        _logger = logger;
    }
    public async Task<List<LeaveRequestDto>> Handle(GetLeaveRequestsQuery request, CancellationToken cancellationToken)
    {
        // 1. Query the database
        var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();

        // 2. Convert data objects to DTO objects
        var leaveRequestDto = _mapper.Map<List<LeaveRequestDto>>(leaveRequests);

        // 3. return list of DTO objects
        _logger.LogInformation("Leave requests were retrieved successfully.");
        return leaveRequestDto;
    }
}
