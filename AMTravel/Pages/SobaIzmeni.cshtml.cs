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
    public class SobaIzmeniModel : PageModel
    {
        [BindProperty]
        public Soba TrenutnaSoba { get; set; }
        [BindProperty]
        public Soba NovaSoba { get; set; }
        public ObjectId NoviId;
        private IMongoCollection<Soba> kolekcija;
        public IActionResult OnGet(string id)
        { 
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("AMTravelDb");
            kolekcija = db.GetCollection<Soba>("soba");
            NoviId = ObjectId.Parse(id);
            TrenutnaSoba= kolekcija.Find(x => x.Id==NoviId).FirstOrDefault();
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
            kolekcija = db.GetCollection<Soba>("soba");
            NoviId = ObjectId.Parse(id);
            NovaSoba= kolekcija.Find(x => x.Id==NoviId).FirstOrDefault();
            NovaSoba.BrojSobe=TrenutnaSoba.BrojSobe;
            NovaSoba.BrojOsoba=TrenutnaSoba.BrojOsoba;
            NovaSoba.Cena=TrenutnaSoba.Cena;
            NovaSoba.Opis=TrenutnaSoba.Opis;            
            kolekcija.ReplaceOne(x=>x.Id==NovaSoba.Id, NovaSoba);
            return RedirectToPage("./SobaJedna",  new { id = id } );
        }

    }
}
