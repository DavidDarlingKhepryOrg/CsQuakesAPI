using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using CsQuakesAPI;

namespace CsQuakesAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Quake>("Quakes");
            builder.EntitySet<tvf_Quakes_State_vs_State_Result>("tvfQuakesStateVsState");
            builder.EntitySet<usp_Quakes_State_vs_State_Result>("uspQuakesStateVsState");

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapODataServiceRoute(
                routeName: "odata",
                routePrefix: "odata",
                model: builder.GetEdmModel()
            );

            config.AddODataQueryFilter();
        }
    }
}
