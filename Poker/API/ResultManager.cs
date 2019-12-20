using Poker.API.Cards;
using Poker.API.Cards.Enums;
using Poker.API.Extensions;
using Poker.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.API
{
    public class ResultManager
    {
        public static string GetResult(List<Player> players)
        {
            List<List<Card>> comboHolder = new List<List<Card>>();
            List<CardCombinations> cardCombo = new List<CardCombinations>();

            GetCombinationData(players, comboHolder, cardCombo);

            var result = ResultCalculation(players, comboHolder, cardCombo);
            Dispose(comboHolder, cardCombo);

            return result;
        }

        private static void GetCombinationData(List<Player> players, List<List<Card>> comboHolder, List<CardCombinations> cardCombo)
        {
            foreach (var item in players)
            {
                List<Card> combo = new List<Card>();
                List<Card> cards = new List<Card>();
                cards.AddRange(TableCards.Cards.Clone());
                var playerCombination = CombinationChecker.CheckCombinations(
                    cards,
                    item.Cards.Clone(),
                    out combo
                );
                cardCombo.Add(playerCombination);
                comboHolder.Add(combo);
            }
        }

        private static string ResultCalculation(List<Player> players, List<List<Card>> comboHolder, List<CardCombinations> cardCombo)
        {
            var maxCombo = cardCombo.Max();
            var resultSet = cardCombo.FindAll((item) => item == maxCombo);
            var gameResult = String.Empty;

            if (resultSet.Count > 1)
            {
                List<CardType> minCards = new List<CardType>();
                List<CardType> maxCards = new List<CardType>();
                List<Player> playersAboutToWin = new List<Player>();
                var indices = GetIndices(maxCombo, resultSet.Count, cardCombo.Clone());

                indices.ForEach((item) =>
                {
                    playersAboutToWin.Add(players[item]);
                    minCards.Add(players[item].MinCard().Type);
                    maxCards.Add(players[item].MaxCard().Type);
                });

                gameResult = GetMaxCardsList(players, comboHolder, cardCombo, minCards, maxCards, playersAboutToWin);
            }
            else
            {
                var index = cardCombo.FindIndex((item) => item == maxCombo);
                gameResult = GetGameResult(players[index].Name, cardCombo[index], comboHolder[index]);
            }

            return gameResult;
        }

        private static string GetMaxCardsList(List<Player> players, List<List<Card>> comboHolder, List<CardCombinations> cardCombo, 
            List<CardType> minCards, List<CardType> maxCards, List<Player> playersAboutToWin)
        {
            var gameResult = String.Empty;
            var card = maxCards.Max();
            var newMaxCards = maxCards.Where((item) => item == card).ToList();
            var cardsToRemove = maxCards.Except(newMaxCards).ToList();

            cardsToRemove.ForEach((item) =>
            {
                var index = maxCards.FindIndex((itm) => itm == item);
                minCards.RemoveAt(index);
                maxCards.RemoveAt(index);
                playersAboutToWin.RemoveAt(index);
            });
            cardsToRemove.Clear();
            cardsToRemove = null;

            if (newMaxCards.Count == 1)
            {
                newMaxCards.Clear();
                newMaxCards = null;
                var index = players.FindIndex((item) => item.Name.Equals(playersAboutToWin[0].Name));
                gameResult = GetGameResult(players[index].Name, cardCombo[index], comboHolder[index]);
            }
            else
            {
                gameResult = GetTopMinCardsList(players, comboHolder, cardCombo, minCards, maxCards, playersAboutToWin);
            }

            return gameResult;
        }

        private static string GetTopMinCardsList(List<Player> players, List<List<Card>> comboHolder, List<CardCombinations> cardCombo,
            List<CardType> minCards, List<CardType> maxCards, List<Player> playersAboutToWin)
        {
            var gameResult = String.Empty;
            var card = minCards.Max();
            var newMinCards = minCards.Where((item) => item == card).ToList();
            var cardsToRemove = minCards.Except(newMinCards).ToList();

            cardsToRemove.ForEach((item) =>
            {
                var index = minCards.FindIndex((itm) => itm == item);
                minCards.RemoveAt(index);
                maxCards.RemoveAt(index);
                playersAboutToWin.RemoveAt(index);
            });
            cardsToRemove.Clear();
            cardsToRemove = null;

            if (newMinCards.Count == 1)
            {
                newMinCards.Clear();
                newMinCards = null;
                var index = players.FindIndex((item) => item.Name.Equals(playersAboutToWin[0].Name));
                gameResult = GetGameResult(players[index].Name, cardCombo[index], comboHolder[index]);
                gameResult += String.Format(",[{0} : HighCard, {1}]", players[index].Name, 
                    players[index].GetCardByType(card).ToString());
            }
            else
            {
                gameResult = "[draw : ";
                playersAboutToWin.ForEach((item) =>
                {
                    gameResult += item.Name + ", ";
                });
                gameResult = gameResult.Remove(gameResult.LastIndexOf(','));
                gameResult += "]";
            }

            return gameResult;
        }

        private static List<int> GetIndices(CardCombinations maxCombo, int comboQuantity, List<CardCombinations> cardCombo)
        {
            List<int> indices = new List<int>();

            for (int i = 0; i < comboQuantity; i++)
            {
                var index = cardCombo.FindLastIndex((item) => item == maxCombo);
                indices.Add(index);
                cardCombo.RemoveAt(index);
            }

            return indices;
        }

        private static string GetGameResult(string name, CardCombinations combo, List<Card> cards)
        {
            var strCards = cards.ToString(true).Replace(Environment.NewLine, ", ");
            strCards = strCards.Remove(strCards.LastIndexOf(','), 2);
            return String.Format("[{0} : {1}, [{2}]]", name, combo.ToString(), strCards);
        }

        private static void Dispose(List<List<Card>> comboHolder, List<CardCombinations> cardCombo)
        {
            comboHolder.ForEach((item) =>
            {
                item.Clear();
                item = null;
            });
            comboHolder.Clear();
            cardCombo.Clear();
            comboHolder = null;
            cardCombo = null;
        }

    }
}
