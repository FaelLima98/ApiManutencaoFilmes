using ApiManutencaoFilmes.Models;
using AutoMapper;

namespace ApiManutencaoFilmes.DTOs.Mappings {
    public class MappingProfile : Profile {

        public MappingProfile() {

            CreateMap<Filme, FilmeDTO>().ReverseMap();
        }
    }
}
