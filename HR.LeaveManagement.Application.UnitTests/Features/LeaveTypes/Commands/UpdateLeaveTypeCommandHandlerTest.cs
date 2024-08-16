using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Application.MappingProfiles;
using HR.LeaveManagement.Application.UnitTests.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Commands
{
    public class UpdateLeaveTypeCommandHandlerTest
    {
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private IMapper _mapper;
        private Mock<IAppLogger<UpdateLeaveTypeCommandHandler>> _logger;

        public UpdateLeaveTypeCommandHandlerTest()
        {
            _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _logger = new Mock<IAppLogger<UpdateLeaveTypeCommandHandler>>();
        }

        [Fact]
        public async Task UpdateLeaveType()
        {
            var handler = new UpdateLeaveTypeCommandHandler(_mapper, _mockRepo.Object, _logger.Object);

            var result = await handler.Handle(new UpdateLeaveTypeCommand
            {
                Id = 1,
                Name = "Test Vacation Update",
                DefaultDays = 11
            }, CancellationToken.None);

            var getHandler = new GetLeaveTypeDetailsQueryHandler(_mapper, _mockRepo.Object);
            var leaveType = await getHandler.Handle(new GetLeaveTypeDetailsQuery(1), CancellationToken.None);

            leaveType.Name.ShouldBe("Test Vacation Update");
            leaveType.DefaultDays.ShouldBe(11);
        }
    }
}
