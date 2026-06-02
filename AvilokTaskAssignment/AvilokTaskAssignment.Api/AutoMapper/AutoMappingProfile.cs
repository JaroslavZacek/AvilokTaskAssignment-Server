using AutoMapper;
using AvilokTaskAssignment.Data.Models;
using AvilokTaskAssignment.Api.DTO;
using Microsoft.Identity.Client;

using TaskStatus = AvilokTaskAssignment.Data.Models.TaskStatus;

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
                    opt => opt.MapFrom(src => src.AssignedUser != null
                        ? src.AssignedUser.UserName
                        : null));

            CreateMap<TaskItem, TaskDetailDto>()
                .ForMember(dest => dest.CreatedByName,
                    opt => opt.MapFrom(src => src.CreatedBy.UserName))
                .ForMember(dest => dest.AssignedUserName,
                    opt => opt.MapFrom(src => src.AssignedUser != null
                        ? src.AssignedUser.UserName
                        : null))
                .ForMember(dest => dest.IsOverdue,
                    opt => opt.MapFrom(src => src.Status != TaskStatus.Finished && src.Deadline < DateTime.UtcNow));
        }
    }
}
