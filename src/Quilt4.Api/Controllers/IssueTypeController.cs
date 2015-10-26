using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Cors.Core;
using Microsoft.AspNet.Mvc;
using Quilt4.Api.DataTransfer;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Quilt4.Api.Controllers
{
    [EnableCors("AllowAllOrigins")]
    [Route("api/[controller]")]
    public class IssueTypeController : Controller
    {
        private readonly List<IssueType> _issueTypes = new List<IssueType>
        {
            new IssueType
            {
                Id = "1",
                ProjectId = "1",
                ApplicationId = "1",
                VersionId = "1",
                Type = "System.Web.HttpException",
                Issues = 20,
                Enviroments = new List<string> { "Prod", "CI" },
                Last = DateTime.UtcNow.AddDays(-4),
                Level = "Error",
                Message = "The remote server returned an error: (404) Not Found.",
                StackTrace = "at HTK.Friskvardsofferten.Business.QuotnationBusiness.SaveSendQuotation(Int32 companyId, String getUserId, Int32 quotationRequestId, String description) in C:\\Dev\\Friskvårdsofferten\\HTK.Friskvardsofferten.Business\\QuotationBusiness.cs:line 337 \nat HTK.Friskvardsofferten.Controllers.CustomerController.SendQuotation() in C:\\Dev\\Friskvårdsofferten\\HTK.Friskvardsofferten\\Controllers\\CustomerController.cs:line 273 \nat lambda_method(Closure , ControllerBase , Object[] ) \nat System.Web.Mvc.ActionMethodDispatcher.Execute(ControllerBase controller, Object[] parameters) \nat System.Web.Mvc.ReflectedActionDescriptor.Execute(ControllerContext controllerContext, IDictionary`2 parameters) \nat System.Web.Mvc.ControllerActionInvoker.InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary`2 parameters) \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.b__39(IAsyncResult asyncResult, ActionInvocation innerInvokeState) \nat System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResult`2.CallEndDelegate(IAsyncResult asyncResult) \nat System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResultBase`1.End() \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.EndInvokeActionMethod(IAsyncResult asyncResult) \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.AsyncInvocationWithFilters.b__3d() \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.AsyncInvocationWithFilters.<>c__DisplayClass46.b__3f() \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.<>c__DisplayClass33.b__32(IAsyncResult asyncResult) \nat System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResult`1.CallEndDelegate(IAsyncResult asyncResult) \nat System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResultBase`1.End() \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.EndInvokeActionMethodWithFilters(IAsyncResult asyncResult) \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.<>c__DisplayClass21.<>c__DisplayClass2b.b__1c() \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.<>c__DisplayClass21.b__1e(IAsyncResult asyncResult)"
            },
            new IssueType
            {
                Id = "2",
                ProjectId = "1",
                ApplicationId = "1",
                VersionId = "1",
                Type = "System.Net.WebException",
                Issues = 20,
                Enviroments = new List<string> { "Dev", "CI" },
                Last = DateTime.UtcNow.AddDays(-3),
                Level = "Warning",
                Message = "The remote server returned an error: (404) Not Found.",
                StackTrace = "at HTK.Friskvardsofferten.Business.QuotnationBusiness.SaveSendQuotation(Int32 companyId, String getUserId, Int32 quotationRequestId, String description) in C:\\Dev\\Friskvårdsofferten\\HTK.Friskvardsofferten.Business\\QuotationBusiness.cs:line 337 \nat HTK.Friskvardsofferten.Controllers.CustomerController.SendQuotation() in C:\\Dev\\Friskvårdsofferten\\HTK.Friskvardsofferten\\Controllers\\CustomerController.cs:line 273 \nat lambda_method(Closure , ControllerBase , Object[] ) \nat System.Web.Mvc.ActionMethodDispatcher.Execute(ControllerBase controller, Object[] parameters) \nat System.Web.Mvc.ReflectedActionDescriptor.Execute(ControllerContext controllerContext, IDictionary`2 parameters) \nat System.Web.Mvc.ControllerActionInvoker.InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary`2 parameters) \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.b__39(IAsyncResult asyncResult, ActionInvocation innerInvokeState) \nat System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResult`2.CallEndDelegate(IAsyncResult asyncResult) \nat System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResultBase`1.End() \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.EndInvokeActionMethod(IAsyncResult asyncResult) \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.AsyncInvocationWithFilters.b__3d() \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.AsyncInvocationWithFilters.<>c__DisplayClass46.b__3f() \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.<>c__DisplayClass33.b__32(IAsyncResult asyncResult) \nat System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResult`1.CallEndDelegate(IAsyncResult asyncResult) \nat System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResultBase`1.End() \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.EndInvokeActionMethodWithFilters(IAsyncResult asyncResult) \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.<>c__DisplayClass21.<>c__DisplayClass2b.b__1c() \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.<>c__DisplayClass21.b__1e(IAsyncResult asyncResult)"
            },
            new IssueType
            {
                Id = "3",
                ProjectId = "1",
                ApplicationId = "1",
                VersionId = "1",
                Type = "Message",
                Issues = 20,
                Enviroments = new List<string> { "Prod", "Dev" },
                Last = DateTime.UtcNow,
                Level = "Information",
                Message = "Sending email to customer",
                StackTrace = "at HTK.Friskvardsofferten.Business.QuotnationBusiness.SaveSendQuotation(Int32 companyId, String getUserId, Int32 quotationRequestId, String description) in C:\\Dev\\Friskvårdsofferten\\HTK.Friskvardsofferten.Business\\QuotationBusiness.cs:line 337 \nat HTK.Friskvardsofferten.Controllers.CustomerController.SendQuotation() in C:\\Dev\\Friskvårdsofferten\\HTK.Friskvardsofferten\\Controllers\\CustomerController.cs:line 273 \nat lambda_method(Closure , ControllerBase , Object[] ) \nat System.Web.Mvc.ActionMethodDispatcher.Execute(ControllerBase controller, Object[] parameters) \nat System.Web.Mvc.ReflectedActionDescriptor.Execute(ControllerContext controllerContext, IDictionary`2 parameters) \nat System.Web.Mvc.ControllerActionInvoker.InvokeActionMethod(ControllerContext controllerContext, ActionDescriptor actionDescriptor, IDictionary`2 parameters) \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.b__39(IAsyncResult asyncResult, ActionInvocation innerInvokeState) \nat System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResult`2.CallEndDelegate(IAsyncResult asyncResult) \nat System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResultBase`1.End() \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.EndInvokeActionMethod(IAsyncResult asyncResult) \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.AsyncInvocationWithFilters.b__3d() \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.AsyncInvocationWithFilters.<>c__DisplayClass46.b__3f() \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.<>c__DisplayClass33.b__32(IAsyncResult asyncResult) \nat System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResult`1.CallEndDelegate(IAsyncResult asyncResult) \nat System.Web.Mvc.Async.AsyncResultWrapper.WrappedAsyncResultBase`1.End() \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.EndInvokeActionMethodWithFilters(IAsyncResult asyncResult) \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.<>c__DisplayClass21.<>c__DisplayClass2b.b__1c() \nat System.Web.Mvc.Async.AsyncControllerActionInvoker.<>c__DisplayClass21.b__1e(IAsyncResult asyncResult)"
            }
        };

        [HttpGet("{ProjectId}/{ApplicationId}/{VersionId}")]
        public IEnumerable<IssueType> Get(string projectId, string applicationId, string versionId)
        {
            return _issueTypes.Where(x => x.ProjectId == projectId && x.ApplicationId == applicationId && x.VersionId == versionId);
        }

        [HttpGet("{ProjectId}/{ApplicationId}/{VersionId}/{IssueTypeId}")]
        public IssueType Get(string projectId, string applicationId, string versionId, string issueTypeId)
        {
            return _issueTypes.FirstOrDefault(x => x.ProjectId == projectId && x.ApplicationId == applicationId && x.VersionId == versionId && x.VersionId == issueTypeId);
        }
    }
}