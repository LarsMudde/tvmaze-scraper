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
        private readonly MapperConfiguration config = new(cfg =>
        {
            cfg.CreateMap<CastMemberDto, Actor>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Person.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Person.Name))
            .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Person.Birthday));
        });

        private readonly Mapper mapper;

        public ActorMapper()
        {
            mapper = new Mapper(config);
        }

        public Actor FromDto(CastMemberDto castMemberDto)
        {
            return mapper.Map<Actor>(castMemberDto);
        }

        //TODO: Update so this works with the mapper itsself instead of a ForEach
        public IEnumerable<Actor> FromDto(IEnumerable<CastMemberDto> castMemberDtos)
        {
            var actors = new List<Actor>();
            castMemberDtos.ToList().ForEach(a => actors.Add(FromDto(a)));
            return actors;
        }
    }
}
