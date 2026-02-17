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
        }
    }
}
