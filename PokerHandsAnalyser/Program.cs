using System;
using System.Collections.Generic;
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

            using (StreamReader reader = new StreamReader(resource))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    var game = line.Split(" ").ToList();
                    var hand1 = game.Take(5);
                    var hand2 = game.Skip(5);
                    Console.WriteLine("\t" + "Player 1 hand: " + string.Join("", hand1) + " Player 2 hand: " + string.Join("", hand2));
                }
            }

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }
    }
}
