using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Driver;

namespace AMTravel.Pages
{
    public class DestinacijaSveModel : PageModel
    {

        [BindProperty]
        public IList<Destinacija> SveDestinacije{get;set;}
        public SelectList SveDrzave {get; set;}
        [BindProperty]
        public IList<Destinacija> DestinacijeFiltrirano{get;set;}
        
        [BindProperty]
        public string IzabranaDrzava {get; set;}
        private IMongoCollection<Destinacija> kolekcija;

        public void OnGet()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("AMTravelDb");
            kolekcija = db.GetCollection<Destinacija>("destinacija");
            SveDestinacije = kolekcija.Find(x => true).ToList();
            
            IList<string> Drzave = new List<string>();
            
            for (var i=0; i<SveDestinacije.Count(); i++)
            {
                if (Drzave.Count != 0)
                {
                    for (int j = 0; j < Drzave.Count(); j++)
                    {
                        if (Drzave[j] != SveDestinacije[i].Drzava)
                        {
                            Drzave.Add(SveDestinacije[i].Drzava);
                        }
                    }
                }
                else {
                    Drzave.Add(SveDestinacije[i].Drzava);
                }
            }
            SveDrzave = new SelectList(Drzave);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("AMTravelDb");
            kolekcija = db.GetCollection<Destinacija>("destinacija");
            SveDestinacije = await kolekcija.Find(x => true).ToListAsync();
            IList<string> Drzave = new List<string>();

            for (var i = 0; i < SveDestinacije.Count(); i++)
            {
                if (Drzave.Count != 0)
                {
                    for (int j = 0; j < Drzave.Count(); j++)
                    {
                        if (Drzave[j] != SveDestinacije[i].Drzava)
                        {
                            Drzave.Add(SveDestinacije[i].Drzava);
                        }
                    }
                }
                else
                {
                    Drzave.Add(SveDestinacije[i].Drzava);
                }
            }
            SveDrzave = new SelectList(Drzave);
            if(IzabranaDrzava!="Prikaži sve")  
            {  
            SveDestinacije = kolekcija.Find(x =>x.Drzava==IzabranaDrzava).ToList();
            }
            return Page();


        }
    }
}
