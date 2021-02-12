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
    public class SobaDodajModel : PageModel
    {
       
		[BindProperty]
        public Soba NovaSoba{get;set;}
        public ObjectId NoviId;

        [BindProperty]
        public int? PostojiVec {get; set;}
        private IMongoCollection<Soba> kolekcija;
        	[BindProperty]
       public Smestaj PostojiSmestaj{get;set;}

        private IMongoCollection<Smestaj> kolekcijaS;
     

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
                kolekcija = db.GetCollection<Soba>("soba");
                NoviId = ObjectId.Parse(id);
                Soba PostojiSoba= kolekcija.Find(x => x.BrojSobe == NovaSoba.BrojSobe && x.Smestaj.Id==NoviId).FirstOrDefault();
                kolekcijaS = db.GetCollection<Smestaj>("smestaj");
                PostojiSmestaj = kolekcijaS.Find(x => x.Id == NoviId).FirstOrDefault();

                if (PostojiSoba != null)
                {
                    PostojiVec = 1;
                    return this.Page();
                }

                kolekcija.InsertOne(NovaSoba);

                //  NovaSoba.Smestaj.Add(new MongoDBRef("smestaj", PostojiSmestaj.Id));
                PostojiSmestaj.Sobe.Add(new MongoDBRef("soba", NovaSoba.Id));
                NovaSoba.Smestaj = new MongoDBRef("smestaj", NoviId);

               
                kolekcija.ReplaceOne(x=>x.Id==NovaSoba.Id, NovaSoba);
                kolekcijaS.ReplaceOne(x=>x.Id==PostojiSmestaj.Id, PostojiSmestaj);
                return RedirectToPage("./SmestajSve");
            }
        }
    }
}
