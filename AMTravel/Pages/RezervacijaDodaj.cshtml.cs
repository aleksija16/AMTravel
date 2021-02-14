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
    public class RezervacijaDodajModel : PageModel
    {

        public ObjectId SobaId{get;set;}
        
        [BindProperty]
        public Soba OvaSoba {get; set;}

        [BindProperty]
        public Korisnik TrenutniKorisnik { get; set; }


        [BindProperty]
        public Rezervacija RezervacijaSobe{get;set;}

        [BindProperty]
        public IList<Rezervacija> RezervacijaUzSobu{get;set;}

        [BindProperty]
        public int NeuspesnaRezervacija{get;set;}
        [BindProperty]
        public string DanasnjiDatum {get; set;}
        private IMongoCollection<Soba> kolekcija;
        private IMongoCollection<Rezervacija> kolekcijaR;
        private IMongoCollection<Korisnik> kolekcijaK;

        public IActionResult OnGet(string id)
        {

            String month = DateTime.Now.Month.ToString();
            if (DateTime.Now.Month < 10)
            {
                month = month.Insert(0, "0");
            }
            String day = DateTime.Now.Day.ToString();
            if (DateTime.Now.Day < 10)
            {
                day = day.Insert(0, "0");
            }
            DanasnjiDatum = DateTime.Now.Year.ToString() + "-" + month + "-" + day;

            return this.Page();
        }

        public IActionResult OnPost(string id)
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("AMTravelDb");
            kolekcija = db.GetCollection<Soba>("soba");
            SobaId = ObjectId.Parse(id);
            OvaSoba = kolekcija.Find(x => x.Id == SobaId).FirstOrDefault();
            kolekcijaR = db.GetCollection<Rezervacija>("rezervacija");
            kolekcijaK = db.GetCollection<Korisnik>("korisnik");
            if (SessionClass.SessionId != null)
            {
                ObjectId KorisnikId = ObjectId.Parse(SessionClass.SessionId);
                TrenutniKorisnik = kolekcijaK.Find(x => x.Id == KorisnikId).FirstOrDefault();
            }
            RezervacijaUzSobu = kolekcijaR.Find(x=>x.Soba.Id==SobaId && 
                                                ((x.DatumOd <= RezervacijaSobe.DatumOd && x.DatumDo>=RezervacijaSobe.DatumOd) || (x.DatumOd <= RezervacijaSobe.DatumDo && x.DatumDo>=RezervacijaSobe.DatumDo)) ).ToList();
            
            if (RezervacijaSobe.DatumOd > RezervacijaSobe.DatumDo)
            {
                NeuspesnaRezervacija = 1;
                return this.Page();
            }
            else if(RezervacijaUzSobu.Count!=0)
            {
                NeuspesnaRezervacija=2;
                return this.Page();
            }

            kolekcijaR.InsertOne(RezervacijaSobe);
            OvaSoba.Rezervacije.Add(new MongoDBRef("rezervacija", RezervacijaSobe.Id));
            TrenutniKorisnik.Rezervacije.Add(new MongoDBRef("rezervacija", RezervacijaSobe.Id));
            RezervacijaSobe.Soba = new MongoDBRef("soba", SobaId);
            RezervacijaSobe.Korisnik = new MongoDBRef("korisnik", TrenutniKorisnik.Id);

            kolekcijaR.ReplaceOne(x => x.Id == RezervacijaSobe.Id, RezervacijaSobe);
            kolekcija.ReplaceOne(x => x.Id == OvaSoba.Id, OvaSoba);
            kolekcijaK.ReplaceOne(x=>x.Id==TrenutniKorisnik.Id, TrenutniKorisnik);
            return RedirectToPage("./RezervacijaSve");
        }


    }
}
