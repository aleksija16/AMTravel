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
    public class PrijavaModel : PageModel
    {
        [BindProperty]
        public Korisnik TrenutniKorisnik { get; set; }
        public Korisnik PostojiKorisnik { get; set; }
        public int Session { get; set; }
         private IMongoCollection<Korisnik> kolekcija;
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
                kolekcija = db.GetCollection<Korisnik>("korisnik");
                Korisnik PostojiKorisnik = kolekcija.Find(x => x.Username == TrenutniKorisnik.Username).FirstOrDefault();


                if (PostojiKorisnik != null && PostojiKorisnik.Password == TrenutniKorisnik.Password)
                {
            
                    SessionClass.SessionId=PostojiKorisnik.Id.ToString();
                    SessionClass.TipKorisnika = PostojiKorisnik.Tip;
                    if (PostojiKorisnik.Tip == "K")
                    {
                       
                        SessionClass.ImeKorisnika = PostojiKorisnik.Ime + " " + PostojiKorisnik.Prezime;
                    }
                    else if (PostojiKorisnik.Tip == "A")
                    {
                        SessionClass.ImeKorisnika = "Administrator";
                    }
                    return RedirectToPage("./Index");
                }
                else
                {
                    Session = -1;
                    return Page();
                }

            }
        }
    }
}
