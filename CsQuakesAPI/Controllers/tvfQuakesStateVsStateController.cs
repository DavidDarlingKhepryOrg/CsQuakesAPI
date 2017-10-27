using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using CsQuakesAPI;

namespace CsQuakesAPI.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using CsQuakesAPI;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<tvf_Quakes_State_vs_State_Result>("tvfQuakesStateVsState");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class tvfQuakesStateVsStateController : ODataController
    {
        private earthquakesEntities db = new earthquakesEntities();

        // GET: odata/tvfQuakesStateVsState?state1='Arizona'&state2='Kansas'&$orderby=cc,Year desc,admin1
        [EnableQuery]
        public IQueryable<tvf_Quakes_State_vs_State_Result> GettvfQuakesStateVsState([FromODataUri] string state1, [FromODataUri] string state2)
        {
            return db.tvf_Quakes_State_vs_State(state1, state2);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
