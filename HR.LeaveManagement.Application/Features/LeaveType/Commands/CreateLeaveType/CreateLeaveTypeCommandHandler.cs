using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<CreateLeaveTypeCommandHandler> _logger;
        public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<CreateLeaveTypeCommandHandler> logger)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _logger = logger;
        }
        public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // 1. Validate incoming data
            var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Any())
            {
                _logger.LogWarning($"Validation errors in create request for {nameof(LeaveType)}.");
                throw new BadRequestException("Invalid Leave Type", validationResult);
            }

            // 2. convert to domain entity object
            var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);

            // 3. Add to database
            await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);

            // 4. return record id
            return leaveTypeToCreate.Id;
        }
    }
}
