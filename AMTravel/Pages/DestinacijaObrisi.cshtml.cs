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
    public class DestinacijaObrisiModel : PageModel
    {
       
        public ObjectId NoviId{get;set;}
        [BindProperty]
        private Destinacija DestinacijaBrisanje{get;set;}
         public int Provera { get; set; }
        private IMongoCollection<Destinacija> kolekcija;
        public IActionResult OnPost(string id)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("AMTravelDb");
            kolekcija = db.GetCollection<Destinacija>("destinacija");
            NoviId = ObjectId.Parse(id);
            DestinacijaBrisanje = kolekcija.Find(x => x.Id == NoviId).FirstOrDefault();
            if(DestinacijaBrisanje.Smestaji.Count==0)
            {
            kolekcija.DeleteOne(x=> x.Id==DestinacijaBrisanje.Id);
            return RedirectToPage("./DestinacijaSve");
            }
            else
            {
                Provera=1;
                 return Page();
            }
            
        }
    }
}
