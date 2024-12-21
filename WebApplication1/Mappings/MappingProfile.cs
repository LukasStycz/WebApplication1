using AutoMapper;
using WebApplication1.Application.Models;
using WebApplication1.Domain.Models;

namespace WebApplication1.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateToDoDto, ToDo>().ReverseMap();
            CreateMap<UpdateTodoDto, ToDo>().ReverseMap();
        }       
    }
}