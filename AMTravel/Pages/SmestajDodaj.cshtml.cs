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
    public class SmestajDodajModel : PageModel
    {
       
		[BindProperty]
        public Smestaj NoviSmestaj{get;set;}

        [BindProperty]
        public int? PostojiVec {get; set;}
        private IMongoCollection<Smestaj> kolekcija;
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
                kolekcija = db.GetCollection<Smestaj>("smestaj");
                Smestaj PostojiSmestaj = kolekcija.Find(x => x.Naziv == NoviSmestaj.Naziv).FirstOrDefault();

                if (PostojiSmestaj != null)
                {
                    PostojiVec = 1;
                    return this.Page();
                }

                kolekcija.InsertOne(NoviSmestaj);
            
                return RedirectToPage("./SmestajSve");
            }
        }
    }
}
