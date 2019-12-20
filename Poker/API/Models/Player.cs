using Poker.API.Cards;
using Poker.API.Cards.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.API.Models
{
    public class Player : ICloneable
    {
        public Player()
        {
            Cards = new List<Card>();
        }

        public Player(string name) : this()
        {
            Name = name;
        }


        public string Name { get; set; }
        public List<Card> Cards { get; set; }


        public override string ToString()
        {
            var result = String.Empty;

            Cards.ForEach((item) =>
            {
                result += String.Format("[{0}, {1}]{2}", item.Type, item.Lear, Environment.NewLine);
            });

            return result;
        }

        public Card MinCard()
        {
            return Cards[0].Type > Cards[1].Type ? Cards[1] : Cards[0];
        }

        public Card MaxCard()
        {
            return Cards[0].Type > Cards[1].Type ? Cards[0] : Cards[1];
        }

        public Card GetCardByType(CardType type)
        {
            return Cards.First((item) => item.Type == type);
        }

        public object Clone()
        {
            return new Player(this.Name)
            {
                Cards = this.Cards
            };
        }
    }
}
