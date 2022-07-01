using System.ComponentModel;

namespace Frends.Community.MongoDB.Helpers
{
    public class DatabaseConnection
    {
        /// <summary>
        /// Connection string to the database.
        /// </summary>
        [PasswordPropertyText]
        public string ConnectionString { get; set; }

        /// <summary>
        /// The database to connect to
        /// </summary>
        [DisplayName("Database")]
        [DefaultValue("")]
        public string Database { get; set; }

        /// <summary>
        /// The name of the MongoDB collection to perform the operation on
        /// </summary>
        [DisplayName("Collection Name")]
        [DefaultValue("")]
        public string CollectionName { get; set; }
    }
}
