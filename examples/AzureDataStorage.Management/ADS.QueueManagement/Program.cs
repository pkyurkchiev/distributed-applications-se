using Microsoft.Azure;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using System;
using System.Threading.Tasks;

namespace ADS.QueueManagement
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create the queue name -- use a guid in the name so it's unique.
            string queueName = "demotest-" + System.Guid.NewGuid().ToString();

            // Create or reference an existing queue.
            CloudQueue queue = CreateQueueAsync(queueName).Result;

            // Demonstrate basic queue functionality.  
            await BasicQueueOperationsAsync(queue);

            // Demonstrate how to update an enqueued message 
            await UpdateEnqueuedMessageAsync(queue);

            // to delete the queue uncomment the line of code below.  
            await DeleteQueueAsync(queue);
        }

        public static async Task<CloudQueue> CreateQueueAsync(string queueName)
        {
            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("UseDevelopmentStorage=true");

            // Create a queue client for interacting with the queue service
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            Console.WriteLine("1. Create a queue for the demo");

            CloudQueue queue = queueClient.GetQueueReference(queueName);
            try
            {
                await queue.CreateIfNotExistsAsync();
            }
            catch
            {
                Console.WriteLine("If you are running with the default configuration please make sure you have started the storage emulator.  ess the Windows key and type Azure Storage to select and run it from the list of applications - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return queue;
        }

        public static async Task BasicQueueOperationsAsync(CloudQueue queue)
        {
            // Insert a message into the queue using the AddMessage method. 
            Console.WriteLine("2. Insert a single message into a queue");
            await queue.AddMessageAsync(new CloudQueueMessage("Hello World!"));

            // Peek at the message in the front of a queue without removing it from the queue using PeekMessage (PeekMessages lets you peek >1 message)
            Console.WriteLine("3. Peek at the next message");
            CloudQueueMessage peekedMessage = await queue.PeekMessageAsync();
            if (peekedMessage != null)
            {
                Console.WriteLine("The peeked message is: {0}", peekedMessage.AsString);
            }

            // You de-queue a message in two steps. Call GetMessage at which point the message becomes invisible to any other code reading messages 
            // from this queue for a default period of 30 seconds. To finish removing the message from the queue, you call DeleteMessage. 
            // This two-step process ensures that if your code fails to process a message due to hardware or software failure, another instance 
            // of your code can get the same message and try again. 
            Console.WriteLine("4. De-queue the next message");
            CloudQueueMessage message = await queue.GetMessageAsync();
            if (message != null)
            {
                Console.WriteLine("Processing & deleting message with content: {0}", message.AsString);
                await queue.DeleteMessageAsync(message);
            }
        }

        public static async Task UpdateEnqueuedMessageAsync(CloudQueue queue)
        {
            // Insert another test message into the queue 
            Console.WriteLine("5. Insert another test message ");
            await queue.AddMessageAsync(new CloudQueueMessage("Hello World Again!"));

            Console.WriteLine("6. Change the contents of a queued message");
            CloudQueueMessage message = await queue.GetMessageAsync();
            message.SetMessageContent2("Updated contents.", false);
            await queue.UpdateMessageAsync(
                message,
                TimeSpan.Zero,  // For the purpose of the sample make the update visible immediately
                MessageUpdateFields.Content |
                MessageUpdateFields.Visibility);
        }

        public static async Task DeleteQueueAsync(CloudQueue queue)
        {
            Console.WriteLine("7. Delete the queue");
            await queue.DeleteAsync();
        }
    }
}
