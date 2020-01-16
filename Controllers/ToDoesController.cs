using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ToDoApp.Models;

namespace ToDoApp.Controllers
{
    public class ToDoesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ToDoes
        //[Authorize]
        public ActionResult Index()        
        {
            return View();
        }

        //Gets users ToDo list
        private IEnumerable<ToDo> GetMyToDoes()
        {
            string currentUserId = User.Identity.GetUserId();

            ApplicationUser currentUser = db.Users.FirstOrDefault
                (x => x.Id == currentUserId);

            IEnumerable<ToDo> myToDoes = db.ToDos.ToList().Where(x => x.User == currentUser).OrderByDescending(x => x.Id);

            int completeCount = 0;
            foreach (ToDo toDo in myToDoes)
            {
                if (toDo.IsDone)
                {
                    completeCount++;
                }
            }

            if (myToDoes.Count() == 0)
            {
                ViewBag.Percent = 0;
            }
            else
            {
                ViewBag.Percent = Math.Round(100f * ((float)completeCount / (float)myToDoes.Count()));
            }
            
            ViewBag.Completed = "  Completed " + completeCount + " of " + myToDoes.Count() + "  ";
            return myToDoes;
        }

        //Returns Partial view HTM for AJAX script
        public ActionResult BuildToDoTable()
        {
            return PartialView("_ToDoTable", GetMyToDoes());
        }

        //Creates and returns Partial view with tables, so AJAX could refresh items without reloading page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AJAXCreate([Bind(Include = "Id,Description,Priority,DueDate")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();

                ApplicationUser currentUser = db.Users.FirstOrDefault
                    (x => x.Id == currentUserId);

                toDo.User = currentUser;
                toDo.IsDone = false;
                toDo.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                db.ToDos.Add(toDo);
                db.SaveChanges();
            }

            return PartialView("_ToDoTable", GetMyToDoes());
        }

        // GET: ToDoes/Edit/5 Opens Edit ToDo item page
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ToDo toDo = db.ToDos.Find(id);

            if (toDo == null)
            {
                return HttpNotFound();
            }

            string currentUserId = User.Identity.GetUserId();

            ApplicationUser currentUser = db.Users.FirstOrDefault
                (x => x.Id == currentUserId);

            if (toDo.User != currentUser)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(toDo);
        }


        // POST: ToDoes/Edit/5 Saves ToDo from Edit item page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Description,CreatedDate,IsDone,Priority,DueDate")] ToDo toDo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(toDo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(toDo);
        }


        //Changes ToDo Status to Completed/Uncompleted
        [HttpPost]
        public ActionResult AJAXEdit(int? id, bool value)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                ToDo toDo = db.ToDos.Find(id);
                if (toDo == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    toDo.IsDone = value;
                    db.Entry(toDo).State = EntityState.Modified;
                    db.SaveChanges();
                    return PartialView("_ToDoTable", GetMyToDoes());
                }
            }
            return View();
        }

        //Deletes ToDo record without reloading page
        [HttpPost]
        public ActionResult AJAXDelete(int? id)
        {
            if (ModelState.IsValid)
            {
                ToDo toDo = db.ToDos.Find(id);
                db.ToDos.Remove(toDo);
                db.SaveChanges();
                return PartialView("_ToDoTable", GetMyToDoes());
            }
            return View();
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
