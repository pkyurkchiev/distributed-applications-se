using System;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;

namespace DC.Treminal
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run().Wait();
        }

        private async Task Run()
        {
            var firebase = new FirebaseClient("https://testproj-b0585.firebaseio.com/");

            var dinos = await firebase
              .Child("dinosaurs")
              .OrderByKey()
              .StartAt("apatosaurus")
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
