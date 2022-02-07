using AutoMapper;
using Fakultet.Dtos;
using Fakultet.Models;

namespace Fakultet.Profiles
{
    public class PredmetsProfile : Profile
    {
        public PredmetsProfile()
        {
            CreateMap<Predmet, PredmetReadDto>();

            CreateMap<PredmetCreateDto, Predmet>();

            CreateMap<PredmetUpdateDto, Predmet>();

            CreateMap<Predmet, PredmetUpdateDto>();
        }
    }
}