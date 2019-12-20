using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.API.Cards
{
    public static class TableCards
    {
        static TableCards()
        {
            Cards = new List<Card>();
        }


        public static List<Card> Cards { get; set; }


        public new static string ToString()
        {
            var result = String.Empty;

            Cards.ForEach((item) =>
            {
                result += String.Format("[{0}, {1}]{2}", item.Type, item.Lear, Environment.NewLine);
            });

            return result;
        }

    }
}
