using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using XYZ.Customer.Contacts.Service.Business.DtoModels;
using XYZ.Customer.Contacts.Service.Business.Interfaces;
using XYZ.Customer.Contacts.Service.Web.Interfaces;
using XYZ.Customer.Contacts.Service.Web.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XYZ.Customer.Contacts.Service.Web.Controllers.Versions.v1
{
    public class CustomerController : V1Controller
    {
        private readonly ICustomerValidator _customerValidator;
        private readonly ICustomerProvider _customerProvider;
        public CustomerController(ICustomerValidator customerValidator, ICustomerProvider customerProvider)
        {
            _customerValidator = customerValidator;
            _customerProvider = customerProvider;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var result = _customerProvider.GetAllCustomers().Select(MapCustomerDTOToViewModel);
                return new JsonResult(result) { StatusCode = (int)HttpStatusCode.OK };
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error occured at {nameof(Get)}.");
            }
        }

        [HttpGet("{pnr}")]
        public IActionResult Get(string pnr)
        {
            try
            {
                if (_customerValidator.SecurityNumberValidator(pnr, out var message))
                {
                    var found = _customerProvider.GetCustomer(pnr);
                    if (found ==null) return NotFound();
                    var result = MapCustomerDTOToViewModel(found);
                    return new JsonResult(result) { StatusCode = (int)HttpStatusCode.OK };
                }
                return new JsonResult(message) { StatusCode = (int)HttpStatusCode.BadRequest };
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error occured at {nameof(Get)}.");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] CustomerViewModel customer)
        {
            try
            {
                _customerValidator.Vaidate(customer);
                return new JsonResult(_customerProvider.CreateCustomer(MapCustomerViewModelToDTO(customer))) { StatusCode = (int)HttpStatusCode.Created };
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     $"Error occured at {nameof(Post)}.");
            }
        }


        [HttpPut("{pnr}")]
        public IActionResult Put(string pnr, [FromBody] CustomerViewModel customer)
        {
            try
            {
                if (_customerValidator.SecurityNumberValidator(pnr, out var message) && pnr == customer.SocialSecurityNumber)
                {
                    _customerValidator.Vaidate(customer);
                    var result = _customerProvider.UpdateCustomer(pnr, MapCustomerViewModelToDTO(customer));
                    if (result == null) return NotFound();
                    return new JsonResult( result );
                }
                return new JsonResult(message) { StatusCode = (int)HttpStatusCode.BadRequest };
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Error occured at {nameof(Put)}.");
            }
        }

        [HttpDelete("{pnr}")]
        public IActionResult Delete(string pnr)
        {
            try
            {
                if (_customerValidator.SecurityNumberValidator(pnr, out var message))
                {
                    var result = _customerProvider.DeleteCustomer(pnr);

                    if (result == null) return NotFound();

                    return new JsonResult(result) { StatusCode = (int)HttpStatusCode.OK };

                }
                return new JsonResult(message) { StatusCode = (int)HttpStatusCode.BadRequest };
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                     $"Error occured at {nameof(Delete)}.");
            }
        }

        [NonAction]
        private CustomerViewModel MapCustomerDTOToViewModel(CustomerDTO c)
        {
            return new CustomerViewModel { SocialSecurityNumber = c.SocialSecurityNumber, EmailAddress = c.EmailAddress, PhoneNumber = c.PhoneNumber };
        }

        [NonAction]
        private CustomerDTO MapCustomerViewModelToDTO(CustomerViewModel c)
        {
            return new CustomerDTO { SocialSecurityNumber = c.SocialSecurityNumber, EmailAddress = c.EmailAddress, PhoneNumber = c.PhoneNumber };
        }
    }
}
