using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PagedList;
using _1081759project.Models;

namespace _1081759project.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Authorize(Users = "CCJ")]

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();   
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(string id, string Pwd)
        {
            string[] idAry = new string[] { "CCJ" };
            string[] pwdAry = new string[] { "123" };

            int index = -1;
            for (int i = 0; i < idAry.Length; i++)
            {
                if (idAry[i] == id && pwdAry[i] == Pwd)
                {
                    index = i;
                    break;
                }
            }
            if (index == -1)
            {
                ViewBag.Err = "帳密錯誤!";
            }
            else
            {
                FormsAuthentication.RedirectFromLoginPage(id, true);
                return RedirectToAction("Index");
            }
            return View();
        }




        // GET: Home
        dbFoodEntities db = new dbFoodEntities();

        int pageSize = 10;

        public ActionResult Page(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var goods = db.TableFoods1081759.OrderBy(m => m.foodId).ToList();
            var result = goods.ToPagedList(currentPage, pageSize);
            return View(result);
        }

        public ActionResult Index(int fId = 1)
        {
            ViewBag.FactoryName = db.TableFactorys1081759
                .Where(m => m.factoryId == fId)
                .FirstOrDefault().factoryName + "菜單";
            CVMFacFood vm = new CVMFacFood()
            {
                factory = db.TableFactorys1081759.ToList(),
                food = db.TableFoods1081759
                    .Where(m => m.factoryId == fId).ToList()
            };
            return View(vm);
        }
        public ActionResult Create()
        {
            return View(db.TableFactorys1081759.ToList());
        }

        [HttpPost]
        public ActionResult Create(TableFoods1081759 food)
        {
            try
            {
                db.TableFoods1081759.Add(food);
                db.SaveChanges();
                return RedirectToAction
                    ("Index", new { fId = food.factoryId });
            }
            catch (Exception ex)
            { }

            return View(food);
        }

        public ActionResult Edit(string foodId)
        {
            var food = db.TableFoods1081759
                .Where(m => m.foodId == foodId).FirstOrDefault();
            return View(food);
        }

        [HttpPost]
        public ActionResult Edit(TableFoods1081759 food)
        {
            if (ModelState.IsValid)
            {
                var temp = db.TableFoods1081759
                    .Where(m => m.foodId == food.foodId)
                    .FirstOrDefault();
                temp.foodName = food.foodName;
                temp.foodPrice = food.foodPrice;
                temp.foodStock = food.foodStock;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(food);
        }



        public ActionResult Delete(string foodId)
        {
            var food = db.TableFoods1081759.Where
               (m => m.foodId == foodId).FirstOrDefault();
            db.TableFoods1081759.Remove(food);
            db.SaveChanges();
            return RedirectToAction
               ("Index", new { fId = food.factoryId });
        }

        
    }
}