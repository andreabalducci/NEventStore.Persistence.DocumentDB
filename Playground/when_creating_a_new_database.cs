using System.Linq;
using Machine.Specifications;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace Playground
{
    // ReSharper disable InconsistentNaming
    [Subject("Server")]
    public class with_new_server
    {
        private static DocumentClient client;
        private static Database _database;
        private static ResourceResponse<Database> response;

        private Establish context = () =>
        {
            client = new DocumentClient(DocumentDbSettings.EndPoint, DocumentDbSettings.AuthorizationKey);
            _database = new Database {Id = DocumentDbSettings.DatabaseId};
        };

        private It database_id_should_match = () => response.Resource.Id.ShouldBeLike(_database.Id);
        private Because of = () => { response = client.CreateDatabaseAsync(_database).Await(); };

        private Cleanup resources = () =>
        {
            Database database = client
                .CreateDatabaseQuery()
                .Where(db => db.Id == DocumentDbSettings.DatabaseId)
                .AsEnumerable().FirstOrDefault();

            if (database != null)
            {
                client.DeleteDatabaseAsync(database.SelfLink).Await();
            }

            client.Dispose();
        };
    }
}