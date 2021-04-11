using System;
using System.Collections.Generic;
using System.Linq;
using BasicBilling.Data.Entities;
using BasicBilling.Data.Enums;
using BasicBilling.Data.Interfaces;

namespace BasicBilling.Data.Repositories
{
  public class MockBasicBillingRepoImpl : IBasicBillingRepo
  {
    private readonly List<Bill> Bills = default!;
    private readonly List<Client> Clients = default!;
    private readonly List<Service> Services = default!;
    private readonly List<Payment> Payments = default!;

    public MockBasicBillingRepoImpl()
    {
      Clients = new List<Client>(){
        new Client(){ Id = 100, Firstname = "Joseph", Lastname = "Carlton" },
        new Client(){ Id = 200, Firstname = "Maria", Lastname = "Juarez" },
        new Client(){ Id = 300, Firstname = "Albert", Lastname = "Kenny" },
        new Client(){ Id = 400, Firstname = "Jessica", Lastname = "Phillips" },
        new Client(){ Id = 500, Firstname = "Charles", Lastname = "Johnson" }
      };

      Services = new List<Service>(){
        new Service(){ Id = 1, Shortname = "WATER" },
        new Service(){ Id = 2, Shortname = "ELECTRICITY" },
        new Service(){ Id = 3, Shortname = "SEWER" }
      };

      Bills = new List<Bill>(){
        new Bill(){ Id = 1, Period = 202101, Client = Clients.ElementAt(0), Service = Services.ElementAt(0), Amount = 235, Status = BillingStatus.Paid },
        new Bill(){ Id = 2,  Period = 202102, Client = Clients.ElementAt(0), Service = Services.ElementAt(1), Amount = 150, Status = BillingStatus.Pending },
        new Bill(){ Id = 3,  Period = 202103, Client = Clients.ElementAt(0), Service = Services.ElementAt(2), Amount = 300, Status = BillingStatus.Pending },

        new Bill(){ Id = 4,  Period = 202101, Client = Clients.ElementAt(1), Service = Services.ElementAt(0), Amount = 235, Status = BillingStatus.Paid },
        new Bill(){ Id = 5,  Period = 202102, Client = Clients.ElementAt(1), Service = Services.ElementAt(1), Amount = 150, Status = BillingStatus.Pending },
        new Bill(){ Id = 6,  Period = 202103, Client = Clients.ElementAt(1), Service = Services.ElementAt(2), Amount = 300, Status = BillingStatus.Pending },

        new Bill(){ Id = 7,  Period = 202101, Client = Clients.ElementAt(2), Service = Services.ElementAt(0), Amount = 235, Status = BillingStatus.Paid },
        new Bill(){ Id = 8,  Period = 202102, Client = Clients.ElementAt(2), Service = Services.ElementAt(1), Amount = 150, Status = BillingStatus.Pending },
        new Bill(){ Id = 9,  Period = 202103, Client = Clients.ElementAt(2), Service = Services.ElementAt(2), Amount = 300, Status = BillingStatus.Pending },

        new Bill(){ Id = 10,  Period = 202101, Client = Clients.ElementAt(3), Service = Services.ElementAt(0), Amount = 235, Status = BillingStatus.Paid },
        new Bill(){ Id = 11,  Period = 202102, Client = Clients.ElementAt(3), Service = Services.ElementAt(1), Amount = 150, Status = BillingStatus.Pending },
        new Bill(){ Id = 12,  Period = 202103, Client = Clients.ElementAt(3), Service = Services.ElementAt(2), Amount = 300, Status = BillingStatus.Pending },

        new Bill(){ Id = 13,  Period = 202101, Client = Clients.ElementAt(4), Service = Services.ElementAt(0), Amount = 235, Status = BillingStatus.Paid },
        new Bill(){ Id = 14,  Period = 202102, Client = Clients.ElementAt(4), Service = Services.ElementAt(1), Amount = 150, Status = BillingStatus.Pending },
        new Bill(){ Id = 15,  Period = 202103, Client = Clients.ElementAt(4), Service = Services.ElementAt(2), Amount = 300, Status = BillingStatus.Pending }
      };

      Payments = new List<Payment>(){
        new Payment(){ Id = 1, Bill = Bills.ElementAt(0), CreatedOn = DateTime.UtcNow },
        new Payment(){ Id = 2, Bill = Bills.ElementAt(3), CreatedOn = DateTime.UtcNow },
        new Payment(){ Id = 3, Bill = Bills.ElementAt(6), CreatedOn = DateTime.UtcNow },
        new Payment(){ Id = 4, Bill = Bills.ElementAt(9), CreatedOn = DateTime.UtcNow },
        new Payment(){ Id = 5, Bill = Bills.ElementAt(12), CreatedOn = DateTime.UtcNow }
      };
    }

    public void CreateBill(Bill bill)
    {
      if (bill == null) throw new ArgumentNullException(nameof(bill));
      Bills.Add(bill);
    }

    public void CreateClient(Client client)
    {
      if (client == null) throw new ArgumentNullException(nameof(client));
      Clients.Add(client);
    }

    public void CreatePayment(Payment payment)
    {
      if (payment == null) throw new ArgumentNullException(nameof(payment));
      Payments.Add(payment);
    }

    public void CreateService(Service service)
    {
      if (service == null) throw new ArgumentNullException(nameof(service));
      Services.Add(service);
    }

    public IEnumerable<Client> GetAllClients()
    {
      return Clients.ToList();
    }

    public IEnumerable<Service> GetAllServices()
    {
      return Services.ToList();
    }

    public Bill GetBillByClientServiceAndPeriod(Client client, Service service, int period)
    {
      return Bills.FirstOrDefault(e => e.Client.Id == client.Id && e.Service.Id == service.Id && e.Period == period);
    }

    public Bill GetBillById(int id)
    {
      return Bills.FirstOrDefault(e => e.Id == id);
    }

    public Client GetClientById(int id)
    {
      return Clients.FirstOrDefault(e => e.Id == id);
    }

    public Payment GetPaymentById(int id)
    {
      return Payments.FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<Payment> GetPaymentsByService(Service service)
    {
      return Payments.Where(e => e.Bill.Service.Id == service.Id).ToList();
    }

    public IEnumerable<Bill> GetPendingBillsByClient(int ClientId)
    {
      return Bills.Where(e => e.Client.Id == ClientId && e.Status == BillingStatus.Pending).ToList();
    }

    public Service GetServiceById(int id)
    {
      return Services.FirstOrDefault(e => e.Id == id);
    }

    public Service GetServiceByShortname(string shortname)
    {
      return Services.FirstOrDefault(e => e.Shortname == shortname);
    }

    public bool SaveChanges()
    {
      return true;
    }

    public void UpdateClient(Client client)
    {

    }

    public void UpdateService(Service service)
    {

    }
  }
}