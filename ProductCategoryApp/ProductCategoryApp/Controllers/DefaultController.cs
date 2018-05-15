using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductCategoryApp.Controllers
{
    public class DefaultController : Controller
    {
        public ActionResult Index()
        {
            ApplicationProductEntities entities = new ApplicationProductEntities();
            return View(entities.Products);
        }

        public ActionResult DynamicDropDown()
        {
            return View();
        }
        //public JsonResult GetCategory()
        //{
        //    List<ProductCategory> details = new List<ProductCategory>();
        //    SqlConnection con = new SqlConnection("Data Source=(Localdb)\\MSSQLLocalDB;Initial Catalog=Application;integrated Security=True");
        //    DataSet ds = new DataSet();
        //    SqlDataAdapter da = new SqlDataAdapter("select * from ProductCategory", con);
        //    da.Fill(ds);
        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        //    {
        //        ProductCategory category = new ProductCategory();
        //        category.CategoryName = ds.Tables[0].Rows[i]["CategoryName"].ToString();
        //        details.Add(category);
        //    }
        //    return Json(details, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult ShowData()
        {
            //var constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            //DataContext dc = new DataContext(constr);
            ApplicationCategoryEntities db = new ApplicationCategoryEntities();
            List<ProductCategory> lst = db.ProductCategories.ToList();
            return Json(lst, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
public JsonResult Create(Product product)
{
    using (ApplicationProductEntities entities = new ApplicationProductEntities())
    {
        entities.Products.Add(product);
        entities.SaveChanges();
    }

    return Json(product);
}

[HttpPost]
public ActionResult UpdateProduct(Product product)
{
    using (ApplicationProductEntities entities = new ApplicationProductEntities())
    {
        Product updatedList = (from c in entities.Products
                               where c.ProductId == product.ProductId
                               select c).FirstOrDefault();
        updatedList.ProductName = product.ProductName;
        updatedList.ProductCode = product.ProductCode;
        updatedList.Quantity = product.Quantity;
        updatedList.Amount = product.Amount;
        updatedList.TaxPercent = product.TaxPercent;
        updatedList.TaxAmount = product.TaxAmount;
        updatedList.NetAmount = product.NetAmount;
        updatedList.Category = product.Category;
        entities.SaveChanges();
    }

    return new EmptyResult();
}

[HttpPost]
public ActionResult DeleteProduct(int productId)
{
    using (ApplicationProductEntities entities = new ApplicationProductEntities())
    {
        Product productList = (from c in entities.Products
                               where c.ProductId == productId
                               select c).FirstOrDefault();
        entities.Products.Remove(productList);
        entities.SaveChanges();
    }
    return new EmptyResult();
}
    }
}