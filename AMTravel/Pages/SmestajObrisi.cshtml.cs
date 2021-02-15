using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AMTravel.Pages
{
    public class SmestajObrisiModel : PageModel
    {
       
        public ObjectId NoviId{get;set;}
        [BindProperty]
        private Smestaj SmestajBrisanje{get;set;}
         public int Provera { get; set; }
        private IMongoCollection<Smestaj> kolekcija;
        public IActionResult OnPost(string id)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("AMTravelDb");
            kolekcija = db.GetCollection<Smestaj>("smestaj");
            NoviId = ObjectId.Parse(id);
            SmestajBrisanje = kolekcija.Find(x => x.Id == NoviId).FirstOrDefault();
            if(SmestajBrisanje.Sobe.Count==0)
            {
            kolekcija.DeleteOne(x=> x.Id==SmestajBrisanje.Id);
            return RedirectToPage("./SmestajSve", new{id=id});
            }
            else
            {
                Provera=1;
                 return Page();
            }
            
        }
    }
}
