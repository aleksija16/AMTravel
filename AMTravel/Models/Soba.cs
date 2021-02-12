using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMTravel.Models
{
    public class Soba
    {
        public ObjectId Id { get; set; }

        public string BrojSobe { get; set; }

        public string Opis { get; set; }

        public string Cena { get; set; }

        public int BrojOsoba { get; set; }

        public MongoDBRef Smestaj { get; set; }

        public List<MongoDBRef> Rezervacije { get; set; }

        public Soba()
        {
            Rezervacije = new List<MongoDBRef>();
        }
    }
}
