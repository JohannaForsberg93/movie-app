using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Labb3_databaser.Models
    {
    public class Movie
        {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement]
        public string Title { get; set; } = string.Empty;

        [BsonElement]
        public string Director { get; set; } = string.Empty;

        [BsonElement]
        public string Plot { get; set; } = string.Empty;

        [BsonElement]
        public string[] Genre { get; set; }

        }
    }
