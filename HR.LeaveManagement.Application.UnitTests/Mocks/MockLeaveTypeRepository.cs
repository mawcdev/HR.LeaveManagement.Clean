using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.UnitTests.Mocks
{
    public class MockLeaveTypeRepository
    {
        public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()
        {
            var leaveTypes = new List<LeaveType>
            {
                new LeaveType
                {
                    Id= 1,
                    DefaultDays =10,
                    Name = "Test Vacation"
                },
                new LeaveType
                {
                    Id=2,
                    DefaultDays =15,
                    Name="Test Sick"
                },
                new LeaveType 
                {
                    Id=3,
                    DefaultDays= 15,
                    Name="Test Maternity"
                }
            };

            var mockRepo = new Mock<ILeaveTypeRepository>();
            mockRepo.Setup(r => r.GetAsync()).ReturnsAsync(leaveTypes);
            mockRepo.Setup(r => r.CreateAsync(It.IsAny<LeaveType>()))
                .Returns((LeaveType leaveType) =>
                {
                    leaveTypes.Add(leaveType);
                    return Task.CompletedTask;
                });
            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).Returns((int id) => 
                {
                    LeaveType leaveType = leaveTypes.FirstOrDefault(x => x.Id == id);
                    return Task.FromResult(leaveType); 
                });
            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<LeaveType>())).Returns((LeaveType leaveType) =>
            {
                leaveTypes.First(x => x.Id == leaveType.Id).Name = leaveType.Name;
                leaveTypes.First(x => x.Id == leaveType.Id).DefaultDays = leaveType.DefaultDays;
                
                return Task.CompletedTask;
            });
            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<LeaveType>())).Returns((LeaveType leaveType) =>
            {
                leaveTypes.Remove(leaveType);
                return Task.CompletedTask;
            });
            mockRepo.Setup(r => r.IsLeaveTypeUnique(It.IsAny<string>())).Returns((string name) =>
            {
                var isUnique = leaveTypes.Any(x => x.Name.ToLower() == name.ToLower());
                return Task.FromResult(!isUnique);
            });

            return mockRepo;
        }
    }
}
