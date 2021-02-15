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
    public class RezervacijaSveModel : PageModel
    {

        [BindProperty]
        public IList<Rezervacija> SveRezervacije{get;set;}
        [BindProperty]
        public IList<Rezervacija> MojeRezervacije{get;set;}
        [BindProperty]
        public IList<Soba> SveSobe { get; set; }
        private IMongoCollection<Rezervacija> kolekcija;
        private IMongoCollection<Soba> kolekcijaS;

        public void OnGet()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("AMTravelDb");
            kolekcija = db.GetCollection<Rezervacija>("rezervacija");
            kolekcijaS = db.GetCollection<Soba>("soba");
            SveSobe = kolekcijaS.Find(x => true).ToList();
            SveRezervacije = kolekcija.Find(x => true).ToList();
            
            if(SessionClass.SessionId!=null)
            {
            ObjectId KorisnikId=ObjectId.Parse(SessionClass.SessionId);
            MojeRezervacije=kolekcija.Find(x=>x.Korisnik.Id==KorisnikId).ToList();
            }

        }
        
    }
}
