using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SpencerFA.DAL.Models;

namespace SpencerFA.DAL.Controllers
{
    public class UsersController : ApiController
    {
        private FlowerAppEntities db = new FlowerAppEntities();

        // GET: api/Users
        public List<tbl_users> Gettbl_users()
        {
            return db.tbl_users.ToList();
        }

        // GET: api/Users/5
        [ResponseType(typeof(tbl_users))]
        public IHttpActionResult Gettbl_users(int id)
        {
            tbl_users tbl_users = db.tbl_users.Find(id);
            if (tbl_users == null)
            {
                return NotFound();
            }

            return Ok(tbl_users);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_users(int id, tbl_users tbl_users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_users.ID_user)
            {
                return BadRequest();
            }

            db.Entry(tbl_users).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_usersExists(id))
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

        // POST: api/Users
        [ResponseType(typeof(tbl_users))]
        public IHttpActionResult Posttbl_users(tbl_users tbl_users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_users.Add(tbl_users);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_users.ID_user }, tbl_users);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(tbl_users))]
        public IHttpActionResult Deletetbl_users(int id)
        {
            tbl_users tbl_users = db.tbl_users.Find(id);
            if (tbl_users == null)
            {
                return NotFound();
            }

            db.tbl_users.Remove(tbl_users);
            db.SaveChanges();

            return Ok(tbl_users);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_usersExists(int id)
        {
            return db.tbl_users.Count(e => e.ID_user == id) > 0;
        }
    }
}