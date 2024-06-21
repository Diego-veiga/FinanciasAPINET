
using AutoMapper;
using financias.src.DTOs;
using financias.src.models;

namespace financias.src.mapping
{
    public class BanckAccountMapping : Profile
    {
        public BanckAccountMapping()
        {
        

            CreateMap<BanckAccount, BanckAccountView>()
                                         .ForMember(b => b.Id, d => d.MapFrom(b => b.Id))
                                         .ForMember(b => b.Name, d => d.MapFrom(b => b.Name))
                                         .ForMember(b => b.Balance, d=> d.MapFrom(d => d.Balance))
                                         .ForMember(b => b.BanckId, d=> d.MapFrom(d => d.BanckId))
                                         .ForMember(b => b.CreatedAt, d => d.MapFrom(b => b.CreatedAt))
                                         .ForMember(b => b.Id, d => d.MapFrom(b => b.Id));
        }
    }
}