using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Labb3_databaser.Models
    {
    public class Rating
        {

            [BsonId] public ObjectId Id { get; set; }

                [BsonElement]
                public int Rate { get; set; }

        }
    }
