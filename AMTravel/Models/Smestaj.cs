using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMTravel.Models
{
    public class Smestaj
    {
        public ObjectId Id { get; set; }

        public string Naziv { get; set; }

        public string Adresa { get; set; }

        public string Opis { get; set; }

        public string Tip { get; set; }

        public MongoDBRef Destinacija { get; set; }

        public List<MongoDBRef> Sobe { get; set; }

        public Smestaj()
        {
            Sobe = new List<MongoDBRef>();
        }
    }
}
