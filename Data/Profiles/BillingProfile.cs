using AutoMapper;
using BasicBilling.Data.Entities;
using BasicBilling.Data.Models;

namespace Commander.Profiles
{
  public class BillingProfile : Profile
  {
    public BillingProfile()
    {
      CreateMap<Bill, BillReadDto>();
      CreateMap<Client, ClientReadDto>();
      CreateMap<ClientCreateDto, Client>();
      CreateMap<ClientUpdateDto, Client>();
    }
  }
}