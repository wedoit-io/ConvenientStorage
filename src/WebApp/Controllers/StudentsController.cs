namespace WebApp.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;
    using Model.DataAccessLayer.Contexts;
    using Model.Models;

    public class StudentsController : Controller
    {
        private readonly SchoolContext db = new SchoolContext();

        // GET: Students
        public ActionResult Index()
        {
            return this.View(this.db.Students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = this.db.Students.Find(id);
            if (student == null)
            {
                return this.HttpNotFound();
            }

            return this.View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            return this.View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,LastName,FirstMidName,EnrollmentDate")] Student student)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(student);
            }

            this.db.Students.Add(student);
            this.db.SaveChanges();

            return this.RedirectToAction("Index");
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = this.db.Students.Find(id);
            if (student == null)
            {
                return this.HttpNotFound();
            }

            return this.View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,LastName,FirstMidName,EnrollmentDate")] Student student)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(student);
            }

            this.db.Entry(student).State = EntityState.Modified;
            this.db.SaveChanges();

            return this.RedirectToAction("Index");
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var student = this.db.Students.Find(id);
            if (student == null)
            {
                return this.HttpNotFound();
            }

            return this.View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var student = this.db.Students.Find(id);
            this.db.Students.Remove(student);
            this.db.SaveChanges();

            return this.RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
