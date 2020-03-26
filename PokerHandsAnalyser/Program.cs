using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PokerHandsAnalyser
{
    public class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Poker game");

            // read embedded resource -poker-hands.txt
            var assembly = typeof(Program).GetTypeInfo().Assembly;
            Stream resource = assembly.GetManifestResourceStream("PokerHandsAnalyser.InputFiles.poker-hands.txt");
            var game = new PokerGame(); // start game

            using (StreamReader reader = new StreamReader(resource))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var cardDelt = line.Split(" ").ToList();
                    game.Play(cardDelt); // deal out cards
                }
            }
            // Print final results
            Console.WriteLine("\t" + "Player one score: " + game.PlayerOne.Score);
            Console.WriteLine("\t" + "Player two score: " + game.PlayerTwo.Score);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }
    }
}
