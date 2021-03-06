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
    public class SobaJednaModel : PageModel
    {
        [BindProperty]
        public Soba TrenutnaSoba { get; set; }
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
    }
}
