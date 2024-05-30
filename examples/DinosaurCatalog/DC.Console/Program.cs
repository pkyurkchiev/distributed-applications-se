using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Threading.Tasks;

namespace DC.Treminal
{
    class Program
    {
        static void Main(string[] args)
        {
            ArgumentNullException.ThrowIfNull(args);

            Run().Wait();
        }

        private static async Task Run()
        {
            string myproject = "da-test-a98d2";
            FirebaseClient firebase = new($"https://{myproject}.firebaseio.com/");

            var dinos = await firebase
              .Child("dinosaurs")
              .OrderByKey()
              .StartAt("Apatosaurus")
              .LimitToFirst(2)
              .OnceAsync<Dinosaur>();

            foreach (var dino in dinos)
            {
                Console.WriteLine($"{dino.Key} is {dino.Object.Height}m high.");
            }

            Console.ReadKey();
        }
    }
}
