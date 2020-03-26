using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHandsAnalyser
{
    public static class CardsComparisonHelper
    {
        /// <summary>
        /// Compares two poker hands of the same poker hand type;
        /// for example: 2 poker hands with hand type Four Of A Kind.
        /// </summary>
        /// <param name="pokerHand1">First poker hand to compare.</param>
        /// <param name="pokerHand2">Second poker hand to compare.</param>
        /// <param name="pokerHandType">Poker Hand Type of the 2 poker hands.</param>
        /// <returns>
        /// Int value indicating the winning hand. 
        /// 1: Hand 1 is the winning hand, 
        /// 0: The two hands are equal, 
        /// -1: Hand 2 is the winning hand.
        /// </returns>
        public static int CompareHandsOfSameType(Hand pokerHand1, Hand pokerHand2, PokerHandType pokerHandType)
        {
            //Arrange cards
            pokerHand1.Cards = pokerHand1.Cards.OrderBy(h => h.Value);
            pokerHand2.Cards = pokerHand2.Cards.OrderBy(h => h.Value);

            var result = 0;
            //Compare the hands with no sets
            switch (pokerHandType)
            {
                case PokerHandType.StraightFlush:
                case PokerHandType.Straight:
                case PokerHandType.Flush:
                case PokerHandType.HighCard:
                    return ComparePokerHands(pokerHand1.Cards, pokerHand2.Cards);
            }

            //Find sets of cards with same value: KK QQQ
            List<Card> hand1SameCardSet1, hand1SameCardSet2, hand2SameCardSet1, hand2SameCardSet2;
            FindSetsOfCardsWithSameValue(
            pokerHand1, out hand1SameCardSet1, out hand1SameCardSet2);

            FindSetsOfCardsWithSameValue(
            pokerHand2, out hand2SameCardSet1, out hand2SameCardSet2);

            //Comparing the hands with sets
            switch (pokerHandType)
            {
                case PokerHandType.FullHouse:
                case PokerHandType.ThreeOfAKind:
                case PokerHandType.FourOfAKind:
                case PokerHandType.Pair:
                    result = CompareCards(
                        hand1SameCardSet1.FirstOrDefault(), hand2SameCardSet1.FirstOrDefault());
                    break;
                case PokerHandType.TwoPair:
                    //Compare first pair
                    result = CompareCards(
                        hand1SameCardSet1.FirstOrDefault(), hand2SameCardSet1.FirstOrDefault());
                    if (result == 0)
                    {
                        //Compare second pair
                        result = CompareCards(
                        hand1SameCardSet2[0], hand2SameCardSet2[0]);
                    }
                    break;
            }
            if (result != 0)
                return result;

            //Compare kickers (side cards)
            var kicker1 = pokerHand1.Cards.Where(card =>
                !hand1SameCardSet1.Contains(card) &&
                !hand1SameCardSet2.Contains(card)).ToList();
            var kicker2 = pokerHand2.Cards.Where(card =>
                !hand2SameCardSet1.Contains(card) &&
                !hand2SameCardSet2.Contains(card)).ToList();

            return ComparePokerHands(kicker1, kicker2, kicker1.Count()-1);
        }

        private static int ComparePokerHands(IEnumerable<Card> cards1, IEnumerable<Card> cards2, int length = 4)
        {
            var result = 0;
            for (int i = length; i >= 0; i--)
            {
                result = CompareCards(cards1.ToArray()[i], cards2.ToArray()[i]);
                if (result != 0)
                    return result;
            }
            return result;
        }

        private static void FindSetsOfCardsWithSameValue(Hand pokerHand, out List<Card> sameValueSet1, out List<Card> sameValueSet2)
        {
            //Find sets of cards with the same value.
            int index = 0;
            sameValueSet1 = ExtractSets(pokerHand, ref index);
            sameValueSet2 = ExtractSets(pokerHand, ref index);
        }

        /// <summary>
        /// Extract sets from the poker hand. 
        /// E.g pair, three of kind or four of a kinf etc
        /// </summary>
        /// <param name="pokerHand_ArrangedCorrectly"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static List<Card> ExtractSets(Hand pokerHand_ArrangedCorrectly, ref int index)
        {
            List<Card> sameCardSet = new List<Card>();
            for (; index < 4; index++)
            {
                Card currentCard = pokerHand_ArrangedCorrectly.Cards.ToArray()[index];
                Card nextCard = pokerHand_ArrangedCorrectly.Cards.ToArray()[index + 1];
                if (currentCard.Value == nextCard.Value)
                {
                    if (sameCardSet.Count == 0)
                        sameCardSet.Add(currentCard);
                    sameCardSet.Add(nextCard);
                }
                else if (sameCardSet.Count > 0)
                {
                    index++;
                    break;
                }
            }
            return sameCardSet;
        }

        /// <summary>
        /// This function compares poker cards,
        /// and returns an int value indicating the winning hand.
        /// </summary>
        /// <param name="pokerHand1_card">Poker hand 1's card.</param>
        /// <param name="pokerHand2_card">Poker hand 2's card.</param>
        /// <returns>Int value indicating the winning hand. 
        /// 1: Hand 1 is the winning hand, 
        /// 0: The two hands are equal, 
        /// -1: Hand 2 is the winning hand.</returns>
        private static int CompareCards(Card pokerHand1_card, Card pokerHand2_card)
        {
            //Get card int values.
            int pokerHand1_cardIntValue = (int)pokerHand1_card.Value;
            int pokerHand2_cardIntValue = (int)pokerHand2_card.Value;

            //Compare and return result.
            return pokerHand1_cardIntValue > pokerHand2_cardIntValue ? 1 :
                pokerHand1_cardIntValue == pokerHand2_cardIntValue ? 0 : -1;
        }
    }
}
