using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace AMTravel.Models
{
    public class Korisnik
    {
        public ObjectId Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string Broj { get; set; }

        public string Email { get; set; }

        public string Tip { get; set; }
        
        public List<MongoDBRef> Rezervacije { get; set; }

        public Korisnik()
        {
            Rezervacije = new List<MongoDBRef>();
        }

    }
}