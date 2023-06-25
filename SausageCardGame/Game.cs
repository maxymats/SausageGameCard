using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SausageCardGame
{
    public class Game
    {
        static private Deck deck;
        private List<Player> players;  // Cписок гравців 
        private int currentPlayerIndex;  // Індекс поточного гравця в списку гравців
        private List<Card> cardsOnTable;  // Поточні карти на столі

        // Конструктор для ініціалізації гри з гравцями та необов’язковою кількістю карток
        public Game(List<Player> gamePlayers, uint amountOfCards = 52)
        {
            deck = new Deck(amountOfCards);
            deck.Shuffle();

            players = new List<Player>();
            foreach (Player player in gamePlayers)
            {
                players.Add(player);
            }

            currentPlayerIndex = 0;
            cardsOnTable = new List<Card>();
        }

        public List<Player> Players
        {
            get => players;
            set => players = value ?? throw new ArgumentNullException(nameof(value));
        }

        // Спосіб роздачі карт гравцям
        public void DealCards(int numCards)
        {
            for (int i = 0; i < numCards; i++)
            {
                foreach (Player player in players)
                {
                    Card card = deck.Deal();
                    if (card != null && player.Hand.Count < (numCards / players.Count) + 1)
                    {
                        player.AddCardToHand(card);
                    }
                }
            }
        }

        //Спосіб проведення раунду гри
        public bool PlayRound()
        {
            bool roundOver = false;
            int maxCards = 0;
            Player winner = null;

            // Кожен гравець розігрує карту зі своєї руки
            for (int i = 0; i < players.Count; ++i)
            {
                // Отримати поточного гравця на основі currentPlayerIndex 
                Player player = players[(currentPlayerIndex + i) % players.Count];

                // Виберіть першу карту з руки гравця та додайте її до столу
                Card cardToTable = player.Hand[0];
                cardsOnTable.Add(cardToTable);
                player.RemoveCardFromHand(cardToTable);
                Console.WriteLine($"{player.Name} played {cardToTable}");

                // Перевірте, чи якась карта на столі має те саме значення, але іншої масті
                if (cardsOnTable.Any(card => card.Value == cardToTable.Value && card.Suit != cardToTable.Suit))
                {
                    // Знайдіть індекс першої відповідної картки на столі
                    int index = cardsOnTable.FindIndex(card => card.Value == cardToTable.Value);

                    // Візьміть зі столу всі карти однакового значення, але різної масті
                    List<Card> takenCards = cardsOnTable.GetRange(index, cardsOnTable.Count - index);
                    player.Hand.AddRange(takenCards);
                    cardsOnTable.RemoveRange(index, takenCards.Count);
                    Console.WriteLine($"{player.Name} took {takenCards.Count} cards from the table");
                }

                // Перевірте, чи немає у гравця карток, і видаліть їх із гри
                if (player.Hand.Count == 0)
                {
                    Console.WriteLine($"Player {player.Name} has no more cards and loses the game.");
                    players.Remove(player);
                    roundOver = true;
                    break;
                }
            }

            // Перейти до наступного гравця для наступного раунду
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
            // "[(currentPlayerIndex + 1) % players.Count]" уникаєм IndexOutOfBordersException

            return roundOver;
        }

        // Метод, який повертає тру, коли залишився тільки один гравець з усіма картами - переможець
        private bool CheckWinner()
        {
            if (players.Count == 1)
            {
                Console.WriteLine($"Player {players.First().Name} has won the game!");
                return true;
            }
            return false;
        }

        // Спосіб запуску гри
        public void StartGame()
        {
            while (true)
            {
                bool roundOver = PlayRound();
                if (CheckWinner())
                {
                    Console.WriteLine("Game over.");
                    break;
                }
                if (roundOver)
                {
                    Thread.Sleep(3000);
                }
            }
        }
    }
}