using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XYZ.Customer.Contacts.Service.Business.DtoModels;
using XYZ.Customer.Contacts.Service.Business.Interfaces;
using XYZ.Customer.Contacts.Service.Data.Models;

namespace XYZ.Customer.Contacts.Service.Business.Providers
{
    public class CustomerProvider : ICustomerProvider
    {
        public List<CustomerDTO> GetAllCustomers()
        {
            using (var context = new CustomersDBContext())
            {

                return context.CustomersDb.Select(MapCustomer).ToList();
            }
        }

        public CustomerDTO GetCustomer(string prn)
        {
            using (var context = new CustomersDBContext())
            {
                var found = context.CustomersDb.FirstOrDefault(c => c.Prn == prn);
                if (found == null) return null;
                return MapCustomer(found);
            }
        }

        public CustomerDTO CreateCustomer(CustomerDTO customer)
        {
            using (var context = new CustomersDBContext())
            {
                context.CustomersDb.Add(new CustomerDBModel { Prn = customer.SocialSecurityNumber, Email = customer.EmailAddress, PhoneNumber = customer.PhoneNumber });
                context.SaveChanges();
                return customer;
            }
        }

        public CustomerDTO UpdateCustomer(string pnr, CustomerDTO customer)
        {
            using (var context = new CustomersDBContext())
            {
                if (
                 context.CustomersDb.Any(c => c.Prn == pnr))
                {
                    var objectToModify = context.CustomersDb.Find(pnr);
                    objectToModify.Email = customer.EmailAddress;
                    objectToModify.PhoneNumber = customer.PhoneNumber;
                    context.SaveChanges();
                    return customer;
                }
                return null;
            }
        }

        public CustomerDTO DeleteCustomer(string pnr)
        {
            using (var context = new CustomersDBContext())
            {
                var found = context.CustomersDb.Find(pnr);
                if (found != null)
                {
                    context.CustomersDb.Remove(found);
                    context.SaveChanges();
                    return MapCustomer(found);
                }
            }
            return null;
        }

        private CustomerDTO MapCustomer(CustomerDBModel customer)
        {
            return new CustomerDTO
            {
                SocialSecurityNumber = customer.Prn,
                PhoneNumber = customer.PhoneNumber,
                EmailAddress = customer.Email
            };
        }
    }
}
