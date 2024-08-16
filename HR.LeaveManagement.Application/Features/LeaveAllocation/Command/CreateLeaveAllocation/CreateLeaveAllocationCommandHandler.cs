using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Command.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveAllocationCommandHandler(IMapper mapper,
            ILeaveAllocationRepository leaveAllocationRepository,
            ILeaveTypeRepository leaveTypeRepository)
        {
            _mapper = mapper;
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            // 1. Validate incoming data
            var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository, _leaveAllocationRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {
                //_logger.LogWarning($"Validation errors in create request for {nameof(LeaveType)}.");
                throw new BadRequestException("Invalid Leave Type", validationResult);
            }

            // Get Leave Type for Allocations
            var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

            // TODO: create leave allocation
            // Get Employees

            // Get Period

            // 2. convert to domain entity object
            var leaveAllocationToCreate = _mapper.Map<Domain.LeaveAllocation>(request);

            // 3. Add to database
            await _leaveAllocationRepository.CreateAsync(leaveAllocationToCreate);

            // 4. return record id
            return Unit.Value;
        }
    }
}
