using AutoMapper;
using Moodle.Server.Models.Entities;
using Moodle.Server.Models.Dtos;

namespace Moodle.Server.Profiles
{
    public class DegreeMapperProfile : Profile
    {
        public DegreeMapperProfile()
        {
            CreateMap<Degree, DegreeDto>();
        }
    }
}
