using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AgendaOnline.Models;

namespace AgendaOnline.Controllers
{
    public class ContactoesController : Controller
    {
        private AgendaModelContainer db = new AgendaModelContainer();

        // GET: Contactoes
        public ActionResult Index()
        {
            var contactos = db.Contactos.Where(x => x.Grupo.Usuario.Email == User.Identity.Name).Include(c => c.Grupo);
            return View(contactos.ToList());
        }

        // GET: Contactoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contacto contacto = db.Contactos.Find(id);
            if (contacto == null)
            {
                return HttpNotFound();
            }
            return View(contacto);
        }

        // GET: Contactoes/Create
        public ActionResult Create()
        {
            ViewBag.GrupoId = new SelectList(db.Grupos.Where(x=>x.Usuario.Email==User.Identity.Name), "Id", "Nombre");
            return View();
        }

        // POST: Contactoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Apellido,Email,Telf,GrupoId")] Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                db.Contactos.Add(contacto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GrupoId = new SelectList(db.Grupos.Where(x => x.Usuario.Email == User.Identity.Name), "Id", "Nombre", contacto.GrupoId);
            return View(contacto);
        }

        // GET: Contactoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contacto contacto = db.Contactos.Find(id);
            if (contacto == null)
            {
                return HttpNotFound();
            }
            ViewBag.GrupoId = new SelectList(db.Grupos.Where(x => x.Usuario.Email == User.Identity.Name), "Id", "Nombre", contacto.GrupoId);
            return View(contacto);
        }

        // POST: Contactoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Apellido,Email,Telf,GrupoId")] Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contacto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GrupoId = new SelectList(db.Grupos.Where(x => x.Usuario.Email == User.Identity.Name), "Id", "Nombre", contacto.GrupoId);
            return View(contacto);
        }

        // GET: Contactoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contacto contacto = db.Contactos.Find(id);
            if (contacto == null)
            {
                return HttpNotFound();
            }
            return View(contacto);
        }

        // POST: Contactoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contacto contacto = db.Contactos.Find(id);
            db.Contactos.Remove(contacto);
            db.SaveChanges();
            return RedirectToAction("Index");
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
