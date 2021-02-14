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
    public class SmestajDodajModel : PageModel
    {
       
		[BindProperty]
        public Smestaj NoviSmestaj{get;set;}
        public ObjectId NoviId;

        [BindProperty]
        public int? PostojiVec {get; set;}
        private IMongoCollection<Smestaj> kolekcija;
        	[BindProperty]
       public Destinacija PostojiDestinacija{get;set;}

        private IMongoCollection<Destinacija> kolekcijaS;
     

        public IActionResult OnPost(string id)
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
                NoviId = ObjectId.Parse(id);
                Smestaj PostojiSmestaj= kolekcija.Find(x => x.Naziv == NoviSmestaj.Naziv && x.Destinacija.Id==NoviId).FirstOrDefault();
                kolekcijaS = db.GetCollection<Destinacija>("destinacija");
                PostojiDestinacija = kolekcijaS.Find(x => x.Id == NoviId).FirstOrDefault();

                if (PostojiSmestaj != null)
                {
                    PostojiVec = 1;
                    return this.Page();
                }

                kolekcija.InsertOne(NoviSmestaj);

                PostojiDestinacija.Smestaji.Add(new MongoDBRef("smestaj", NoviSmestaj.Id));
                NoviSmestaj.Destinacija = new MongoDBRef("destinacija", NoviId);

               
                kolekcija.ReplaceOne(x=>x.Id==NoviSmestaj.Id, NoviSmestaj);
                kolekcijaS.ReplaceOne(x=>x.Id==PostojiDestinacija.Id, PostojiDestinacija);
                return RedirectToPage("./DestinacijaSve");
            }
        }
    }
}
