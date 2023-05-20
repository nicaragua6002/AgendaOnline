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
    [Authorize]
    public class GrupoesController : Controller
    {
        private AgendaModelContainer db = new AgendaModelContainer();

        // GET: Grupoes
        public ActionResult Index()
        {
            var grupos = db.Grupos.Where(x=>x.Usuario.Email== User.Identity.Name).Include(g => g.Usuario);
            return View(grupos.ToList());
        }

        // GET: Grupoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Grupos.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            return View(grupo);
        }

        // GET: Grupoes/Create
        public ActionResult Create()
        {
            //ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre");
            return View();
        }

        // POST: Grupoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Color,UsuarioId")] Grupo grupo)
        {
            grupo.UsuarioId = db.Usuarios.Where(x => x.Email == User.Identity.Name).FirstOrDefault().Id;
            if (ModelState.IsValid)
            {
                
                db.Grupos.Add(grupo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           // ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre", grupo.UsuarioId);
            return View(grupo);
        }

        // GET: Grupoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Grupos.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
          //  ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre", grupo.UsuarioId);
            return View(grupo);
        }

        // POST: Grupoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Color,UsuarioId")] Grupo grupo)
        {
            grupo.UsuarioId = db.Usuarios.Where(x => x.Email == User.Identity.Name).FirstOrDefault().Id;
            if (ModelState.IsValid)
            {
                db.Entry(grupo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.UsuarioId = new SelectList(db.Usuarios, "Id", "Nombre", grupo.UsuarioId);
            return View(grupo);
        }

        // GET: Grupoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grupo grupo = db.Grupos.Find(id);
            if (grupo == null)
            {
                return HttpNotFound();
            }
            return View(grupo);
        }

        // POST: Grupoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grupo grupo = db.Grupos.Find(id);
            db.Grupos.Remove(grupo);
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
