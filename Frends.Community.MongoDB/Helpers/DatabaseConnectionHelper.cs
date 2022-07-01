using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;

namespace Frends.Community.MongoDB.Helpers
{
    public class DatabaseConnectionHelper
    {
        /// <summary>
        /// Creates a connection to the MongoDB database
        /// </summary>
        /// <param name="connectionString">Connection string to the database.</param>
        /// <param name="database">Database name</param>
        /// <returns>A IMongoDatabase instance with the database connection</returns>
        public IMongoDatabase GetMongoDatabase(string connectionString, string database)
        {
            MongoClient mongoClient = new MongoClient(connectionString);
            var dataBase = mongoClient.GetDatabase(database);
            return dataBase;
        }

        /// <summary>
        /// Creates a connection to the MongoDB database and returns a connection to a MongoDB collection
        /// </summary>
        /// <param name="connectionString">Connection string to the database.</param>
        /// <param name="database">database name</param>
        /// <param name="collectionName">collection name</param>
        /// <returns>A IMongoCollection instance with the connection to the collection</returns>
        public IMongoCollection<BsonDocument> GetMongoCollection(string connectionString, string database, string collectionName)
        {
            var dataBase = GetMongoDatabase(connectionString, database);
            var collection = dataBase.GetCollection<BsonDocument>(collectionName);

            return collection;
        }

        /// <summary>
        /// Returns a GridFS bucket with an open connection to Mongo
        /// </summary>
        /// <param name="connectionString">Connection string to the database.</param>
        /// <param name="database">database name</param>
        /// <param name="bucketName">the name of the bucket (collection) to operate on</param>
        /// <returns>a GridFSBucket with an active connection</returns>
        public GridFSBucket GetGridFSBucket(string connectionString, string database, string bucketName)
        {
            var mongoDatabase = GetMongoDatabase(connectionString, database);

            var bucket = new GridFSBucket(mongoDatabase, new GridFSBucketOptions
            {
                BucketName = bucketName,
                ChunkSizeBytes = 15 * 1024 * 1024, // Set one chunk to 15 MB
                WriteConcern = WriteConcern.WMajority,
                ReadPreference = ReadPreference.Secondary
            });

            return bucket;
        }
    }
}
