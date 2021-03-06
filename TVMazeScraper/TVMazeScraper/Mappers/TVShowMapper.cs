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
        private MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<TVShowDto, TVShow>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            cfg.CreateMap<ActorTVShow, ActorResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Actor.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Actor.Name))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Actor.Birthday));

            cfg.CreateMap<TVShow, TVShowResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Cast, opt => opt.MapFrom(src => src.Cast));
        });

        private Mapper mapper;

        public TVShowMapper()
        {
            mapper = new Mapper(config);
        }

        public TVShow fromDto(TVShowDto tVShowDto)
        {
            return mapper.Map<TVShow>(tVShowDto);
        }

        public TVShowResponseDto toResponseDto(TVShow tVShow)
        {
            return mapper.Map<TVShowResponseDto>(tVShow);
        }

        public IEnumerable<TVShowResponseDto> toResponseDto(IEnumerable<TVShow> tVShows)
        {
            var responseDtos = new List<TVShowResponseDto>();
            tVShows.ToList().ForEach(s => responseDtos.Add(toResponseDto(s)));
            return responseDtos;
        }
    }
}
