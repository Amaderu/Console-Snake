using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Game Game = new Game();
            Console.CursorVisible = false;
            do
            {
                Game.NextMap();
                Game.Move();
                Game.ShowPos();
            }
            while (Game.GameStatus);
            Console.Clear();
            Console.WriteLine("Нажмите любую кнопку для выхода");
            Console.ReadLine();
        }
    }
}
