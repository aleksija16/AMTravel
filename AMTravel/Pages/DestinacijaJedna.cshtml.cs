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
    public class DestinacijaJednaModel : PageModel
    {
        [BindProperty]
        public Destinacija TrenutnaDestinacija { get; set; }
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
    }
}
