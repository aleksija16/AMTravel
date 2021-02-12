using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Driver;

namespace AMTravel.Pages
{
    public class SmestajSveModel : PageModel
    {
        [BindProperty]
        public IList<Smestaj> SviSmestaji{get;set;}
       private IMongoCollection<Smestaj> kolekcija;

        public void OnGet()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("AMTravelDb");
            kolekcija = db.GetCollection<Smestaj>("smestaj");
            SviSmestaji = kolekcija.Find(x => true).ToList();

        }
    }
}
