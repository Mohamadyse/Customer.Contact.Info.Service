using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XYZ.Customer.Contacts.Service.Web.Controllers.Versions.v1
{
    public class CustomerController : V1Controller
    {
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
        public void Post([FromBody] string value)
        {
        }

        [HttpPut("{pnr}")]
        public void Put(int pnr, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int pnr)
        {
        }
    }
}
