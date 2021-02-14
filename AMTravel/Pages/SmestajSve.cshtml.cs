using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Driver;
using MongoDB.Bson;

namespace AMTravel.Pages
{
    public class SmestajSveModel : PageModel
    {
        [BindProperty]
        public IList<Smestaj> SviSmestaji{get;set;}
       private IMongoCollection<Smestaj> kolekcija;

        public void OnGet(string id)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("AMTravelDb");
            kolekcija = db.GetCollection<Smestaj>("smestaj");
            ObjectId NoviId=ObjectId.Parse(id);
            SviSmestaji = kolekcija.Find(x => x.Destinacija.Id==NoviId).ToList();
        }
    }
}
