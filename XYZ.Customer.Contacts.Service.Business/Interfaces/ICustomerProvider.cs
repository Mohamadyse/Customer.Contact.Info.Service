using System.Collections.Generic;
using XYZ.Customer.Contacts.Service.Business.DtoModels;

namespace XYZ.Customer.Contacts.Service.Business.Interfaces
{
    public interface ICustomerProvider
    {
        CustomerDTO CreateCustomer(CustomerDTO customer);
        CustomerDTO DeleteCustomer(string pnr);
        List<CustomerDTO> GetAllCustomers();
        CustomerDTO GetCustomer(string prn);
        CustomerDTO UpdateCustomer(string pnr, CustomerDTO customer);
    }
}