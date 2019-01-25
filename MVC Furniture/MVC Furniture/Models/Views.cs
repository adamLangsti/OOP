using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Furniture.Models
{
    public class ViewModel
    {
        public List <Furniture> FurnitureList { get; set; }
        public UserData UserData { get; set; }

        public static ViewModel viewmodel(List<Furniture> furniturelist, UserData userdata)
        {
            ViewModel VM = new ViewModel();
            VM.FurnitureList = furniturelist;
            VM.UserData = userdata;
            return VM;
        }
    }
}