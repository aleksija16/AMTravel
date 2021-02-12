using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AMTravel.Pages
{
    public class SobaSveModel : PageModel
    {
        [BindProperty]
        public IList<Soba> SveSobe{get;set;}
        private IMongoCollection<Soba> kolekcija;
        public ObjectId SmestajId { get; set; }
        public void OnGet(string id)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("AMTravelDb");
            kolekcija = db.GetCollection<Soba>("soba");
            SmestajId = ObjectId.Parse(id);
            SveSobe = kolekcija.Find(x => x.Smestaj.Id==SmestajId).ToList();
            
            
        }
    }
}
