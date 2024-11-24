using AutoMapper;
using WebStarter6DBApp.DTO;
using WebStarter6DBApp.Models;

namespace WebStarter6DBApp.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<StudentInsertDTO, Student>().ReverseMap();    // <source, destination> with ReverseMap we have two-way mapping
            CreateMap<StudentUpdateDTO, Student>().ReverseMap();
            CreateMap<StudentReadOnlyDTO, Student>().ReverseMap();
        }
    }
}
