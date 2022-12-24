// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using Azure.Messaging.EventGrid;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.EventGrid;
using Microsoft.Extensions.Logging;

namespace FinFolio.SalesService.Functions
{
    public static class UserWishlistEventSubscriber
    {
        [FunctionName("UserWishlistEventSubscriber")]
        public static void Run([EventGridTrigger] EventGridEvent eventGridEvent, [CosmosDB(databaseName: "finfolio-sales-db", collectionName: "wishlist", ConnectionStringSetting = "SalesCosmosDBConnection")] out dynamic document, ILogger log)
        {
            document = new { id = eventGridEvent.Id, subject = eventGridEvent.Subject, wishlist = eventGridEvent.Data.ToString() };
            log.LogInformation(eventGridEvent.Data.ToString());
        }
    }
}
