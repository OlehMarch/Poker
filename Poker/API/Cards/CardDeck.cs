using Poker.API.Cards.Enums;
using Poker.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.API.Cards
{
    public sealed class CardDeck
    {
        public static CardDeck GetInstance()
        {
            if (deck == null)
            {
                deck = new CardDeck();
            }

            return deck;
        }

        private CardDeck()
        {
            Deck = new List<Card>();
            Availability = new List<bool>();
            InitializeDeck();
        }


        public List<Card> Deck { get; set; }
        public List<bool> Availability { get; set; }
        private static CardDeck deck;


        private void InitializeDeck()
        {
            for (int i = 0; i <= (int)CardType.Ace; i++)
            {
                var type = (CardType)i;
                for (int j = 0; j <= 3; j++)
                {
                    var lear = (CardLear)j;
                    Deck.Add(new Card(type, lear));
                    Availability.Add(true);
                }
            }
        }

        private Card GetRandomCard()
        {
            var index = new Random().Next(0, 52);
            Card card = null;

            if (Availability[index] == true)
            {
                Availability[index] = false;
                card = Deck[index].Clone() as Card;
            }

            return card;
        }

        public void AddCardsToPlayer(Player player)
        {
            while (true)
            {
                if (player.Cards.Count == 2)
                {
                    break;
                }

                var card = GetRandomCard();

                if (card != null)
                {
                    player.Cards.Add(card);
                }
            }
        }

        public void AddCardsToTable()
        {
            while (true)
            {
                if (TableCards.Cards.Count == 3)
                {
                    break;
                }

                var card = GetRandomCard();

                if (card != null)
                {
                    TableCards.Cards.Add(card);
                }
            }
        }

        public void AddCardToTable()
        {
            while (true)
            {
                var card = GetRandomCard();

                if (card != null)
                {
                    TableCards.Cards.Add(card);
                    break;
                }
            }
        }

    }
}
