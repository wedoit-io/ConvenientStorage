namespace WebApp.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Model.DataAccessLayer.Contexts;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var schoolContext = new SchoolContext();
            var students = schoolContext.Students.ToList();

            return this.View(students);
        }
    }
}
