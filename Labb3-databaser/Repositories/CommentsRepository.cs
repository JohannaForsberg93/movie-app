using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Labb3_databaser.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Labb3_databaser.Repositories
    {
    public class CommentsRepository
    {
        private readonly IMongoCollection<Comment> _commentCollection;
            public CommentsRepository()
            {
                var databaseName = "movie_app";
            var settings = MongoClientSettings.FromConnectionString("mongodb://User:hej123@ac-fwvkpe0-shard-00-00.5sgvna2.mongodb.net:27017,ac-fwvkpe0-shard-00-01.5sgvna2.mongodb.net:27017,ac-fwvkpe0-shard-00-02.5sgvna2.mongodb.net:27017/?ssl=true&replicaSet=atlas-7cwdpu-shard-0&authSource=admin&retryWrites=true&w=majority");

            var client = new MongoClient(settings);
            var database = client.GetDatabase(databaseName);
                _commentCollection = database.GetCollection<Comment>("comments", new MongoCollectionSettings() { AssignIdOnInsert = true });
            }

            public IEnumerable<Comment> GetComments(string id)
            {

            return _commentCollection.Find(c => c.MovieId.Equals(id)).ToEnumerable();
            }

            public void AddComment(Comment comment)
            {
                _commentCollection.InsertOne(comment);

            }

            public void UpdateComment(object id, Comment comment)
            {
                var filter = Builders<Comment>.Filter.Eq("MovieId", id);

                var update = Builders<Comment>
                    .Update
                    .Set("MovieComment", comment.MovieComment);


                _commentCollection
                    .FindOneAndUpdate(
                        filter,
                        update, new FindOneAndUpdateOptions<Comment, Comment>
                        {
                            IsUpsert = true
                        });
            }

            public void DeleteComment(object id)
            {
                var filter = Builders<Comment>.Filter.Eq("MovieId", id);
                _commentCollection.FindOneAndDelete(filter);
            }

        }
    }
