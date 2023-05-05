using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb3_databaser.Models;
using MongoDB.Driver;

namespace Labb3_databaser.Repositories
    {
    public class RatingRepository
        {
            private readonly IMongoCollection<Rating> _ratingCollection;
            public RatingRepository()
            {
                var databaseName = "movie_app";
                var settings = MongoClientSettings.FromConnectionString("mongodb+srv://johanna:hej123@moviecluster.2ebstih.mongodb.net/?retryWrites=true&w=majority");
                var client = new MongoClient(settings);
                var database = client.GetDatabase(databaseName);

                _ratingCollection = database.GetCollection<Rating>("rating", new MongoCollectionSettings() { AssignIdOnInsert = true });
            }

            //public IEnumerable<Movie> GetRating(object id)
            //{
            //    return _ratingCollection.Find(id).ToEnumerable();
            //}

        }
    }
    
