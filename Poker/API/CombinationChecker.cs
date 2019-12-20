using Poker.API.Cards;
using Poker.API.Cards.Enums;
using Poker.API.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.API
{
    public static class CombinationChecker
    {
        public static CardCombinations CheckCombinations(List<Card> cards, List<Card> playerCards, out List<Card> combinationCards)
        {
            var combination = CardCombinations.HighCard;
            combinationCards = null;
            cards.AddRange(playerCards);

            if      (IsRoyalFlush(cards, out combinationCards)    == true) combination = CardCombinations.RoyalFlush;
            else if (IsStraightFlush(cards, out combinationCards) == true) combination = CardCombinations.StraightFlush;
            else if (IsQuads(cards, out combinationCards)         == true) combination = CardCombinations.Quads;
            else if (IsFullHouse(cards, out combinationCards)     == true) combination = CardCombinations.FullHouse;
            else if (IsFlush(cards, out combinationCards)         == true) combination = CardCombinations.Flush;
            else if (IsStraight(cards, out combinationCards)      == true) combination = CardCombinations.Straight;
            else if (IsSet(cards, out combinationCards)           == true) combination = CardCombinations.Set;
            else if (IsTwoPair(cards, out combinationCards)       == true) combination = CardCombinations.TwoPair;
            else if (IsOnePair(cards, out combinationCards)       == true) combination = CardCombinations.OnePair;
            else
            {
                combinationCards = new List<Card>();
                combinationCards.Add(GetHighCard(playerCards));
                combination = CardCombinations.HighCard;
            }

            return combination;
        }

        private static bool IsRoyalFlush(List<Card> cards, out List<Card> combinationCards)
        {
            var result = false;
            combinationCards = null;

            for (int i = 0; i <= 3; i++)
            {
                var lear = (CardLear)i;
                if (cards.Any((item) => item.Type == CardType.Ten && item.Lear == lear) &&
                    cards.Any((item) => item.Type == CardType.Jack && item.Lear == lear) &&
                    cards.Any((item) => item.Type == CardType.Queen && item.Lear == lear) &&
                    cards.Any((item) => item.Type == CardType.King && item.Lear == lear) &&
                    cards.Any((item) => item.Type == CardType.Ace && item.Lear == lear))
                {
                    result = true;
                    combinationCards = new List<Card>();
                    combinationCards.Add(new Card(CardType.Ten, lear));
                    combinationCards.Add(new Card(CardType.Jack, lear));
                    combinationCards.Add(new Card(CardType.Queen, lear));
                    combinationCards.Add(new Card(CardType.King, lear));
                    combinationCards.Add(new Card(CardType.Ace, lear));
                    break;
                }
                else
                {
                    continue;
                }
            }

            return result;
        }

        private static bool IsStraightFlush(List<Card> cards, out List<Card> combinationCards)
        {
            var result = false;
            combinationCards = null;

            for (int i = 0; i <= 3; i++)
            {
                var lear = (CardLear)i;
                for (int j = 0; j <= 7; j++)
                {
                    var type = (CardType)j;
                    if (cards.Any((item) => item.Type == type + 0 && item.Lear == lear) &&
                        cards.Any((item) => item.Type == type + 1 && item.Lear == lear) &&
                        cards.Any((item) => item.Type == type + 2 && item.Lear == lear) &&
                        cards.Any((item) => item.Type == type + 3 && item.Lear == lear) &&
                        cards.Any((item) => item.Type == type + 4 && item.Lear == lear))
                    {
                        result = true;
                        combinationCards = new List<Card>();
                        for (int k = 0; k <= 4; k++)
                        {
                            combinationCards.Add(new Card(type + k, lear));
                        }
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            return result;
        }

        private static bool IsQuads(List<Card> cards, out List<Card> combinationCards)
        {
            var result = false;
            combinationCards = null;

            for (int i = 0; i <= (int)CardType.Ace; i++)
            {
                var type = (CardType)i;
                if (cards.Any((item) => item.Type == type && item.Lear == CardLear.Diamonds) &&
                    cards.Any((item) => item.Type == type && item.Lear == CardLear.Clubs) &&
                    cards.Any((item) => item.Type == type && item.Lear == CardLear.Hearts) &&
                    cards.Any((item) => item.Type == type && item.Lear == CardLear.Spades))
                {
                    result = true;
                    combinationCards = new List<Card>();
                    combinationCards.AddRange(cards.Where((item) => item.Type == type).ToList());
                    break;
                }
                else
                {
                    continue;
                }
            }

            return result;
        }

        private static bool IsFullHouse(List<Card> cards, out List<Card> combinationCards)
        {
            var result = false;
            combinationCards = null;

            if (IsSet(cards, out combinationCards) == true)
            {
                List<Card> secondCombo = null;
                List<Card> secondCardSet = cards.Clone();

                secondCardSet.RemoveByValue(combinationCards[0]);
                secondCardSet.RemoveByValue(combinationCards[1]);
                secondCardSet.RemoveByValue(combinationCards[2]);

                if (IsOnePair(secondCardSet, out secondCombo) == true)
                {
                    result = true;
                    combinationCards.AddRange(secondCombo);

                    secondCombo.Clear();
                    secondCombo = null;
                }

                secondCardSet.Clear();
                secondCardSet = null;
            }

            return result;
        }

        private static bool IsFlush(List<Card> cards, out List<Card> combinationCards)
        {
            var result = false;
            var quantity = 0;
            combinationCards = null;

            cards.Sort();

            for (int i = 0; i <= 3; i++)
            {
                var lear = (CardLear)i;
                quantity = 0;

                for (int j = 0; j < cards.Count; j++)
                {
                    if (cards[j].Lear == lear)
                    {
                        quantity++;
                    }
                }

                if (quantity >= 5)
                {
                    result = true;
                    combinationCards = new List<Card>();
                    for (int j = 0; j <= 4; j++)
                    {
                        combinationCards.Add(cards[j].Clone() as Card);
                    }
                    break;
                }
            }

            return result;
        }

        private static bool IsStraight(List<Card> cards, out List<Card> combinationCards)
        {
            var result = false;
            combinationCards = null;

            cards.Sort();

            for (int i = 9; i >= 0; i--)
            {
                var type = (CardType)i;
                if (cards.Any((item) => item.Type == type + 0) &&
                    cards.Any((item) => item.Type == type + 1) &&
                    cards.Any((item) => item.Type == type + 2) &&
                    cards.Any((item) => item.Type == type + 3))
                {
                    result = true;
                    combinationCards = new List<Card>();
                    combinationCards.Add(cards.Find((item) => item.Type == type + 0));
                    combinationCards.Add(cards.Find((item) => item.Type == type + 1));
                    combinationCards.Add(cards.Find((item) => item.Type == type + 2));
                    combinationCards.Add(cards.Find((item) => item.Type == type + 3));
                    break;
                }
                else
                {
                    continue;
                }
            }

            return result;
        }

        private static bool IsSet(List<Card> cards, out List<Card> combinationCards)
        {
            var result = false;
            combinationCards = null;

            cards.Sort();

            for (int i = (int)CardType.Ace; i >= (int)CardType.Two; i--)
            {
                var type = (CardType)i;
                var set = cards.Where((item) => item.Type == type).ToList();
                if (set.Count == 3)
                {
                    result = true;
                    combinationCards = new List<Card>();
                    combinationCards.AddRange(set);
                    break;
                }
                else
                {
                    continue;
                }
            }

            return result;
        }

        private static bool IsTwoPair(List<Card> cards, out List<Card> combinationCards)
        {
            var result = false;
            combinationCards = null;

            if (IsOnePair(cards, out combinationCards) == true)
            {
                List<Card> secondCombo = null;
                List<Card> secondCardSet = cards.Clone();

                secondCardSet.RemoveByValue(combinationCards[0]);
                secondCardSet.RemoveByValue(combinationCards[1]);

                if (IsOnePair(secondCardSet, out secondCombo) == true)
                {
                    result = true;
                    combinationCards.AddRange(secondCombo);

                    secondCombo.Clear();
                    secondCombo = null;
                }

                secondCardSet.Clear();
                secondCardSet = null;
            }

            return result;
        }

        private static bool IsOnePair(List<Card> cards, out List<Card> combinationCards)
        {
            var result = false;
            combinationCards = null;

            cards.Sort();

            for (int i = (int)CardType.Ace; i >= (int)CardType.Two; i--)
            {
                var type = (CardType)i;
                var set = cards.Where((item) => item.Type == type).ToList();
                if (set.Count == 2)
                {
                    result = true;
                    combinationCards = new List<Card>();
                    combinationCards.AddRange(set);
                    break;
                }
                else
                {
                    continue;
                }
            }

            return result;
        }

        private static Card GetHighCard(List<Card> cards)
        {
            cards.Sort();
            return cards[0];
        }
    }
}
