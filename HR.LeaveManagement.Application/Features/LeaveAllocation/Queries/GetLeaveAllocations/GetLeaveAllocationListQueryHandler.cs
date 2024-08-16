using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;

public class GetLeaveAllocationListQueryHandler : IRequestHandler<GetLeaveAllocationListQuery, List<LeaveAllocationDto>>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetLeaveAllocationListQueryHandler> _logger;

    public GetLeaveAllocationListQueryHandler(ILeaveAllocationRepository leaveAllocationRepository, 
        IMapper mapper,
        IAppLogger<GetLeaveAllocationListQueryHandler> logger)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListQuery request, CancellationToken cancellationToken)
    {
        // 1. Query the database
        var leaveAllocations = await _leaveAllocationRepository.GetAsync();

        // 2. Convert data objects to DTO objects
        var leaveAllocationsDto = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

        // 3. return list of DTO objects
        _logger.LogInformation("Leave allocations were retrieved successfully.");
        return leaveAllocationsDto;
    }
}
