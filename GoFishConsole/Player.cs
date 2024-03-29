﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFishConsole
{
    public class Player
    {
        public static Random Random = new Random();

        private List<Card> hand = new List<Card>();
        /// <summary>
        /// The cards in the player's hand.
        /// </summary>
        public IEnumerable<Card> Hand => hand;

        private List<Values> books = new List<Values>();
        /// <summary>
        /// The books that player has pulled out.
        /// </summary>
        public IEnumerable<Values> Books => books;

        public readonly string Name;
        /// <summary>
        /// Constructor to create a player.
        /// </summary>
        /// <param name="name">Player's name.</param>
        public Player(string name) => Name = name;

        /// <summary>
        /// Alternate constructor (used for unit testing)
        /// </summary>
        /// <param name="name">Player's name</param>
        /// <param name="cards">Initial set of cards</param>
        public Player(string name, IEnumerable<Card> cards)
        {
            Name = name;
            hand.AddRange(cards);
        }

        /// <summary>
        /// Pluralize a word, adding "s" if a value isn't equal to 1
        /// </summary>
        /// <param name="s"></param>
        /// <returns>returns "s" to make a word plural if input parameter is greater than 1.</returns>
        public static string S(int s) => s == 1 ? "" : "s";

        /// <summary>
        /// Returns the current status of the player: the number of cards and books.
        /// </summary>
        public string Status =>
            $"{Name} has {Hand.Count()} card{S(Hand.Count())} and {Books.Count()} book{S(Books.Count())}";


        /// <summary>
        /// Gets up to five cards from the stock
        /// </summary>
        /// <param name="stock">Stock to get the next hand from</param>        
        public void GetNextHand(Deck stock)
        {
            while (stock.Count > 0 && hand.Count < 5)
            {
                hand.Add(stock.Deal(0));
            }
        }

        /// <summary>
        /// If I have any cards that match the value, return them. If I run out of cards,
        /// get the next hand from the deck.
        /// </summary>
        /// <param name="value">Value I'm asked for</param>
        /// <param name="deck">Deck to draw my next hand from</param>
        /// <returns>The cards that were pulled out of the other player's hand</returns>        
        public IEnumerable<Card> DoYouHaveAny(Values value, Deck deck)
        {
            var matchingCards = from card in hand
                                where card.Value == value
                                orderby card.Suit
                                select card;

            hand = (from card in hand
                    where card.Value != value
                    select card).ToList();

            if (hand.Count() == 0) GetNextHand(deck);

            return matchingCards;
        }

        /// <summary>
        /// When the player receives cards from another player, adds them to the hand
        /// and pulls out any matching books
        /// </summary>
        /// <param name="cards">Cards from the other player to add</param>        
        public void AddCardsAndPullOutBooks(IEnumerable<Card> cards)
        {
            hand.AddRange(cards);
            var matchingCards = hand
                .GroupBy(card => card.Value)
                .Where(group => group.Count() == 4)
                .Select(group => group.Key);

            books.AddRange(matchingCards);
            books.Sort();

            hand = hand
                .Where(card => !books.Contains(card.Value))
                .ToList();
        }

        /// <summary>
        /// Draws a card from the stock and add it to the player's hand
        /// </summary>
        /// <param name="stock">Stock to draw a card from</param>        
        public void DrawCard(Deck stock)
        {
            if (stock.Count > 0)
                AddCardsAndPullOutBooks(new List<Card> { stock.Deal(0) });
        }

        /// <summary>
        /// Gets a random value from the player's hand
        /// </summary>
        /// <returns>the value of a randomly selected card in the player's hand</returns>        
        public Values RandomValuesFromHand() => hand.OrderBy(card=>card.Value)
            .Select(card=>card.Value)
            .Skip(Random.Next(hand.Count()))
            .First();

        public override string ToString() => Name;
    }
}
