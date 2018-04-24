using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WeddingSite.Models;

namespace WeddingSite.Controllers
{
    [RoutePrefix("api/FaqApi")]
    public class FaqApiController : ApiController
    {
        private WeddingManagementContext db = new WeddingManagementContext();

        //public void Options() { }

        [Route]
        // GET api/FaqApi
        public IQueryable<FAQ> GetFAQs()
        {
            return db.FAQs;
        }

        [Authorize(Roles = "Admin")]
        [Route("GetUnAnswered")]
        public IQueryable<PendingQuestion> GetPendingQuestions(bool includeWontAnswer = false)
        {
            var result = db.PendingQuestions.AsQueryable();

            if (!includeWontAnswer)
            {
                result = result.Where(q => q.WillAnswer);
            }

            return result;
        }

        [Authorize(Roles = "Admin")]
        [Route("UpdateUnAnswered")]
        public async Task<IHttpActionResult> PutPendingQuestion(int id, PendingQuestion question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != question.Id)
            {
                return BadRequest();
            }

            db.Entry(question).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PendingQuestionExists(id))
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

        [Route("{id}")]
        // GET api/FaqApi/5
        [ResponseType(typeof(FAQ))]
        public async Task<IHttpActionResult> GetFAQ(int id)
        {
            FAQ faq = await db.FAQs.FindAsync(id);
            if (faq == null)
            {
                return NotFound();
            }

            return Ok(faq);
        }

        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        // PUT api/FaqApi/5
        public async Task<IHttpActionResult> PutFAQ(int id, [FromBody]FAQ faq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != faq.FAQId)
            {
                return BadRequest();
            }

            db.Entry(faq).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FAQExists(id))
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

        public class AnsweredQuestion
        {
            public FAQ Faq { get; set; }

            public PendingQuestion AskedQuestion { get; set; }
        }

        [ResponseType(typeof(FAQ))]
        [Route("AnswerQuestion")]
        [Authorize(Roles = "Admin")]
        public async Task<IHttpActionResult> PostFAQ([FromBody]AnsweredQuestion question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FAQs.Add(question.Faq);
            await db.SaveChangesAsync();

            db.Entry(question.AskedQuestion).State = EntityState.Deleted;
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = question.Faq.FAQId }, question.Faq);
        }

        // POST api/FaqApi
        [ResponseType(typeof(PendingQuestion))]
        public async Task<IHttpActionResult> PostFAQ(PendingQuestion faq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PendingQuestions.Add(faq);
            await db.SaveChangesAsync();

            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = "mail.privateemail.com";
            client.EnableSsl = true;
            client.Timeout = 60000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("contact@anthonyaliciawedding.com", "WedM3!");

            MailMessage mm = new MailMessage(
                "contact@anthonyaliciawedding.com",
                "contact@anthonyaliciawedding.com",
                "Someone asked a question",
                string.Format("From: {0} Question: {1}", faq.ReplyEmail, faq.Question));
            mm.BodyEncoding = UTF8Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);

            //TODO: save new faqs to the db.

            return CreatedAtRoute("DefaultApi", new { id = faq.Id }, faq);
            //db.FAQs.Add(faq);
            //await db.SaveChangesAsync();

            //return CreatedAtRoute("DefaultApi", new { id = faq.FAQId }, faq);
        }

        [Authorize(Roles = "Admin")]
        // DELETE api/FaqApi/5
        [ResponseType(typeof(FAQ))]
        public async Task<IHttpActionResult> DeleteFAQ(int id)
        {
            FAQ faq = await db.FAQs.FindAsync(id);
            if (faq == null)
            {
                return NotFound();
            }

            db.FAQs.Remove(faq);
            await db.SaveChangesAsync();

            return Ok(faq);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FAQExists(int id)
        {
            return db.FAQs.Count(e => e.FAQId == id) > 0;
        }

        private bool PendingQuestionExists(int id)
        {
            return db.PendingQuestions.Any(e => e.Id == id);
        }
    }
}