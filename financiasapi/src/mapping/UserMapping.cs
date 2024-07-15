using AutoMapper;
using financiasapi.src.dtos;
using financiasapi.src.models;

namespace financias.src.mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserView>();



        }

    }
}