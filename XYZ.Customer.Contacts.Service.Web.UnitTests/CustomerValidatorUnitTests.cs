using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.ComponentModel.DataAnnotations;
using XYZ.Customer.Contacts.Service.Web.Validation;
using XYZ.Customer.Contacts.Service.Web.ViewModels;

namespace XYZ.Customer.Contacts.Service.Web.UnitTests
{
    [TestClass]
    public class CustomerValidatorUnitTests
    {
        private readonly CustomerValidator _sut;

        public CustomerValidatorUnitTests()
        {
            _sut = new CustomerValidator();
        }


        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void InvalidPersonalNumberFormula_ShouldThrowValidationException()
        {
            /// Arange
            var customer = new CustomerViewModel { SocialSecurityNumber = "000123456789" };

            /// Act
            _sut.Vaidate(customer);
             
             // Assert - Expects exception
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void NoDigitPerosnalNumber_ShouldThrowValidationException()
        {
            /// Arange
            var customer = new CustomerViewModel { SocialSecurityNumber = "asdfasdfs" };

            /// Act
            _sut.Vaidate(customer);

            // Assert - Expects exception
        }

        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void PrsonalIdentityNumberAsEmptyString_ShouldThrowValidationException()
        {
            /// Arange
            var customer = new CustomerViewModel { SocialSecurityNumber = "" };

            /// Act
            _sut.Vaidate(customer);

            // Assert - Expects exception
        }
        [TestMethod]
        [ExpectedException(typeof(ValidationException))]
        public void NullPersonalIdentityNumber_ShouldThrowValidationException()
        {
            /// Arange
            var customer = new CustomerViewModel { SocialSecurityNumber = null };

            /// Act
            _sut.Vaidate(customer);

            // Assert - Expects exception
        }
        [TestMethod]
        public void InvalidEmailAddress_ShouldReturnTheErrorMessage()
        {
            try
            {
                /// Arange
                var customer = new CustomerViewModel { SocialSecurityNumber = "199001012070", EmailAddress = "not valid" };

                /// Act
                _sut.Vaidate(customer);
                 
            }
            catch (Exception ex)
            {
                // Assert
                Assert.AreEqual(ex.Message, "Invalid personal number. ");
            }
        }
       
    }
}
