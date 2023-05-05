using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb3_databaser.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Labb3_databaser.Repositories
    {
    public class MovieRepository 
        {
        private readonly IMongoCollection<Movie> _movieCollection;

        public MovieRepository()

            {

                var databaseName = "movie_app";
                var settings = MongoClientSettings.FromConnectionString("mongodb://User:hej123@ac-fwvkpe0-shard-00-00.5sgvna2.mongodb.net:27017,ac-fwvkpe0-shard-00-01.5sgvna2.mongodb.net:27017,ac-fwvkpe0-shard-00-02.5sgvna2.mongodb.net:27017/?ssl=true&replicaSet=atlas-7cwdpu-shard-0&authSource=admin&retryWrites=true&w=majority");

            var client = new MongoClient(settings);
            var database = client.GetDatabase(databaseName);

            _movieCollection = database.GetCollection<Movie>("movies", new MongoCollectionSettings() { AssignIdOnInsert = true });
            }

        public void Add(Movie movieObject)
            {
            _movieCollection.InsertOne(movieObject);
            }

        public IEnumerable<Movie> GetAllMovies()
            {
            return _movieCollection.Find(movie => true).ToEnumerable();
            }


        public void Update(object id, Movie movie)
        {
            var filter = Builders<Movie>.Filter.Eq("Id", id);

            var update = Builders<Movie>
                .Update
                .Set("Title", movie.Title)
                .Set("Director", movie.Director)
                .Set("Plot", movie.Plot)
                .Set("Genre", movie.Genre);

            _movieCollection
                .FindOneAndUpdate(
                    filter,
                    update, new FindOneAndUpdateOptions<Movie, Movie>
                        {
                        IsUpsert = true
                        });
            }
        
        public void Delete(object id)
        {
            var filter = Builders<Movie>.Filter.Eq("Id", id);
            _movieCollection.DeleteOne(filter);
        }


        //public void GetSelectedMovie(object id)
        //{
        //   var filter = Builders<Movie>.Filter.Eq("id", id);

        //   _movieCollection.Find(m => m.Id == ObjectId.Parse((string)id)).FirstOrDefault();

        //}

        }
    }
