using AutoMapper;
using PlusNarrativeAssessment.Data;
using PlusNarrativeAssessment.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlusNarrativeAssessment.Business
{
    public class MappingProfile : Profile
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<GameModel, Game>().ReverseMap();
                cfg.CreateMap<GameModel, PlayerModel>().ReverseMap();
                cfg.CreateMap<PlayerAnswerModel, PlayerAnswer>().ReverseMap();
                cfg.CreateMap<MovieModel, PlayerAnswer>().ReverseMap();
                cfg.CreateMap<PlayerModel, ResultModel>().ReverseMap();
            });

            var mapper = config.CreateMapper();
            return mapper;
        });

        public static IMapper Mapper => Lazy.Value;
    }
}
