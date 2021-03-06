using AutoMapper;
using BasicBilling.Data.Entities;
using BasicBilling.Data.Models;

namespace BasicBilling.Profiles
{
  public class BillingProfile : Profile
  {
    public BillingProfile()
    {
      CreateMap<Bill, BillReadDto>();

      CreateMap<Client, ClientReadDto>();
      CreateMap<ClientCreateDto, Client>();
      CreateMap<ClientUpdateDto, Client>();

      CreateMap<Service, ServiceReadDto>();
      CreateMap<ServiceCreateDto, Service>();
      CreateMap<ServiceUpdateDto, Service>();

      CreateMap<Payment, PaymentReadDto>();
    }
  }
}