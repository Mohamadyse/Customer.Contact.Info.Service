using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using XYZ.Customer.Contacts.Service.Web.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XYZ.Customer.Contacts.Service.Web.Controllers.Versions.v1
{
    public class CustomerController : V1Controller
    {
        private readonly ICustomerValidator _customerValidator;

        public CustomerController(ICustomerValidator customerValidator)
        {
            _customerValidator = customerValidator;
        }


        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{pnr}")]
        public string Get(int pnr)
        {
            return "value";
        }

        [HttpPost]
        public void Post([FromBody] Models.Customer customer)
        {
            _customerValidator.Vaidate(customer);
        }

        [HttpPut("{pnr}")]
        public void Put(int pnr, [FromBody] Models.Customer customer)
        {
            _customerValidator.Vaidate(customer);

        }

        [HttpDelete("{pnr}")]
        public void Delete(int pnr)
        {
        }
    }
}
