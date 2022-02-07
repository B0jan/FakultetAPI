using AutoMapper;
using Fakultet.Dtos;
using Fakultet.Models;

namespace Fakultet.Profiles
{
    public class StudentsProfile : Profile
    {
        public StudentsProfile()
        {
            CreateMap<Student, StudentReadDto>();

            CreateMap<StudentCreateDto, Student>();

            CreateMap<StudentUpdateDto, Student>();

            CreateMap<Student, StudentUpdateDto>();
        }
    }
}