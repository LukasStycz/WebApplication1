using AutoMapper;
using WebApplication1.Application.Models;
using WebApplication1.Domain.Models;

//Class responsible for mapping objects of classes that hold and validate incoming data to objects of the class representing a table in the database.
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