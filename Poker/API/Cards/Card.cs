using Poker.API.Cards.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.API.Cards
{
    public class Card : IComparable, ICloneable
    {
        public CardLear Lear { get; set; }
        public CardType Type { get; set; }


        public Card(CardType Type, CardLear Lear)
        {
            this.Type = Type;
            this.Lear = Lear;
        }

        public int CompareTo(object obj)
        {
            if (this.Type > (obj as Card).Type)
                return -1;
            if (this.Type < (obj as Card).Type)
                return 1;
            else
                return 0;
        }

        public object Clone()
        {
            return new Card(this.Type, this.Lear);
        }

        public override string ToString()
        {
            return String.Format("[{0}, {1}]", this.Type, this.Lear);
        }
    }
}
