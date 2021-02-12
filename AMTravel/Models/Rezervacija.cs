using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMTravel.Models
{
    public class Rezervacija
    {
        public ObjectId Id { get; set; }

        public MongoDBRef Korisnik { get; set; }
        
        public MongoDBRef Soba { get; set; }

        public DateTime DatumOd { get; set; }

        public DateTime DatumDo { get; set; }
    }
}
