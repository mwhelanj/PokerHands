using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHandsAnalyser
{
    public class Hand
    {
        public IEnumerable<Card> Cards { get; set; }

		/// <summary>
		/// Determines the poker hand type. For example: Straight Flush or Four of a Kind.
		/// </summary>
		/// <param name="Cards">The poker hand to be evaluated.</param>
		/// <returns>The poker hand type.
		/// For example: Straight Flush or Four of a Kind.</returns>
		public PokerHandType DeterminePokerHandType()
		{
			//Determine Poker Hand Type
			if (IsRoyalFlush)
				return PokerHandType.RoyalFlush;

			if (IsFlush && IsStraight)
				return PokerHandType.StraightFlush;

			if (IsFlush)
				return PokerHandType.Flush;

			if (IsStraight)
				return PokerHandType.Straight;

			if (IsFourOfAKind)
				return PokerHandType.FourOfAKind;
			
			if (IsFullHouse)
				return PokerHandType.FullHouse;

			if (IsThreeOfAKind)
				return PokerHandType.ThreeOfAKind;

			if (IsTwoPair)
				return PokerHandType.TwoPair;

			if (IsPair)
				return PokerHandType.Pair;

			return PokerHandType.HighCard;
		}

		public bool Contains(Value val)
		{
			return Cards.Where(c => c.Value == val).Any();
		}

		public bool IsPair
		{
			get
			{
				return Cards.GroupBy(h => h.Value)
						   .Where(g => g.Count() == 2)
						   .Count() == 1;
			}
		}

		public bool IsTwoPair
		{
			get
			{
				return Cards.GroupBy(h => h.Value)
							.Where(g => g.Count() == 2)
							.Count() == 2;
			}
		}

		public bool IsThreeOfAKind
		{
			get
			{
				var val = Cards.GroupBy(h => h.Value)
							.Where(g => g.Count() == 3);
							

				return val.Any(); 
			}
		}

		public bool IsFourOfAKind
		{
			get
			{
				return Cards.GroupBy(h => h.Value)
							.Where(g => g.Count() == 4)
							.Any();
			}
		}

		//Check whether all cards are in the same suit
		public bool IsFlush
		{
			get
			{
				return Cards.GroupBy(h => h.Suit).Count() == 1;
			}
		}

		public bool IsFullHouse
		{
			get
			{
				return IsPair && IsThreeOfAKind;
			}
		}

		public bool IsStraight
		{
			get
			{
				var ordered = Cards.OrderBy(h => h.Value).ToArray();
				var straightStart = (int)ordered.First().Value;
				for (var i = 1; i < ordered.Length; i++)
				{
					if ((int)ordered[i].Value != straightStart + i)
						return false;
				}
				return true;
			}

		}

		public bool IsRoyalFlush
		{
			get
			{
				return IsStraight && IsFlush;
			}
		}
	}
}
