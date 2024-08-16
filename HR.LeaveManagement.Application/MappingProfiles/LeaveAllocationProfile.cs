using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Command.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Command.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveAllocationProfile : Profile
    {
        public LeaveAllocationProfile()
        {
            CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>();
            CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();
            CreateMap<LeaveAllocationDto, LeaveAllocation>().ReverseMap();
            CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>();
        }
    }
}
