using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyAdvices.BusinessLayer;
using MyAdvices.BusinessLayer.Result;
using MyAdvices.Entities;

namespace MyAdvices.Web.Controllers
{
    public class AdvicesUserController : Controller
    {
        private AdvicesUserManager advicesUserManager = new AdvicesUserManager();

        // GET: AdvicesUser
        public ActionResult Index()
        {
            return View(advicesUserManager.List());
        }

        // GET: AdvicesUser/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdvicesUser advicesUser = advicesUserManager.Find(x => x.Id == id.Value);
            if (advicesUser == null)
            {
                return HttpNotFound();
            }
            return View(advicesUser);
        }

        // GET: AdvicesUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdvicesUser/Create
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( AdvicesUser advicesUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                //TODO : düzeltilecek...
                BusinessLayerResult<AdvicesUser> res = advicesUserManager.Insert(advicesUser);
                if (res.Errors.Count>0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(advicesUser);
                }
                return RedirectToAction("Index");
            }

            return View(advicesUser);
        }

        // GET: AdvicesUser/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdvicesUser advicesUser = advicesUserManager.Find(x => x.Id == id.Value);
            if (advicesUser == null)
            {
                return HttpNotFound();
            }
            return View(advicesUser);
        }

        // POST: AdvicesUser/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( AdvicesUser advicesUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                //TODO : düzenlecenek...
                BusinessLayerResult<AdvicesUser> res = advicesUserManager.Update(advicesUser);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(advicesUser);
                }
                return RedirectToAction("Index");

                
            }
            return View(advicesUser);
        }

        // GET: AdvicesUser/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AdvicesUser advicesUser = advicesUserManager.Find(x => x.Id == id.Value);
            if (advicesUser == null)
            {
                return HttpNotFound();
            }
            return View(advicesUser);
        }

        // POST: AdvicesUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AdvicesUser advicesUser = advicesUserManager.Find(x => x.Id == id);
            advicesUserManager.Delete(advicesUser);
            return RedirectToAction("Index");
        }

        
    }
}
