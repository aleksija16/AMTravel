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
    public class DestinacijaIzmeniModel : PageModel
    {
        [BindProperty]
        public Destinacija TrenutnaDestinacija { get; set; }
        [BindProperty]
        public Destinacija NovaDestinacija { get; set; }
        public string NovaSlika { get; set; }

        public ObjectId NoviId;
        private IMongoCollection<Destinacija> kolekcija;
        public IActionResult OnGet(string id)
        { 
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("AMTravelDb");
            kolekcija = db.GetCollection<Destinacija>("destinacija");
            NoviId = ObjectId.Parse(id);
            TrenutnaDestinacija= kolekcija.Find(x => x.Id==NoviId).FirstOrDefault();
            return Page();
        }

        public IActionResult OnPost(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("AMTravelDb");
            kolekcija = db.GetCollection<Destinacija>("destinacija");
            NoviId = ObjectId.Parse(id);
            NovaDestinacija= kolekcija.Find(x => x.Id==NoviId).FirstOrDefault();
          
                NovaDestinacija.Naziv=TrenutnaDestinacija.Naziv;
            
           
                NovaDestinacija.Drzava=TrenutnaDestinacija.Drzava;
            
                NovaDestinacija.Opis=TrenutnaDestinacija.Opis;
            
            kolekcija.ReplaceOne(x=>x.Id==NovaDestinacija.Id, NovaDestinacija);
            return RedirectToPage("./DestinacijaJedna",  new { id = id } );
        }

    }
}
