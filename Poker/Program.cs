using Poker.API;
using Poker.API.Cards;
using Poker.API.Cards.Enums;
using Poker.API.Extensions;
using Poker.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player("player");
            Player ai1 = new Player("ai1");
            Player ai2 = new Player("ai2");
            Player ai3 = new Player("ai3");

            CardDeck cardDeck = CardDeck.GetInstance();

            cardDeck.AddCardsToPlayer(player);
            cardDeck.AddCardsToPlayer(ai1);
            cardDeck.AddCardsToPlayer(ai2);
            cardDeck.AddCardsToPlayer(ai3);
            cardDeck.AddCardsToTable();
            cardDeck.AddCardToTable();
            cardDeck.AddCardToTable();

            Console.WriteLine("Table Cards:\n" + TableCards.ToString());
            Console.WriteLine("Player Cards:\n" + player.ToString());
            Console.WriteLine("AI1 Cards:\n" + ai1.ToString());
            Console.WriteLine("AI2 Cards:\n" + ai2.ToString());
            Console.WriteLine("AI3 Cards:\n" + ai3.ToString());

            var res = ResultManager.GetResult(new List<Player>() { player, ai1, ai2, ai3 });

            Console.WriteLine("Winner: " + res);

            Console.ReadKey();
        }
    }
}
