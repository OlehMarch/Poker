using Poker.API.Cards;
using Poker.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.API.Extensions
{
    public static class PlayerExtensions
    {
        public static List<Player> Clone(this List<Player> players)
        {
            List<Player> newList = new List<Player>();

            foreach (var item in players)
            {
                newList.Add((Player)item.Clone());
            }

            return newList;
        }

        public static int IndexByValue(this List<Player> players, Player player)
        {
            var result = 0;

            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Name.Equals(player.Name))
                {
                    result = i;
                    break;
                }
            }

            return result;
        }
    }
}
