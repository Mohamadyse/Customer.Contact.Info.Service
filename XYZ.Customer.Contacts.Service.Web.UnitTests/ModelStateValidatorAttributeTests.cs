using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using XYZ.Customer.Contacts.Service.Web.Validation;

namespace XYZ.Customer.Contacts.Service.Web.UnitTests
{
    [TestClass]
    public class ModelStateValidatorAttributeTests
    {
        #region Private declarations

        private readonly ModelStateValidatorAttribute _sut;
        private readonly ActionExecutingContext _actionExecutingContext;

        #endregion

        #region SetUp and TearDown

        public ModelStateValidatorAttributeTests()
        {
            var controllerMock = new Mock<Controller>();
            var actionContext = new ActionContext(
                new Mock<HttpContext>().Object,
                new Mock<Microsoft.AspNetCore.Routing.RouteData>().Object,
                new Mock<ActionDescriptor>().Object,
                new ModelStateDictionary());

            _actionExecutingContext = new ActionExecutingContext(
                actionContext,
                new List<IFilterMetadata>(),
                new Dictionary<string, object>(),
                controllerMock.Object);

            _sut = new ModelStateValidatorAttribute();
        }

        #endregion

        #region Tests

        [TestMethod]
        public void Construction_SetsOrder()
        {
            Assert.AreEqual(2, _sut.Order);
        }

        [TestMethod]
        public void OnActionExecuting_WithInvalidModelState_ShouldSetBadRequestResult()
        {
            _actionExecutingContext.ModelState.AddModelError("error", "error message");

            _sut.OnActionExecuting(_actionExecutingContext);

            var result = _actionExecutingContext.Result;
            Assert.IsTrue(result is BadRequestObjectResult);
        }

        [TestMethod]
        public void OnActionExecuting_WithValidModelState_ShouldDoNothing()
        {
            _sut.OnActionExecuting(_actionExecutingContext);

            var result = _actionExecutingContext.Result;
            Assert.IsFalse(result is BadRequestObjectResult);
        }

        #endregion

    }
}
