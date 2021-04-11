using System.Collections.Generic;
using BasicBilling.Data.Entities;

namespace BasicBilling.Data.Interfaces
{
  public interface IBasicBillingRepo
  {
    bool SaveChanges();
    
    #region Clients
    IEnumerable<Client> GetAllClients();
    Client GetClientById(int id);
    void CreateClient(Client client);
    void UpdateClient(Client client);
    #endregion

    #region Services
    IEnumerable<Service> GetAllServices();
    Service GetServiceByShortname(string shortname);
    Service GetServiceById(int id);
    void CreateService(Service service);
    void UpdateService(Service service);
    #endregion

    #region Bills
    void CreateBill(Bill bill);
    Bill GetBillById(int id);
    Bill GetBillByClientServiceAndPeriod(Client client, Service service, int period);
    IEnumerable<Bill> GetPendingBillsByClient(int ClientId);
    #endregion

    #region Payments
    Payment GetPaymentById(int id);
    IEnumerable<Payment> GetPaymentsByService(Service service);
    void CreatePayment(Payment payment);
    #endregion
  }
}