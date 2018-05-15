using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductCategoryApp.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
            ApplicationCategoryEntities entitiesCategory = new ApplicationCategoryEntities();
            return View(entitiesCategory.ProductCategories);
        }
        [HttpPost]
        public JsonResult Create(ProductCategory category)
        {
            using (ApplicationCategoryEntities entitiesCategory = new ApplicationCategoryEntities())
            {
                entitiesCategory.ProductCategories.Add(category);
                entitiesCategory.SaveChanges();
            }

            return Json(category);
        }
        [HttpPost]
        public ActionResult UpdateCategory(ProductCategory category)
        {
            using (ApplicationCategoryEntities entities = new ApplicationCategoryEntities())
            {
                ProductCategory updatedList = (from c in entities.ProductCategories
                                       where c.CategoryId == category.CategoryId
                                       select c).FirstOrDefault();
                updatedList.CategoryName = category.CategoryName;
                entities.SaveChanges();
            }

            return new EmptyResult();
        }

        [HttpPost]
        public ActionResult DeleteCategory(int categoryId)
        {
            using (ApplicationCategoryEntities entities = new ApplicationCategoryEntities())
            {
                ProductCategory categoryList = (from c in entities.ProductCategories
                                       where c.CategoryId == categoryId
                                       select c).FirstOrDefault();
                entities.ProductCategories.Remove(categoryList);
                entities.SaveChanges();
            }
            return new EmptyResult();
        }
    }
}