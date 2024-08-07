using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger;
    public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<UpdateLeaveTypeCommandHandler> logger)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }
    public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        // 1. Validate incoming data
        var validator = new UpdateLeaveTypeCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
        {
            _logger.LogWarning($"Validation errors in update request for {nameof(LeaveType)} - {request.Id}.");
            throw new BadRequestException("Invalid Leave Type.", validationResult);
        }

        // 2. convert to domain entity object
        var leaveTypeToUpdate = _mapper.Map<Domain.LeaveType>(request);

        // 3. Add to database
        await _leaveTypeRepository.UpdateAsync(leaveTypeToUpdate);

        // 4. return record id
        return Unit.Value;
    }
}
