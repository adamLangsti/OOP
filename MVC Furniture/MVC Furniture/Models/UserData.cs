using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;


namespace MVC_Furniture.Models
{
    public class UserData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List <Kundvagn> KundvagnList { get; set; }

        public static List<UserData> UserList = GetUsers();

        public static UserData GetUserData(string Email)
        {
            var selected = UserList.Where(x => x.Email == Email).FirstOrDefault();
            return selected;
        }
        
        public static UserData GetUserData(int id)
        {
            UserData userdata;
            string filepath = HttpContext.Current.Server.MapPath("~/App_Data/Storage/user" + id + ".json");

            if (System.IO.File.Exists(filepath))
            {
                var settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Objects
                };
                var json = System.IO.File.ReadAllText(filepath);
                userdata = JsonConvert.DeserializeObject<UserData>(json, settings);
            }
            else
            {
                userdata = UserList.Where(x => x.Id == id).FirstOrDefault();
            }

            return userdata;
        }
        public static void SaveUserData(UserData user)
        {
            string filepath = HttpContext.Current.Server.MapPath("~/App_Data/Storage/user" + user.Id + ".json");
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented
            };
            string json = JsonConvert.SerializeObject(user, settings);
            System.IO.File.WriteAllText(filepath, json);
        }

        public static List <UserData> GetUsers()
        {
            List<UserData> UserList = new List<UserData>();
            UserList.Add(new UserData { Id = 1, Email = "Adam.Lang-1@hotmail.com", Password = "hejsan", Name = "Adam Lang" });
            return UserList;
        }

        public class Kundvagn
        {
            public int Id;
            public DateTime ReturnDate;

        }
    }
}
