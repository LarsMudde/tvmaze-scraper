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

        public Actor fromDto(CastMemberDto castMemberDto)
        {
            var mapper = new Mapper(config);
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
