using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Command.DeleteLeaveAllocation
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            // 1. retrieve domain entity object
            var leaveAllocationToDelete = await _leaveAllocationRepository.GetByIdAsync(request.Id);

            // 2. verify that record exists
            if (leaveAllocationToDelete == null)
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);

            // 3. delete in database
            await _leaveAllocationRepository.DeleteAsync(leaveAllocationToDelete);

            // 4. return record id
            return Unit.Value;
        }
    }
}
