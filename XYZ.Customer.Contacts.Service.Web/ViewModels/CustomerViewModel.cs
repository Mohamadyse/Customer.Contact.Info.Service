using System.ComponentModel.DataAnnotations;

namespace XYZ.Customer.Contacts.Service.Web.ViewModels
{
    public class CustomerViewModel
    {
        [Required]
        public string SocialSecurityNumber { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
    }
}
