using System;
using System.Collections.Generic;
using System.Text;

namespace XYZ.Customer.Contacts.Service.Business.DtoModels
{
    public class CustomerDTO
    {
        public string SocialSecurityNumber { get; set; }
        public string? EmailAddress { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
