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
    public class SmestajIzmeniModel : PageModel
    {
        [BindProperty]
        public Smestaj TrenutniSmestaj { get; set; }
        [BindProperty]
        public Smestaj NoviSmestaj { get; set; }
        public ObjectId NoviId;
        private IMongoCollection<Smestaj> kolekcija;
        public IActionResult OnGet(string id)
        { 
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("AMTravelDb");
            kolekcija = db.GetCollection<Smestaj>("smestaj");
            NoviId = ObjectId.Parse(id);
            TrenutniSmestaj= kolekcija.Find(x => x.Id==NoviId).FirstOrDefault();
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
            kolekcija = db.GetCollection<Smestaj>("smestaj");
            NoviId = ObjectId.Parse(id);
            NoviSmestaj= kolekcija.Find(x => x.Id==NoviId).FirstOrDefault();
            NoviSmestaj.Naziv=TrenutniSmestaj.Naziv;
            NoviSmestaj.Adresa=TrenutniSmestaj.Adresa;
            NoviSmestaj.Opis=TrenutniSmestaj.Opis;       
            NoviSmestaj.Tip=TrenutniSmestaj.Tip;     
            kolekcija.ReplaceOne(x=>x.Id==NoviSmestaj.Id, NoviSmestaj);
            return RedirectToPage("./SmestajJedan",  new { id = id } );
        }

    }
}
