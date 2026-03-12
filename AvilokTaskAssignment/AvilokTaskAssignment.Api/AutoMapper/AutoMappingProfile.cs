using AutoMapper;
using AvilokTaskAssignment.Data.Models;
using AvilokTaskAssignment.Api.DTO;
using Microsoft.Identity.Client;

namespace AvilokTaskAssignment.Api.AutoMapper
{
    public class AutoMappingProfile: Profile
    {
        public AutoMappingProfile() 
        {
            CreateMap<CreateTaskDto, CreateTaskDto>();

            CreateMap<TaskItem, TaskListDto>()
                .ForMember(dest => dest.CreatedByName,
                    opt => opt.MapFrom(src => src.CreatedBy.UserName))
                .ForMember(dest => dest.AssignedUserName,
                    opt => opt.MapFrom(src => 
                        src.Status != Data.Models.TaskStatus.Closed && src.Deadline < DateTime.UtcNow));
        }
    }
}
