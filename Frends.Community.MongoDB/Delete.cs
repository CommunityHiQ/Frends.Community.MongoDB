﻿using Frends.Community.MongoDB.Helpers;
using System.ComponentModel;

namespace Frends.Community.MongoDB
{
    public class Delete
    {
        public class DeleteParameters
        {
            /// <summary>
            /// The database connection
            /// </summary>
            [DisplayName("MongoDB Database Connection")]
            public DatabaseConnection DbConnection { get; set; }

            /// <summary>
            /// The filter to use, in JSON. Note that ObjectId is not strictly valid json, but works in MongoDB.
            /// </summary>
            [DisplayName("Filter")]
            [DefaultValue("{ \"_id\": ObjectId(\"...\") }")]
            public string FilterString { get; set; }
        }

        /// <summary>
        /// Deletes documents that match the filtering criteria
        /// </summary>
        /// <param name="parameters">The parameters</param>
        /// <returns>A long value with the amount of deleted documents</returns>
        public static long DeleteDocuments(DeleteParameters parameters)
        {
            var helper = new DatabaseConnectionHelper();
            var collection = helper.GetMongoCollection(parameters.DbConnection.ConnectionString,
                                                parameters.DbConnection.Database,
                                                parameters.DbConnection.CollectionName);

            // Initialize the filter
            var filter = parameters.FilterString;
            return collection.DeleteMany(filter).DeletedCount;
        }
    }
}
