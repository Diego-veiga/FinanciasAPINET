using AutoMapper;
using financias.src.DTOs;
using financias.src.models;

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