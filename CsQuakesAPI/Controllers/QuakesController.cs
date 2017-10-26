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
    builder.EntitySet<Quake>("Quakes");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class QuakesController : ODataController
    {
        private earthquakesEntities db = new earthquakesEntities();

        // GET: odata/Quakes
        [EnableQuery]
        public IQueryable<Quake> GetQuakes()
        {
            return db.Quakes;
        }

        // GET: odata/Quakes(5)
        [EnableQuery]
        public SingleResult<Quake> GetQuake([FromODataUri] string key)
        {
            return SingleResult.Create(db.Quakes.Where(quake => quake.Event_ID == key));
        }

        // PUT: odata/Quakes(5)
        public async Task<IHttpActionResult> Put([FromODataUri] string key, Delta<Quake> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Quake quake = await db.Quakes.FindAsync(key);
            if (quake == null)
            {
                return NotFound();
            }

            patch.Put(quake);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuakeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(quake);
        }

        // POST: odata/Quakes
        public async Task<IHttpActionResult> Post(Quake quake)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Quakes.Add(quake);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (QuakeExists(quake.Event_ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(quake);
        }

        // PATCH: odata/Quakes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<Quake> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Quake quake = await db.Quakes.FindAsync(key);
            if (quake == null)
            {
                return NotFound();
            }

            patch.Patch(quake);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuakeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(quake);
        }

        // DELETE: odata/Quakes(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            Quake quake = await db.Quakes.FindAsync(key);
            if (quake == null)
            {
                return NotFound();
            }

            db.Quakes.Remove(quake);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool QuakeExists(string key)
        {
            return db.Quakes.Count(e => e.Event_ID == key) > 0;
        }
    }
}
