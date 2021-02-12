using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMTravel.Models
{
    public class Destinacija
    {
        public ObjectId Id { get; set; }

        public string Naziv { get; set; }

        public string Drzava { get; set; }

        public string Opis { get; set; }
        public string Slika { get; set; }

        public List<MongoDBRef> Smestaji { get; set; }

        public Destinacija()
        {
            Smestaji = new List<MongoDBRef>();
        }


    }
}
