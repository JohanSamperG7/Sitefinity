using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace SitefinityWebApp.Mvc.Controllers
{
    [Route("api/Label")]
    public class TetsController : Controller
    {
        // GET: Tets
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public object GetLabelsData()
        {
            var resource = ResourceManager.GetManager();
            //Type canoneventsType = TypeResolutionService.ResolveType("Telerik.Sitefinity.Localization.Data.ResourceEntry");
            var response = resource.GetResourceOrEmpty(Thread.CurrentThread.CurrentCulture, "Labels", "labelTest");
            return null;
        }
    }
}