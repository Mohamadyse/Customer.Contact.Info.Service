using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using XYZ.Customer.Contacts.Service.Web.Interfaces;
using XYZ.Customer.Contacts.Service.Web.ViewModels;

namespace XYZ.Customer.Contacts.Service.Web.Validation
{
    public class CustomerValidator : ICustomerValidator
    {

        #region Public Methods

        public void Vaidate(CustomerViewModel customer)
        {
            string message;
            if (customer is null)
            {
                throw new ArgumentNullException(nameof(customer));
            }

            if (
            SecurityNumberValidator(customer.SocialSecurityNumber, out message) &&
            MailValidator(customer.EmailAddress, ref message) &&
            PhoneNumberValidator(customer.PhoneNumber, ref message))
                return;
            else throw new ValidationException(message);

        }

        #endregion

        #region Private Methods

        public bool SecurityNumberValidator(string pnr, out string message)
        {

            if (string.IsNullOrEmpty(pnr)) { message = ""; return false; }

            pnr = CleanString(pnr);
            if (string.IsNullOrEmpty(pnr) || (pnr.Length != 12) || (pnr.Length != 10)) { message = "Invalid personal number. "; return false; }

            var pattern = "^(19|20)?[0-9]{6}[0-9]{4}$";

            var pnrTenDigits = pnr.Substring(pnr.Length - 10);

            var valid = Regex.IsMatch(pnr, pattern) && (CheckLuhnAlgorithm(pnrTenDigits));

            if (valid) { message = ""; return true; }
            else { message = "Invalid personal number. "; return false; }
        }


        private static bool MailValidator(string email, ref string message)
        {
            if (string.IsNullOrEmpty(email)) return true;

            try
            {
                if (Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250))) { return true; }
                else
                {
                    message += "Invalid email address. ";
                    return false;
                };
            }
            catch (RegexMatchTimeoutException)
            {

                message += "Invalid email address. ";
                return false;
            };
        }

        private static bool PhoneNumberValidator(string phoneNumber, ref string message)
        {
            if (string.IsNullOrEmpty(phoneNumber)) return true;

            var pattern = "^([+]46)?([0-9]{9})$";


            if (Regex.IsMatch(phoneNumber, pattern)) return true;
            else
            {
                message += "Invalid phonenumber. ";
                return false;
            };
        }

        /// <summary>d
        /// Luhn algorithm for validating identification numbers, based on 10 digits
        /// </summary>
        /// <param name="value">Value to be validated</param>
        /// <returns></returns>
        private static bool CheckLuhnAlgorithm(string value)
        {
            return value.All(char.IsDigit) && value.Reverse()
            .Select(c => c - 48)
            .Select((thisNum, i) => i % 2 == 0
                ? thisNum
                : ((thisNum *= 2) > 9 ? thisNum - 9 : thisNum)
            ).Sum() % 10 == 0;
        }

        private static string CleanString(string text)
        {
            var strBuilder = new StringBuilder(text);

            foreach (var c in strBuilder.ToString())
            {
                if (!char.IsDigit(c))
                    strBuilder.Replace(c.ToString(), "");
            }

            return strBuilder.ToString();
        }

        #endregion

    }
}
