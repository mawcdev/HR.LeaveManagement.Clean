using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using HR.LeaveManagement.Domain;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Commands
{
    public class DeleteLeaveTypeCommandHandlerTest
    {
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private IMapper _mapper;
        private Mock<IAppLogger<DeleteLeaveTypeCommandHandler>> _logger;

        public DeleteLeaveTypeCommandHandlerTest()
        {
            _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _logger = new Mock<IAppLogger<DeleteLeaveTypeCommandHandler>>();
        }

        [Fact]
        public async Task DeleteLeaveType()
        {
            var handler = new DeleteLeaveTypeCommandHandler(_mockRepo.Object);
            int id = 1;
            var result = await handler.Handle(new DeleteLeaveTypeCommand { Id = id }, CancellationToken.None);

            var getHandler = new GetLeaveTypeDetailsQueryHandler(_mapper, _mockRepo.Object);
            Task value()
            {
                return getHandler.Handle(new GetLeaveTypeDetailsQuery(1), CancellationToken.None);
            }
            var ex = await Record.ExceptionAsync(value);

            ex.ShouldNotBeNull();
            ex.ShouldBeOfType(typeof(NotFoundException));
            ex.Message.Equals($"{nameof(LeaveType)} ({id}) was not found.");
        }
    }
}
