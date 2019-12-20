using Poker.API.Cards;
using Poker.API.Cards.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.API.Extensions
{
    public static class CardExtensions
    {
        public static CardCombinations CheckCombination(this CardCombinations type, List<Card> cards, List<Card> playerCards, out List<Card> combinationCards)
        {
            combinationCards = null;
            return CombinationChecker.CheckCombinations(cards, playerCards, out combinationCards);
        }

        public static List<CardCombinations> Clone(this List<CardCombinations> cardCombo)
        {
            List<CardCombinations> newList = new List<CardCombinations>();

            foreach (var item in cardCombo)
            {
                CardCombinations newItem = item;
                newList.Add(newItem);
            }

            return newList;
        }

        public static List<Card> Clone(this List<Card> cards)
        {
            List<Card> newList = new List<Card>();

            foreach (var item in cards)
            {
                newList.Add(item.Clone() as Card);
            }

            return newList;
        }

        public static bool RemoveByValue(this List<Card> cards, Card card)
        {
            return cards.Remove(cards.Find((item) => item.Type == card.Type && item.Lear == card.Lear));
        }

        public static string ToString(this List<Card> cards, bool stub)
        {
            var result = String.Empty;

            cards.ForEach((item) =>
            {
                result += String.Format("[{0}, {1}]{2}", item.Type, item.Lear, Environment.NewLine);
            });

            return result;
        }
    }
}
