using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Command.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, Unit>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository)
    {
        _leaveRequestRepository = leaveRequestRepository;
    }

    public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        // 1. retrieve domain entity object
        var leaveRequestToDelete = await _leaveRequestRepository.GetByIdAsync(request.Id);

        // 2. verify that record exists
        if (leaveRequestToDelete == null)
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);

        // 3. Add to database
        await _leaveRequestRepository.DeleteAsync(leaveRequestToDelete);

        // 4. return record id
        return Unit.Value;
    }
}
