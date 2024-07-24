using AutoMapper;
using financiasapi.src.dtos;
using financiasapi.src.models;


namespace financias.src.mapping
{
    public class BankMapping : Profile
    {
        public BankMapping()
        {
            CreateMap<CreateBank, Bank>()
                                      .ForMember(b => b.Id, d => d.MapFrom(d => Guid.NewGuid()))
                                      .ForMember(b => b.Name, d => d.MapFrom(d => d.Name))
                                      .ForMember(b => b.Cnpj, d => d.MapFrom(d => d.Cnpj.Replace(".", string.Empty).Replace("-", string.Empty).Replace("/", string.Empty)))
                                      .ForMember(b => b.CreatedAt, d => d.MapFrom(d => DateTime.Now))
                                      .ForMember(b => b.UserId, d => d.MapFrom(d => d.UserId))
                                      .ForMember(b => b.Active, d => d.MapFrom(d => true));

            CreateMap<Bank, BankView>()
                                         .ForMember(b => b.Id, d => d.MapFrom(b => b.Id))
                                         .ForMember(b => b.Name, d => d.MapFrom(b => b.Name))
                                         .ForMember(b => b.CreatedAt, d => d.MapFrom(b => b.CreatedAt))
                                         .ForMember(b => b.Id, d => d.MapFrom(b => b.Id));
        }

    }
}