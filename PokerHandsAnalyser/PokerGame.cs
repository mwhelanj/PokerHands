using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHandsAnalyser
{
    public class PokerGame
    {
        public PokerGame()
        {
            PlayerOne = new Player();
            PlayerTwo = new Player();
        }

        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }

        public void Play(List<string> cardsDelt)
        {
            dealCards(cardsDelt);
            var card = PlayerOne.Hand.Cards.First();
            var card2 = PlayerOne.Hand.Cards.Skip(1).First();
            var Player1HandRank = (int)PlayerOne.Hand.DeterminePokerHandType();
            var Player2HandRank = (int)PlayerTwo.Hand.DeterminePokerHandType();

            if (Player1HandRank> Player2HandRank)
            {
                PlayerOne.Score++;
            }
            else if(Player2HandRank > Player1HandRank){
                PlayerTwo.Score++;
            }
            else if (Player1HandRank == Player2HandRank && Player1HandRank !=9)
            {
                var results = CardsComparisonHelper.CompareHandsOfSameType(PlayerOne.Hand, PlayerTwo.Hand, PlayerOne.Hand.DeterminePokerHandType());
                if (results == 1)
                {
                    PlayerOne.Score++;
                }
                else if (results == -1)
                {
                    PlayerTwo.Score++;
                }
                else
                {
                    Console.WriteLine("Draw");
                }
            }
            else {
                Console.WriteLine("Draw");
            }
        }

        private void dealCards(List<string> cardsDelt)
        {
            // Player one set up 
            var hand1 = cardsDelt.Take(5);
            var handPlayer1 = convertStringToHand(hand1);
            PlayerOne.Hand =handPlayer1;

            // Player two set up 
            var hand2 = cardsDelt.Skip(5);
            var handPlayer2 = convertStringToHand(hand2);
            PlayerTwo.Hand =handPlayer2;
        }

        /// <summary>
        /// Convert the string representation from the text file to an object 
        /// </summary>
        /// <param name="handArray"></param>
        /// <returns>Hand which contains a collection of cards</returns>
        private Hand convertStringToHand(IEnumerable<string> handArray)
        {
            var hand = new Hand();
            try
            {
                var cards = new List<Card>();
                //add each card to hand 
                foreach (var cardstring in handArray) {
                    Value value = (Value)Enum.Parse(typeof(Value), cardstring[0].ToString(), true);
                    Suit suit = (Suit)Enum.Parse(typeof(Suit), cardstring[1].ToString(), true);
                    cards.Add(new Card() { Value = value, Suit = suit }); 
                }
                hand.Cards = cards;
            }
            catch (Exception e)
            {
                Console.WriteLine("Wrong Value entered in file: " + e);
            }
            return hand;
        }
    }
}
