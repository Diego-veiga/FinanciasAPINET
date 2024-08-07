using AutoMapper;
using financiasapi.src.dtos;
using financiasapi.src.models;

namespace financias.src.mapping
{
    public class BankAccountMapping : Profile
    {
        public BankAccountMapping()
        {
        

            CreateMap<BankAccount, BankAccountView>()
                                         .ForMember(b => b.Id, d => d.MapFrom(b => b.Id))
                                         .ForMember(b => b.Name, d => d.MapFrom(b => b.Name))
                                         .ForMember(b => b.Balance, d=> d.MapFrom(d => d.Balance))
                                         .ForMember(b => b.BanckId, d=> d.MapFrom(d => d.BankId))
                                         .ForMember(b => b.CreatedAt, d => d.MapFrom(b => b.CreatedAt))
                                         .ForMember(b => b.Id, d => d.MapFrom(b => b.Id));
        }
    }
}