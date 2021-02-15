using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AMTravel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace AMTravel.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
         [BindProperty]
        public IList<Destinacija> SveDestinacije{get;set;}
        private IMongoCollection<Destinacija> kolekcija;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017");
            IMongoDatabase db = client.GetDatabase("AMTravelDb");
            kolekcija = db.GetCollection<Destinacija>("destinacija");
            SveDestinacije = kolekcija.Find(x => true).ToList();
            
        }
    }
}
