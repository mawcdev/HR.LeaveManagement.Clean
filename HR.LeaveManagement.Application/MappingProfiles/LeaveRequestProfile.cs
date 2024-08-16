using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveRequest.Command.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Command.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveRequestProfile : Profile
    {
        public LeaveRequestProfile()
        {
            CreateMap<UpdateLeaveRequestCommand, LeaveRequest>();
            CreateMap<CreateLeaveRequestCommand, LeaveRequest>();
            CreateMap<LeaveRequestDto, LeaveRequest>().ReverseMap();
            CreateMap<LeaveRequest, LeaveRequestDetailsDto>();
        }
    }
}
