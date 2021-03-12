using AutoMapper;
using MSR.DAL.Entities;
using MSRDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSRDAL.AutoMapper
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Month, MonthModel>();
            CreateMap<Years, YearModel>();
            CreateMap<MSR.DAL.Entities.Task, TaskModel>();
            CreateMap<SubTask, SubTaskModel>();
            CreateMap<JiraTicket, JiraTicketModel>();
        }
    }
    public class AutoMapperConfiguration
    {
        public MapperConfiguration Configure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            return config;
        }
    }
}
