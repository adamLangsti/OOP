using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Furniture.Models;

namespace MVC_Furniture.Controllers
{
    public class CartController : Controller
    {

        public List<Furniture> furniturelist = Furniture.GetData();
        public UserData userdata;

        public ActionResult Index()
        {
            if (Session["UserId"] is int)
            {
                userdata = UserData.GetUserData((int)Session["UserId"]);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

            ViewModel VM = ViewModel.viewmodel(furniturelist, userdata);

            
            return View("Index", VM);
            
        }
    }
}
