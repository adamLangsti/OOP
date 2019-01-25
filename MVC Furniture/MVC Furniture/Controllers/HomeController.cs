using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Furniture.Models;

namespace MVC_Furniture.Controllers
{
    public class HomeController : Controller
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

            return View(VM);
        }
        //Loop that adds or removes products
        public ActionResult Buy(int Id)
        {
            foreach (Models.Furniture furniture in furniturelist)
            {
                if (furniture.Id == Id && furniture.Count > 0)
                {
                    furniture.Count--;
                    furniture.BuyCount++;
                    Models.Furniture.SaveData(furniturelist);
                    userdata = UserData.GetUserData((int)Session["UserId"]);
                    if (userdata.KundvagnList == null)
                    {
                        userdata.KundvagnList = new List<Models.UserData.Kundvagn>();
                    }
                    userdata.KundvagnList.Add(new Models.UserData.Kundvagn { Id = furniture.Id, ReturnDate = DateTime.Now.AddDays(30) });
                    Models.UserData.SaveUserData(userdata);
                }
            }
            userdata = UserData.GetUserData((int)Session["UserId"]);
            ViewModel VM = ViewModel.viewmodel(furniturelist, userdata);
            return View("Index", VM);
        }

        public ActionResult RemoveCount(int id)
        {
            foreach (Models.Furniture furniture in furniturelist)
            {
                if (furniture.Id == id)
                {
                    furniture.Count++;
                    Models.Furniture.SaveData(furniturelist);
                    userdata = UserData.GetUserData((int)Session["UserId"]);
                    var itemToRemove = userdata.KundvagnList.FirstOrDefault(r => r.Id == id);
                    if (itemToRemove != null)
                    {
                        userdata.KundvagnList.Remove(itemToRemove);
                        Models.UserData.SaveUserData(userdata);
                    }
                }
            }


            ViewModel VM = ViewModel.viewmodel(furniturelist, userdata);

            return View("Index", VM);
        }
    }
}