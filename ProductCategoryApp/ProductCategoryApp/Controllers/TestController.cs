using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductCategoryApp.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            SQLEntities entities = new SQLEntities();
            return View(entities.Tests);
        }

        [HttpPost]
        public JsonResult Create(Test test)
        {
            using (SQLEntities entities = new SQLEntities())
            {
                entities.Tests.Add(test);
                entities.SaveChanges();
            }

            return Json(test);
        }
    }
}