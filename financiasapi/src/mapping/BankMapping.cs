using AutoMapper;
using financiasapi.src.dtos;
using financiasapi.src.models;


namespace financias.src.mapping
{
    public class BankMapping : Profile
    {
        public BankMapping()
        {

            CreateMap<Bank, BankView>()
                                         .ForMember(b => b.Id, d => d.MapFrom(b => b.Id))
                                         .ForMember(b => b.Name, d => d.MapFrom(b => b.Name))
                                         .ForMember(b => b.CreatedAt, d => d.MapFrom(b => b.CreatedAt))
                                         .ForMember(b => b.Id, d => d.MapFrom(b => b.Id));
        }

    }
}