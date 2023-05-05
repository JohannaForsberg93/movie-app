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
    public class Comment
        {
        [BsonId] 
        public ObjectId Id { get; set; }

        [BsonElement]
        public string MovieId { get; set; }

        [BsonElement]
        public string MovieComment { get; set; } = string.Empty;
        }
    }
