﻿using Frends.Community.MongoDB.Helpers;
using MongoDB.Bson;
using MongoDB.Driver;
using System.ComponentModel;

namespace Frends.Community.MongoDB
{
    public class Update
    {
        public class UpdateParameters
        {
            /// <summary>
            /// The database connection
            /// </summary>
            [DisplayName("MongoDB Database Connection")]
            public DatabaseConnection DbConnection { get; set; }

            /// <summary>
            /// The filter to use for the search, in JSON. Note that ObjectId is not strictly valid json, but works in MongoDB.
            /// </summary>
            [DisplayName("Filter")]
            [DefaultValue("{ \"_id\": ObjectId(\"...\") }")]
            public string FilterString { get; set; }

            /// <summary>
            /// The values to update in the document, as JSON
            /// </summary>
            [DisplayName("Filter")]
            [DefaultValue("{ $set: { \"bar\": \"updated\"} }")]
            public string UpdateString { get; set; }
        }

        /// <summary>
        /// Updates all files in MongoDB that match the search criteria
        /// </summary>
        /// <param name="parameters">The parameters</param>
        /// <returns>The count of updated documents</returns>
        public static long UpdateDocuments(UpdateParameters parameters)
        {
            var helper = new DatabaseConnectionHelper();
            var collection = helper.GetMongoCollection(parameters.DbConnection.ConnectionString,
                                                parameters.DbConnection.Database,
                                                parameters.DbConnection.CollectionName);

            // Initialize the filter
            var filter = parameters.FilterString;

            UpdateDefinition<BsonDocument> update = parameters.UpdateString;

            return collection.UpdateMany(filter, update).ModifiedCount;
        }
    }
}
