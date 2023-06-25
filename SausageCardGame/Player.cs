using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SausageCardGame
{
    public class Player
    {
        public string Name { get; } 
        public List<Card> Hand { get; }  // Рука гравця (список карт)
        public Player(string name)
        {
            Name = name;
            Hand = new List<Card>();
        }

        // Метод додавання карти в руку гравця
        public void AddCardToHand(Card card)
        {
            Hand.Add(card);
        }

        public void RemoveCardFromHand(Card card)
        {
            Hand.Remove(card);
        }

        // Спосіб отримання карти найменшого значення в руці гравця
        public Card GetLowestCard()
        {
            Card lowestCard = Hand[0];

            // Перебираємо кожну карту в руці та знаходимо карту з найменшим значенням
            for (int i = 1; i < Hand.Count; i++)
            {
                if (Hand[i].CompareTo(lowestCard) == -1)
                {
                    lowestCard = Hand[i];
                }
            }

            return lowestCard;
        }

        // Спосіб отримання козиря з найменшою вартістю в руці гравця
        public Card GetLowestTrumpCard(Suit trumpSuit)
        {
            var lowestTrumpCard = Hand.Where(card => card.Suit == trumpSuit)
                .OrderBy(card => card)
                .FirstOrDefault();

            return lowestTrumpCard;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Player name: {Name}");
            sb.AppendLine("Cards in hand:");

            // Перебираємо кожну картку в руці та додаємо її рядкове представлення до StringBuilder
            foreach (Card card in Hand)
            {
                sb.Append(card.ToString() + " ");
            }

            return sb.ToString();
        }
    }
}