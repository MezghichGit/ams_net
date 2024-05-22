using ams.Dtos;
using ams.Models;
using AutoMapper;

namespace ams
{
    public class AppMapperProfile : Profile
    {
        public AppMapperProfile()
        {
            CreateMap<ProviderDto, Provider>().ReverseMap();
            CreateMap<ArticleDto, Article>().ReverseMap();
        }
    }
}
