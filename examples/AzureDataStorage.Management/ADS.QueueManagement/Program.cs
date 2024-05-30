using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using System;

namespace ADS.QueueManagement
{
    class Program
    {
        private const string connectionString = "UseDevelopmentStorage=true";
        static void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            // Create the queue name -- use a guid in the name so it's unique.
            string queueName = $"demotest-{Guid.NewGuid()}";

            // Create or reference an existing queue.
            CreateQueue(queueName);

            // Demonstrate basic queue functionality.  
            InsertMessage(queueName, "Hello");
            PeekMessage(queueName);
            DequeueMessage(queueName);
            GetQueueLength(queueName);

            // Delete the queue uncomment the line of code below.  
            DeleteQueue(queueName);
        }

        //-------------------------------------------------
        // Create a message queue
        //-------------------------------------------------
        public static bool CreateQueue(string queueName)
        {
            try
            {
                // Instantiate a QueueClient which will be used to create and manipulate the queue
                QueueClient queueClient = new(connectionString, queueName);

                // Create the queue
                queueClient.CreateIfNotExists();

                if (queueClient.Exists())
                {
                    Console.WriteLine($"Queue created: '{queueClient.Name}'");
                    return true;
                }
                else
                {
                    Console.WriteLine($"Make sure the Azurite storage emulator running and try again.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}\n\n");
                Console.WriteLine($"Make sure the Azurite storage emulator running and try again.");
                return false;
            }
        }

        //-------------------------------------------------
        // Insert a message into a queue
        //-------------------------------------------------
        public static void InsertMessage(string queueName, string message)
        {
            // Instantiate a QueueClient which will be used to create and manipulate the queue
            QueueClient queueClient = new(connectionString, queueName);

            // Create the queue if it doesn't already exist
            queueClient.CreateIfNotExists();

            if (queueClient.Exists())
            {
                // Send a message to the queue
                queueClient.SendMessage(message);
            }

            Console.WriteLine($"Inserted: {message}");
        }

        //-------------------------------------------------
        // Peek at a message in the queue
        //-------------------------------------------------
        public static void PeekMessage(string queueName)
        {
            // Instantiate a QueueClient which will be used to manipulate the queue
            QueueClient queueClient = new(connectionString, queueName);

            if (queueClient.Exists())
            {
                // Peek at the next message
                PeekedMessage[] peekedMessage = queueClient.PeekMessages();

                // Display the message
                Console.WriteLine($"Peeked message: '{peekedMessage[0].MessageText}'");
            }
        }

        //-------------------------------------------------
        // Process and remove a message from the queue
        //-------------------------------------------------
        public static void DequeueMessage(string queueName)
        {
            // Instantiate a QueueClient which will be used to manipulate the queue
            QueueClient queueClient = new(connectionString, queueName);

            if (queueClient.Exists())
            {
                // Get the next message
                QueueMessage[] retrievedMessage = queueClient.ReceiveMessages();

                // Process (i.e. print) the message in less than 30 seconds
                Console.WriteLine($"Dequeued message: '{retrievedMessage[0].MessageText}'");

                // Delete the message
                queueClient.DeleteMessage(retrievedMessage[0].MessageId, retrievedMessage[0].PopReceipt);
            }
        }

        //-----------------------------------------------------
        // Get the approximate number of messages in the queue
        //-----------------------------------------------------
        public static void GetQueueLength(string queueName)
        {
            // Instantiate a QueueClient which will be used to manipulate the queue
            QueueClient queueClient = new(connectionString, queueName);

            if (queueClient.Exists())
            {
                QueueProperties properties = queueClient.GetProperties();

                // Retrieve the cached approximate message count.
                int cachedMessagesCount = properties.ApproximateMessagesCount;

                // Display number of messages.
                Console.WriteLine($"Number of messages in queue: {cachedMessagesCount}");
            }
        }

        //-------------------------------------------------
        // Delete the queue
        //-------------------------------------------------
        public static void DeleteQueue(string queueName)
        {
            // Instantiate a QueueClient which will be used to manipulate the queue
            QueueClient queueClient = new(connectionString, queueName);

            if (queueClient.Exists())
            {
                // Delete the queue
                queueClient.Delete();
            }

            Console.WriteLine($"Queue deleted: '{queueClient.Name}'");
        }
    }
}
