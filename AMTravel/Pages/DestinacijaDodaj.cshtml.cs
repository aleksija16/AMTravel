using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;

namespace AMTravel.Pages
{
    public class DestinacijaDodajModel : PageModel
    {
       
		[BindProperty]
        public Destinacija NovaDestinacija{get;set;}

        [BindProperty]
        public int? PostojiVec {get; set;}
        [BindProperty]
        public IList<Smestaj> SviSmestaji {get; set;}
          [BindProperty]
        public IList<int> IzabraniSmestaji{get; set;}
        private IMongoCollection<Destinacija> kolekcija;
        private IMongoCollection<Smestaj> kolekcijaS;
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {

                MongoClient client = new MongoClient("mongodb://localhost:27017");
                IMongoDatabase db = client.GetDatabase("AMTravelDb");
                kolekcija = db.GetCollection<Destinacija>("destinacija");
                Destinacija PostojiDestinacija = kolekcija.Find(x => x.Naziv == NovaDestinacija.Naziv).FirstOrDefault();

                if (PostojiDestinacija != null)
                {
                    PostojiVec = 1;
                    return this.Page();
                }

             /*   kolekcijaS = db.GetCollection<Smestaj>("smestaj");
                SviSmestaji = kolekcijaS.Find(x => true).ToList();*/
                kolekcija.InsertOne(NovaDestinacija);
            
                return RedirectToPage("./DestinacijaSve");
            }
        }
    }
}
