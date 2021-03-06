using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TVMazeScraper.Models;
using TVMazeScraper.Models.Dtos;

namespace TVMazeScraper.Mappers
{
    public class TVShowMapper
    {
        public TVShow fromDto(TVShowDto tVShowDto)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CastMemberDto, Actor>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.person.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.person.Name))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.person.Birthday));

                cfg.CreateMap<TVShowDto, TVShow>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Cast, opt => opt.MapFrom(src => src._embedded.Cast));
            });

            var mapper = new Mapper(config);
            return mapper.Map<TVShow>(tVShowDto);
        }
    }
}
