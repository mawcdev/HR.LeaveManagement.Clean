using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logging;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
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
    public class CreateLeaveTypeCommandHandlerTest
    {
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
        private IMapper _mapper;
        private Mock<IAppLogger<CreateLeaveTypeCommandHandler>> _logger;

        public CreateLeaveTypeCommandHandlerTest()
        {
            _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _logger = new Mock<IAppLogger<CreateLeaveTypeCommandHandler>>();
        }

        [Fact]
        public async Task CreateLeaveType()
        {
            var handler = new CreateLeaveTypeCommandHandler(_mapper, _mockRepo.Object, _logger.Object);

            var result = await handler.Handle(new CreateLeaveTypeCommand
            {
                Name="Test Emergency",
                DefaultDays = 12
            }, CancellationToken.None);

            var getHandler = new GetLeaveTypeDetailsQueryHandler(_mapper, _mockRepo.Object);
            var leaveType = await getHandler.Handle(new GetLeaveTypeDetailsQuery(result), CancellationToken.None);

            result.ShouldBeOfType<int>();
            leaveType.Name.ShouldBe("Test Emergency");
            leaveType.DefaultDays.ShouldBe(12);
        }
    }
}
