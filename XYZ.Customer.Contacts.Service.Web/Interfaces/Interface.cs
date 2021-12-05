using XYZ.Customer.Contacts.Service.Web.ViewModels;

namespace XYZ.Customer.Contacts.Service.Web.Interfaces
{
    public interface ICustomerValidator
    {
        void Vaidate(CustomerViewModel customer);
        bool SecurityNumberValidator(string pnr, out string message);
    }
}
