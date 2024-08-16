using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public class GetLeaveRequestDetailsQueryHandler : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IAppLogger<GetLeaveRequestDetailsQueryHandler> _logger;
    public GetLeaveRequestDetailsQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository, IAppLogger<GetLeaveRequestDetailsQueryHandler> logger)
    {
        _mapper = mapper;
        _leaveRequestRepository = leaveRequestRepository;
        _logger = logger;
    }
    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
    {
        // 1. Query the database
        var leaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetails(request.Id);

        // 2. verify that record exists
        if (leaveRequest == null)
            throw new NotFoundException(nameof(LeaveRequest), request.Id);

        // 3. Convert data objects to DTO objects
        var leaveRequestDetailsDto = _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);

        // 4. return list of DTO objects
        return leaveRequestDetailsDto;
    }
}
