using System;
using System.Collections.Generic;

namespace SausageCardGame
{
    public class Deck
    {
        private List<Card> cards;  // Список для зберігання карт у колоді

        public Deck()
        {
            cards = new List<Card>();

            // Створення карток для кожної масті та комбінації значень
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                {
                    cards.Add(new Card(suit, value));
                }
            }
        }

        // Конструктор для створення колоди з певною кількістю карт
        public Deck(uint amountOfCards)
        {
            cards = new List<Card>();

            // Створення карток на основі вказаної кількості
            if (amountOfCards == 52)
            {
                // Створення стандартної колоди з 52 карт
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
                    {
                        cards.Add(new Card(suit, value));
                    }
                }
            }
            else if (amountOfCards == 36)
            {
                // Створення колоди з 36 карт (видалення карт нижче шести)
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    for (int i = (int)CardValue.Six; i <= (int)CardValue.Ace; i++)
                    {
                        cards.Add(new Card(suit, (CardValue)i));
                    }
                }
            }
        }

        // Метод тасування колоди за алгоритмом Фішера-Йетса
        public void Shuffle()
        {
            Random rng = new Random();

            // Перегляд кожної карти в колоді від останньої до першої
            for (int i = cards.Count - 1; i > 0; i--)
            {
                // Створення випадкового індексу для обміну з поточною карткою
                int j = rng.Next(i + 1);

                // Поміняти поточну картку випадково вибраною
                (cards[i], cards[j]) = (cards[j], cards[i]);
            }
        }

        // Спосіб роздачі карти з колоди
        public Card Deal()
        {
            if (cards.Count == 0)
            {
                return null;
            }

            // Видаляєм першу карту з колоди та повертаєм її
            Card card = cards[0];
            cards.RemoveAt(0);
            return card;
        }
    }
}