using AutoMapper;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TVMazeScraper.Models;
using TVMazeScraper.Models.Dtos;

namespace TVMazeScraper.Mappers
{
    public class ActorMapper
    {
        MapperConfiguration config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CastMemberDto, Actor>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.person.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.person.Name))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.person.Birthday));
        });

        private Mapper mapper;

        public ActorMapper()
        {
            mapper = new Mapper(config);
        }

        public Actor fromDto(CastMemberDto castMemberDto)
        {
            return mapper.Map<Actor>(castMemberDto);
        }

        public IEnumerable<Actor> fromDto(IEnumerable<CastMemberDto> castMemberDtos)
        {
            var actors = new List<Actor>();
            castMemberDtos.ToList().ForEach(a => actors.Add(fromDto(a)));
            return actors;
        }
    }
}
