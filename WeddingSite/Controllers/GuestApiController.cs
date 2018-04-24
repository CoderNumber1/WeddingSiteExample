using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WeddingSite.Models;
using WeddingSite.Models.GuestApi;

namespace WeddingSite.Controllers
{
    [RoutePrefix("api/GuestApi")]
    [Authorize(Roles = "Admin")]
    public class GuestApiController : ApiController
    {
        private WeddingManagementContext db = new WeddingManagementContext();

        // GET api/GuestApi
        [Route("")]
        public IQueryable<Guest> GetGuests()
        {
            return db.Guests.Include(g => g.GuestCodes);
        }

        // GET api/GuestApi/5
        [Route("{id}", Name = "GetGuest")]
        [ResponseType(typeof(Guest))]
        public async Task<IHttpActionResult> GetGuest(int id)
        {
            Guest guest = await db.Guests.Include(g => g.GuestCodes).FirstOrDefaultAsync(g => g.GuestId == id);
            if (guest == null)
            {
                return NotFound();
            }

            return Ok(guest);
        }

        // PUT api/GuestApi/5
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IHttpActionResult> PutGuest([FromBody]Guest guest, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != guest.GuestId)
            {
                return BadRequest();
            }

            db.Entry(guest).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/GuestApi
        [Route("")]
        [ResponseType(typeof(Guest))]
        public async Task<IHttpActionResult> PostGuest(Guest guest)
        {
            // TODO: Need to generate guest codes here
            guest.GuestCode = string.Empty;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Guests.Add(guest);
            await db.SaveChangesAsync();

            return CreatedAtRoute("GetGuest", new { id = guest.GuestId }, guest);
        }

        // DELETE api/GuestApi/5
        [Route("{id}")]
        [ResponseType(typeof(Guest))]
        public async Task<IHttpActionResult> DeleteGuest(int id)
        {
            Guest guest = await db.Guests.FindAsync(id);
            if (guest == null)
            {
                return NotFound();
            }

            db.Guests.Remove(guest);
            await db.SaveChangesAsync();

            return Ok(guest);
        }

        [HttpPut]
        [Route("GenerateCode")]
        [ResponseType(typeof(GuestCode))]
        public async Task<IHttpActionResult> GenerateGuestCode([FromBody]GuestCodeRequest codeRequest)
        {
            var guestCode = new GuestCode();
            guestCode.GuestCode1 = GuestCodeGenerator.RandomString(6);
            guestCode.UseLimit = codeRequest.UseCount ?? 1;

            if (codeRequest.GuestId != null)
            {
                Guest guest = await db.Guests.FindAsync(codeRequest.GuestId);

                if (guest == null)
                {
                    return NotFound();
                }

                guestCode.GuestId = guest.GuestId;
                guestCode.UseLimit = codeRequest.GuestId ?? guest.MaxAllowed;
            }

            db.GuestCodes.Add(guestCode);

            await db.SaveChangesAsync();

            db.Entry(guestCode).State = EntityState.Detached;

            return Ok(guestCode);
        }

        [HttpPut]
        [Route("GenerateAllCodes")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> GenerateAllGuestCodes()
        {
            IEnumerable<Guest> guests = db.Guests.AsNoTracking().ToList();

            foreach (Guest guest in guests)
            {
                if (!db.GuestCodes.Any(gc => gc.GuestId == guest.GuestId))
                {
                    var guestCode = new GuestCode();
                    guestCode.GuestCode1 = GuestCodeGenerator.RandomString(6);
                    guestCode.GuestId = guest.GuestId;
                    guestCode.UseLimit = guest.MaxAllowed;
                    guestCode.GuestId = guest.GuestId;

                    db.GuestCodes.Add(guestCode);

                    await db.SaveChangesAsync();
                }
            }

            return Ok();
        }

        [HttpGet]
        [Route("GuestFile")]
        public HttpResponseMessage GetGuestFile()
        {
            IEnumerable<Tuple<Guest, GuestCode>> guests = db.Guests.Join(db.GuestCodes,
                g => g.GuestId,
                gc => gc.GuestId,
                (g, gc) => new { Guest = g, GuestCode = gc })
                .ToList()
                .GroupBy(gcg => gcg.Guest)
                .Select(gcgg => new Tuple<Guest, GuestCode>(gcgg.Key, gcgg.FirstOrDefault().GuestCode))
                .ToList();

            if (guests.Any())
            {
                var result = new HttpResponseMessage(HttpStatusCode.OK);
                using (var guestFileStream = new MemoryStream())
                using (var writer = new StreamWriter(guestFileStream))
                {
                    foreach (Tuple<Guest, GuestCode> guest in guests)
                    {
                        writer.WriteLine();
                        writer.WriteLine(string.Format("Guest Name: {0}", guest.Item1.Name));
                        writer.WriteLine(string.Format("Guests Allowed: {0}", guest.Item1.MaxAllowed));
                        writer.WriteLine(string.Format("Guest Code: {0}", guest.Item2.GuestCode1));
                        writer.WriteLine("- To RSVP online, go to: http://anthonyaliciawedding.com/rsvp and enter the guest code above.");
                        writer.WriteLine("- If you have any questions check out our F.A.Q. page first: http://anthonyaliciawedding.com/faq or send us an email at: contact@anthonyaliciawedding.com.");
                        writer.WriteLine("- Be sure to use your guest code to check out special guest only information at http://anthonyaliciawedding.com/guest.");
                        writer.WriteLine();
                    }

                    result.Content = new ByteArrayContent(guestFileStream.ToArray());
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/text");
                }

                return result;
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GuestExists(int id)
        {
            return db.Guests.Count(e => e.GuestId == id) > 0;
        }
    }
}