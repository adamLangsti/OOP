using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace MVC_Furniture.Models
{
    public class Furniture
    {
        //Common traits for all furniture
        public int Id { get; set; }
        public bool Store { get; set; }
        public bool Delivery { get; set; }
        public int Count { get; set; }
        public int InitialCount { get; set; }
        public int Price { get; set; }
        public int Points { get; set; }
        public int RemoveCount { get; set; }
        public string Title { get; set; }
        public int BuyCount { get; set; }

        public static List <Furniture> CreateData()
        {
            List<Furniture> FurnitureList = new List<Furniture>();

            FurnitureList.Add(new Couch { Id=1, Title="Längtan", Seats = 4, Colour = "Grey", Store = true, Delivery = true, Count = 30, InitialCount=30, Price = 54999});
            FurnitureList.Add(new Couch { Id = 2, Title="Hörnan", Seats = 8, Colour = "Black", Store = false, Delivery = true, Count = 40, InitialCount=40, Price = 14999 });
            FurnitureList.Add(new Desk { Id = 3, Title="Sitdown", Draws = 4, Design = "Carina Bengs", Store = true, Delivery = false, Count = 10,InitialCount=10, Price = 8999 });
            FurnitureList.Add(new Desk { Id = 4 , Title="Sten", Draws = 6, Design = "Henrik Preutz", Store = true, Delivery = true, Count = 25, InitialCount=25, Price = 7999 });
            FurnitureList.Add(new BookShelf { Id = 5,Title="Puthere", Height = 2.5, Shelves = 6 , Store = false, Delivery = false, Count = 15,InitialCount=15, Price = 10999 });
            FurnitureList.Add(new BookShelf { Id = 6, Title="Förvara", Height = 4, Shelves = 8 , Store = true, Delivery = true, Count = 10,InitialCount=10, Price = 24999 });

            return FurnitureList;
        }

        public static string filepath = HttpContext.Current.Server.MapPath("~/App_Data/Storage/library.json");
        /// <summary>
        /// Saves data to JSON file
        /// </summary>
        /// <param name="furniturelist">List of class items</param>
        /// <returns></returns>
        public static bool SaveData(List<Furniture> furniturelist)
        {
            var settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented
            };

            string json = JsonConvert.SerializeObject(furniturelist.ToArray(), settings);
            System.IO.File.WriteAllText(filepath, json);

            return true;
        }

        public static List<Furniture> GetData()
        {
            List<Furniture> data;
            if (System.IO.File.Exists(filepath))
            {
                var settings = new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.Objects
                };
                var json = System.IO.File.ReadAllText(filepath);
                data = JsonConvert.DeserializeObject<List<Furniture>>(json, settings);

            }
            else
            {
                data = CreateData();
            }

            //Algoritm: gives more points to more popular products.

            data = data.OrderBy(x => x.Count).ToList();
            int points = 0;
            foreach (var d1 in data)
            {
                points = points + 5;
                d1.Points = points;
            }


            data = data.OrderBy(x => x.Count).ToList();
            points = 0;
            foreach (var d2 in data)
            {
                points = points + 3;
                d2.Points += points;
            }

            data = data.OrderByDescending(x => x.Points).ToList();
            SaveData(data);
            return data;
        }
    }

    // Unique traits for children

    public class Couch : Furniture
    {
        public int Seats { get; set; }
        public string Colour { get; set; }
    }

    public class Desk : Furniture
    {
        public int Draws { get; set; }
        public string Design { get; set; }
    }

    public class BookShelf : Furniture
    {
        public double Height { get; set; }
        public int Shelves { get; set; } 
    }
}