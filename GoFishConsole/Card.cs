using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFishConsole
{
    public class Card
    {
        public Values Value { get; set; }
        public Suits Suit { get; set; }
        public Card(Values value,Suits suit)
        {
            Value = value;
            Suit = suit;
        }
        public override string ToString() => $"{Value} of {Suit}";
    }
}
