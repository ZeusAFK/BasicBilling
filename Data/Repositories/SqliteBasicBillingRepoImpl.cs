using System;
using System.Collections.Generic;
using System.Linq;
using BasicBilling.Data.Contexts;
using BasicBilling.Data.Entities;
using BasicBilling.Data.Interfaces;

namespace BasicBilling.Data.Repositories
{
  public class SqliteBasicBillingRepoImpl : IBasicBillingRepo
  {
    private readonly BasicBillingContext context = default!;

    public SqliteBasicBillingRepoImpl(BasicBillingContext context)
    {
      this.context = context;
    }

    public void CreateBill(Bill bill)
    {
      if (bill == null) throw new ArgumentNullException(nameof(bill));
      context.Bills.Add(bill);
    }

    public void CreateClient(Client client)
    {
      if (client == null) throw new ArgumentNullException(nameof(client));
      context.Clients.Add(client);
    }

    public void CreateService(Service service)
    {
      if (service == null) throw new ArgumentNullException(nameof(service));
      context.Services.Add(service);
    }

    public IEnumerable<Client> GetAllClients()
    {
      return context.Clients.ToList();
    }

    public IEnumerable<Service> GetAllServices()
    {
      return context.Services.ToList();
    }

    public Bill GetBillById(int id)
    {
      return context.Bills.FirstOrDefault(e => e.Id == id);
    }

    public Client GetClientById(int id)
    {
      return context.Clients.FirstOrDefault(e => e.Id == id);
    }

    public Service GetServiceById(int id)
    {
      return context.Services.FirstOrDefault(e => e.Id == id);
    }

    public Service GetServiceByShortname(string shortname)
    {
      return context.Services.FirstOrDefault(e => e.Shortname == shortname);
    }

    public bool SaveChanges()
    {
      return context.SaveChanges() >= 0;
    }

    public void UpdateClient(Client client)
    {
      
    }

    public void UpdateService(Service service)
    {
      
    }
  }
}