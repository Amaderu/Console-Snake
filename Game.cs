using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Game
    {
        private const int constTime = 500;
        private const int TimeEasy = 500; 
        private const int TimeMedium = 300; 
        private const int TimeHard = 100; 
        private const int height = 10;
        private const int lenght = 30;
        public int challenge;
        int[,] Body = new int[2, lenght * height];//0-x 1-y
        private char SnakeHead = '@';
        private char SnakeBody = '0';
        private char Fruct = '*';
        Random rand = new Random();
        int HeadX, HeadY;
        int FructX, FructY;

        int count = 0;
        public bool GameStatus = true;
        public bool GameEND = false;//для самоседения
        char[,] field = new char[height + 2, lenght + 2];
        private int endSnakeX;
        private int endSnakeY;
        int Time = constTime, TimeMINUS = 0;
        bool eating = false;
        //
        private int EX;
        private int EY;
        //

        ConsoleKeyInfo mKey = new ConsoleKeyInfo('P', ConsoleKey.P, false, false, false);

        public Game()
        {
            this.GAMECHALLENGE();
            this.Walls();
            this.StartPoz();
            this.DrowMap();
            this.ShowPos();

        }

        public void GAMECHALLENGE()//easy-1;medium-2;hard-3
        {
            string input;
            int height;
            int lenght;
            Console.WriteLine("Выберите сложность игры //easy-1;medium-2;hard-3");
            do
            {
                input = Console.ReadLine();
                Int32.TryParse(input, out challenge);
            } while ((challenge > 3)||(challenge < 1));

            if (challenge == 1)
            {
                Console.WriteLine("Вы выбрали сложность игры easy 10x10 скорость 500");
                height = 10;
                lenght = 10;
                char[,] fieldEasy = new char[height + 2, lenght + 2];
                int[,] NewBody = new int[2, lenght * height];
                field = fieldEasy;
                Body = NewBody;
                Time = TimeEasy;

            }
            else if (challenge == 2)
            {
                Console.WriteLine("Вы выбрали сложность игры medium 15x15 скорость 300");
                height = 15;
                lenght = 15;
                char[,] fieldmedium = new char[height + 2, lenght + 2];
                int[,] NewBody = new int[2, lenght * height];
                field = fieldmedium;
                Body = NewBody;
                Time = TimeMedium;
            }
            else if (challenge == 3)
            {
                Console.WriteLine("Вы выбрали сложность игры medium 20x20 скорость 100");
                height = 20;
                lenght = 20;
                char[,] fieldhard = new char[height + 2, lenght + 2];
                int[,] NewBody = new int[2, lenght * height];
                Body = NewBody;
                field = fieldhard;
                Time = TimeHard;
            }
            System.Threading.Thread.Sleep(2000);
            Console.Clear();
        }

        public void ShowPos()
        {
            string FX, FY, snakeHeadX, snakeHeadY, snakeEndX = "", snakeEndY = "";
            if (FructX < 10) FX = " " + FructX;
            else FX = "" + FructX;
            if (FructY < 10) FY = " " + FructY;
            else FY = "" + FructY;
            if (Body[0, 0] < 10) snakeHeadX = " " + Body[0, 0];
            else snakeHeadX = "" + Body[0, 0];
            if (Body[1, 0] < 10) snakeHeadY = " " + Body[1, 0];
            else snakeHeadY = "" + Body[1, 0];

            if (Body[0, count] < 10) snakeEndX = " " + Body[0, count];
            else snakeEndX = "" + Body[0, count];
            if (Body[1, count] < 10) snakeEndY = " " + Body[1, count];
            else snakeEndY = "" + Body[1, count];
            Console.SetCursorPosition(0, (field.GetLength(0) - 1) + 6);
            Console.Write("            ");
            Console.SetCursorPosition(0, (field.GetLength(0) - 1) + 3);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"SnakeHead X:{snakeHeadX} Y:{snakeHeadY}");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.SetCursorPosition(20, (field.GetLength(0) - 1) + 3);
            Console.Write($"Fruct X:{FX} Y:{FY}\n");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            if (Body[0, count] == Body[0, 0] && Body[1, count] == Body[1, 0]) Console.BackgroundColor = ConsoleColor.Yellow;
            Console.Write($"SnakeTail X:{snakeEndX} Y:{snakeEndY}\nсчёт {count}\nСкорость {Time}");
            Console.ResetColor();
        }
        private void Walls()
        {
            int h= field.GetLength(0), l= field.GetLength(1);
            for (int i = 0; i < l; i++)
            {
                field[0, i] = '#';
                field[l - 1, i] = '#';
            }/*Up and Down Border*/
            for (int i = 0; i < h ; i++)
            {
                field[i, 0] = '#';
                field[i, h - 1] = '#';
            }
            /*Left and Right Border*/
        }
        public void StartPoz()
        {
            int h = field.GetLength(0), l = field.GetLength(1);
            count = 0;
            do
            {
                HeadX = rand.Next(1, l-1);
                HeadY = rand.Next(1, h - 1);
                FructX = rand.Next(1, l - 1);
                FructY = rand.Next(1, h - 1);
            } while (HeadX == FructX && HeadY == FructY);

            Body[0, count] = HeadX;
            Body[1, count] = HeadY;
            //
            endSnakeX = Body[0, count];
            endSnakeY = Body[1, count];
            //
            field[HeadY, HeadX] = SnakeHead;
            field[FructY, FructX] = Fruct;

        }
        private void DrowMap()
        {
            int FLY = field.GetLength(0), FLX = field.GetLength(1);
            for (int i = 0; i < FLY; i++)
            {
                for (int j = 0; j < FLX; j++)
                {
                    if (field[i, j] == SnakeHead || field[i, j] == SnakeBody)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                    }
                    else if (field[i, j] == Fruct)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    Console.Write(field[i, j]);
                    if(i< FLY-2&& j < FLX - 2)
                    field[i+1, j+1] = ' ';
                    
                }
                Console.WriteLine();
            }
            Console.ResetColor();

        }
        public void New_fruct()
        {
            bool stat = true;
            int rescount = 0;
            System.Threading.Thread.Sleep(Time);//time 400
            if (Body[0, count] == FructX && Body[1, count] == FructY)
            {
                eating = true;
                EX = FructX;
                EY = FructY;
                do
                {
                    FructX = rand.Next(1, field.GetLength(1)-1);
                    FructY = rand.Next(1, field.GetLength(0)-1);
                    for (int j = 0; j < count + 1; j++)
                    {
                        if (Body[0, j] == FructX && Body[1, j] == FructY)
                        {
                            rescount++;
                        }

                    }
                    if (rescount < count + 1) { stat = false; }
                } while (stat);
            }
        }
        private void EatFruct()
        {
            if (eating)
            {
                if (Time > 50) { Time = Time - TimeMINUS; }
                if (EX == endSnakeX && EY == endSnakeY)
                {
                    count++;
                    Body[0, count] = endSnakeX;
                    Body[1, count] = endSnakeY;
                    eating = false;
                }
            }
        }
        private void restartMap()
        {
            if (challenge==1) { Time = TimeEasy; }
            else if(challenge == 2) { Time = TimeMedium; }
            else if(challenge == 3) { Time = TimeHard; }
            //Console.SetCursorPosition(field.GetLength(1) + 10, field.GetLength(0) / 2);
            //Console.Write($"Идёт подготовка поля подождите {3} сек");
            //Console.SetCursorPosition(field.GetLength(1) + 10, field.GetLength(0) / 2);
            //Console.Write($"                                             ");
            for (int i = 1; i < count; i++)
            {
                Console.SetCursorPosition(Body[0, i], Body[1, i]);
                Console.Write(" ");
            }
            Console.SetCursorPosition(FructX, FructY);
            Console.Write(" ");
        }
        private void ClearEndSnake()
        {
            Console.SetCursorPosition(endSnakeX, endSnakeY);
            Console.Write($" ");

        }
        public void NextMap()
        {
            Console.SetCursorPosition(FructX, FructY);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write(Fruct);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(Body[0, 0], Body[1, 0]);
            Console.Write(SnakeHead);
            if (count > 0)
            {
                for (int i = 1; i < count + 1; i++)
                {
                    Console.SetCursorPosition(Body[0, i], Body[1, i]);
                    Console.Write(SnakeBody);
                }
            }
            Console.ResetColor();
        }
        private void Segmentation()//-> сдвиг значений
        {
            endSnakeX = Body[0, count];//y-1 x-0 [0,..]
            endSnakeY = Body[1, count];
            for (int i = count; i > 0; --i)
            {
                Body[0, i] = Body[0, i - 1];//x
                Body[1, i] = Body[1, i - 1];//y
            }
        }

        public void Move()
        {
            if (mKey.Key == ConsoleKey.Q || Console.KeyAvailable == true)
            {
                mKey = Console.ReadKey(true);
            }
            switch (mKey.Key)
            {
                case ConsoleKey.UpArrow:
                    if (Body[1, 0] > 0)//y-1 x-0 [0,..]  >1
                    {
                        Segmentation();
                        Body[1, 0]--;

                    }
                    break;
                case ConsoleKey.LeftArrow:
                    if (Body[0, 0] > 0)
                    {
                        Segmentation(); Body[0, 0]--;

                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (Body[1, 0] < field.GetLength(0)-1)
                    {
                        Segmentation(); Body[1, 0]++;

                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (Body[0, 0] < field.GetLength(1)-1)
                    {
                        Segmentation();
                        Body[0, 0]++;
                    }
                    break;
                default:
                    break;
            }
            New_fruct();
            ClearEndSnake();
            EatFruct();
            Game_Over();

        }
        public void Game_Over()
        {
            int height = field.GetLength(0), lenght = field.GetLength(1);
            for (int i = count; i > -1; --i)
            {
                if ((Body[1, 0] == 0) || (Body[0, 0] == 0) || (Body[1, 0] == height -1) || (Body[0, 0] == lenght - 1))//y-1 x-0 [0,..]  >1
                {
                    GameEND = true;
                    Console.SetCursorPosition(field.GetLength(1) + 10, field.GetLength(0) / 2);
                    Console.Write("Game Over");
                    break;
                }
                else if (i > 0)
                {
                    if (Body[0, 0] == Body[0, i] && Body[1, 0] == Body[1, i])
                    {
                        GameEND = true;
                        Console.SetCursorPosition(field.GetLength(1) + 10, field.GetLength(0) / 2);
                        Console.Write("Game Over");
                        break;
                    }
                }

            }
            while (GameEND)
            {
                if (mKey.Key == ConsoleKey.Q || Console.KeyAvailable == true)
                {
                    mKey = Console.ReadKey(true);
                    Console.SetCursorPosition(field.GetLength(1) + 10, field.GetLength(0) / 2);
                    Console.Write("Ещё раз? Y/N");
                    switch (mKey.Key)
                    {
                        case ConsoleKey.Y:
                            restartMap();
                            StartPoz();
                            Console.SetCursorPosition(0,0);
                            DrowMap();
                            GameEND = false;
                            break;
                        case ConsoleKey.N:
                            GameEND = false;
                            GameStatus = false;
                            break;
                        default:
                            break;
                    }
                    System.Threading.Thread.Sleep(500);
                    Console.SetCursorPosition(field.GetLength(1) + 10, field.GetLength(0) / 2);
                    Console.Write("            ");
                }
            }
        }

    }
}




