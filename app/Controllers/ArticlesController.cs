using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using app.DAL;
using app.Models;
using System.ComponentModel.DataAnnotations;
using PagedList;

namespace app.Controllers
{

    public class ArticlesController : Controller
    {
        private ArticleContext db = new ArticleContext();

        // GET: Articles
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.IntSortParm = sortOrder == "int" ? "int_desc" : "Int";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var articles = from s in db.Articles
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                articles = articles.Where(s => s.Title.Contains(searchString)
                                       || s.Content.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "int_desc":
                    articles = articles.OrderByDescending(s => s.Views);
                    break;
                case "int":
                    articles = articles.OrderBy(s => s.Views);
                    break;
                case "name_desc":
                    articles = articles.OrderByDescending(s => s.Title);
                    break;
                case "Date":
                    articles = articles.OrderBy(s => s.CreatedDate);
                    break;
                case "date_desc":
                    articles = articles.OrderByDescending(s => s.CreatedDate);
                    break;
                default:
                    articles = articles.OrderBy(s => s.Title);
                    break;
            }

            articles.ToList().ForEach(article => { article.Content = article.Content.ToString().Substring(0, 3); });

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(articles.ToPagedList(pageNumber, pageSize));
            
        }

        // GET: Articles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // GET: Articles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title, Content, CreatedDate, FirstName, LastName, CategoryName")]Article article)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Articles.Add(article);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(article);
        }

        // GET: Articles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Content,CreatedDate, FirstName, LastName, CategoryName, Views")] Article article)
        {
            if (ModelState.IsValid)
            {
                db.Entry(article).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(article);
        }

        // GET: Articles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = db.Articles.Find(id);
            db.Articles.Remove(article);
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
