using System;
using System.Collections.Generic;

namespace SausageCardGame
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Game game = new Game(new List<Player> { new Player("Maksym"), new Player("Dima"), new Player("Pavlo"), new Player("Roman") }, 52);

            // Роздаєм картки гравцям
            game.DealCards(52);

            // Отримуєм список гравців з гри
            List<Player> players = game.Players;

            foreach (Player player in players)
            {
                Console.WriteLine(player);
            }
            //початок гри
            game.StartGame();
        }
    }
}