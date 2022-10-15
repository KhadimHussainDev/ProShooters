using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ProShooters.Classes;
using EZInput;
namespace ProShooters
{
    internal class Program
    {
        static char[,] gameBoard = new char[30, 100];
        static void Main()
        {
            game Game = new game();
            loadScores(Game.highScores);
            start(ref Game);
            playGame(ref Game);
        }
        static void loadScores(List<int> h)
        {
            string path = "Highscores.txt";
            StreamReader sr = new StreamReader(path);
            while (!sr.EndOfStream)
            {
                h.Add(int.Parse(sr.ReadLine()));
            }
            sr.Close();
        }
        static void loadLevel()
        {
            // string path = "E:\\2nd Semester\\Projects In C#\\ClearIT\\Data Files\\Level.txt";
            string path = "Level.txt";
            StreamReader sr = new StreamReader(path);
            string line;
            for (int i = 0; i < 30; i++)
            {
                line = sr.ReadLine();
                for (int j = 0; j < 99; j++)
                {
                    gameBoard[i, j] = line[j];
                }
            }
            sr.Close();
        }
        static void showLevel()
        {
            for (int i = 0; i < 30; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    if (gameBoard[i, j] == '#')
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.Write(gameBoard[i, j]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(gameBoard[i, j]);
                    }
                }
                Console.Write("\n");
            }
        }
        static void start(ref game Game)
        {
            Game.health.Enemy1 = 100;
            Game.health.Enemy2 = 100;
            Game.health.Enemy3 = 100;
            Game.health.boss = 100;
            Game.counters.level = 1;
            Game.counters.scores = 0;
            Game.boss.X = 28;
            Game.boss.Y = 50;
            Game.enemy1.X = 1;
            Game.enemy1.Y = 3;
            Game.enemy1.H = true;
            Game.enemy2.X = 7;
            Game.enemy2.Y = 35;
            Game.enemy2.H = true;
            Game.enemy3.X = 1;
            Game.enemy3.Y = 65;
            Game.enemy3.H = true;
            Game.cursor.X = 110;
            Game.cursor.Y = 1;
            Game.move.enemyMove1 = 0;
            Game.move.enemyMove2 = 30;
            Game.move.enemyMove3 = 0;
        }
        static void playGame(ref game Game)
        {
            while (true)
            {

                Console.Clear();
                string option = menu();
                if (option == "1")
                {
                    playLevel(ref Game);
                }
                else if (option == "2")
                {
                    playLevel(ref Game);
                    //chooseLevel();
                }
                else if (option == "3")
                {
                    printHighScores(Game.highScores);
                }
                else if (option == "4")
                {
                    //  storeScores();
                    Console.Clear();
                    header();
                    Console.Write("Thanks For Playing....");
                    Console.Write("\n");


                    Environment.Exit(0);
                }
                else
                {
                    Console.Write("Invalid Option.\nTry Again.\n");
                    playGame(ref Game);
                }
                Console.Write("Press Any Key To Continue...");
                Console.ReadKey();
            }
        }
        static void printHighScores(List<int> high)
        {
            Console.Clear();
            header();
            Console.WriteLine("High Scores : ");
            high.Sort((x, y) => x.CompareTo(y));
            high.Reverse();
            int count = high.Count;
            if (count > 10)
            {
                count = 10;
            }
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(i + 1 + "." + high[i]);
            }
        }
        static void playLevel(ref game Game)
        {
            Console.Clear();
            loadLevel();
            showLevel();
            //..............................................While
            // Loop.................................
            int count = 0,count1=0;
            while (true)
            {
                Thread.Sleep(50);
                Console.ForegroundColor = ConsoleColor.Blue;

                printBoss(Game.boss.X,Game.boss.Y);
                Console.ResetColor();

                movebulletup(ref Game);
                
                printScore(ref Game);
                printEnemy1(ref Game.enemy1.X, ref Game.enemy1.Y, Game.health.Enemy1);
                if (Game.health.Enemy1 > 0)
                {
                    
                    moveEnemy1(ref Game.enemy1.X, ref Game.enemy1.Y, ref Game.move.enemyMove1);
                }
                else
                {
                    if (Game.enemy1.H)
                    {
                        Game.enemy1.H = false;
                        empty(Game.enemy1.X, Game.enemy1.Y);
                    }
                }
                printEnemy2(ref Game.enemy2.X, ref Game.enemy2.Y, Game.health.Enemy2);
                if (Game.health.Enemy2 > 0)
                {
                    moveEnemy2(ref Game.enemy2.X, ref Game.enemy2.Y, ref Game.move.enemyMove2);
                }
                else
                {
                    if (Game.enemy2.H)
                    {
                        Game.enemy2.H = false;
                        empty(Game.enemy2.X, Game.enemy2.Y);
                    }
                }
                printEnemy3(ref Game.enemy3.X, ref Game.enemy3.Y, Game.health.Enemy3);
                if (Game.health.Enemy3 > 0)
                {
                    moveEnemy3(ref Game.enemy3.X, ref Game.enemy3.Y, ref Game.move.enemyMove3);
                }
                else
                {
                    if (Game.enemy3.H)
                    {
                        Game.enemy3.H = false;
                        empty(Game.enemy3.X, Game.enemy3.Y);
                    }
                }
                if (count == 0)
                {
                    if (Game.health.Enemy1 > 0)
                    {
                        creatEnemy(ref Game.enemy1.X, ref Game.enemy1.Y);
                    }
                    if (Game.health.Enemy2 > 0)
                    {
                        creatEnemy(ref Game.enemy2.X, ref Game.enemy2.Y);
                    }
                    if (Game.health.Enemy3 > 0)
                    {
                        creatEnemy(ref Game.enemy3.X, ref Game.enemy3.Y);
                    }
                    count = 3;
                }
                count--;
                moveBulletDown(ref Game);
                Console.ForegroundColor = ConsoleColor.DarkCyan;

                if (Keyboard.IsKeyPressed(Key.RightArrow))
                {
                    moveRight(ref Game);
                }
                if (Keyboard.IsKeyPressed(Key.LeftArrow))
                {

                    moveLeft(ref Game);
                }
                Console.ResetColor();

                if (Keyboard.IsKeyPressed(Key.Space))
                {
                    if (count1 <= 0)
                    {
                        count1 = 3;
                    bullet(ref Game);
                    }
                    
                }
                count1--;
                if (Keyboard.IsKeyPressed(Key.Escape))
                {
                    Game.highScores.Add(Game.counters.scores);
                    Game.counters.level = 1;
                    Game.counters.scores = 0;
                    storeScores(Game.highScores);
                    Main();
                }
            }
        }
        static void printBoss(int x,int y)
        {
            Console.SetCursorPosition(y-3,x);
            Console.Write("o  o  o");
            gameBoard[x, y - 3] = gameBoard[x, y - 1] = gameBoard[x, y + 1] = '+';
            Console.SetCursorPosition( y-5, x -1);
            Console.Write("<--------->");
            gameBoard[x - 1, y - 5] = gameBoard[x - 1, y + 5] = '&';
            gameBoard[x - 1, y - 4] = gameBoard[x - 1, y - 3] = gameBoard[x - 1, y - 2] = gameBoard[x - 1, y - 1] = gameBoard[x - 1, y] = gameBoard[x - 1, y + 1] = gameBoard[x - 1, y + 2] = gameBoard[x - 1, y + 3] = gameBoard[x - 1, y + 4] = '+';
            Console.SetCursorPosition( y-3, x-2);
            Console.Write("!  !  !");
            gameBoard[x-2, y - 3] = gameBoard[x-2, y - 1] = gameBoard[x-2, y + 1] = '+';

        }
        static void storeScores(List<int> high)
        {

            StreamWriter sw = new StreamWriter("Highscores.txt", false);
            high.Sort((x, y) => x.CompareTo(y));
            high.Reverse();
            int count = high.Count;
            if (count > 10)
            {
                count = 10;
            }
            for (int i = 0; i < count; i++)
            {
                sw.WriteLine(high[i]);
            }
            sw.Flush();
            sw.Close();
        }
        static void movebulletup(ref game Game)
        {
            for (int i = 0; i < 30; i++)
            {

                for (int j = 0; j < 100; j++)
                {
                    if (gameBoard[i, j] == '^')
                    {
                        gameBoard[i, j] = ' ';
                        Console.SetCursorPosition(j, i);
                        Console.Write(" ");
                        if (gameBoard[i - 1, j] == ' ')
                        {
                            gameBoard[i - 1, j] = '^';
                            Console.SetCursorPosition(j, i - 1);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("^");
                            Console.ResetColor();
                        }
                        if (gameBoard[i - 1, j] == '*')
                        {
                            gameBoard[i - 1, j] = ' ';
                            Console.SetCursorPosition(j, i - 1);
                            Console.Write(" ");
                            calculateScore(ref Game.counters.scores);
                        }
                        if (gameBoard[i - 1, j] == '%')
                        {
                            if(Game.health.Enemy1>0)
                            Game.health.Enemy1-=5;
                            calculateScore(ref Game.counters.scores);
                        }
                        if (gameBoard[i - 1, j] == '@')
                        {
                            if(Game.health.Enemy2>0)
                            Game.health.Enemy2-=5;
                            calculateScore(ref Game.counters.scores);
                        }
                        if (gameBoard[i - 1, j] == '!')
                        {
                            if(Game.health.Enemy3>0)
                            Game.health.Enemy3-=5;
                            calculateScore(ref Game.counters.scores);
                        }
                    }
                }
            }
        }
        static void moveBulletDown(ref game Game)
        {
            for (int i = 29; i > 0; i--)
            {

                for (int j = 99; j > 0; j--)
                {
                    if (gameBoard[i, j] == '*')
                    {
                        gameBoard[i, j] = ' ';
                        Console.SetCursorPosition(j, i);
                        Console.Write(" ");
                        if (gameBoard[i + 1, j] == ' ')
                        {

                            gameBoard[i + 1, j] = '*';
                            Console.SetCursorPosition(j, i + 1);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("*");
                            Console.ResetColor();
                        }
                        if (gameBoard[i + 1, j] == '+')
                        {
                            gameOver(ref Game);
                        }
                    }
                }
            }
        }
        static void bullet(ref game Game)
        {

            gameBoard[Game.boss.X - 3, Game.boss.Y-3] = gameBoard[Game.boss.X - 3, Game.boss.Y ]= gameBoard[Game.boss.X - 3, Game.boss.Y +3] ='^';
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(Game.boss.Y-3, Game.boss.X - 3);
            Console.Write("^");
            Console.SetCursorPosition(Game.boss.Y, Game.boss.X - 3);
            Console.Write("^");
            Console.SetCursorPosition(Game.boss.Y+3, Game.boss.X - 3);
            Console.Write("^");
            Console.ResetColor();
        }
        static void moveRight(ref game Game)
        {
            int x=Game.boss.X;
            int y=Game.boss.Y;
            if (Game.boss.Y + 8 < 100)
            {
                Console.SetCursorPosition(y - 3, x);
                Console.Write("       ");
                gameBoard[x, y - 3] = gameBoard[x, y - 1] = gameBoard[x, y + 1] = ' ';
                Console.SetCursorPosition(y - 5, x - 1);
                Console.Write("           ");
                gameBoard[x - 1, y - 5] = gameBoard[x - 1, y + 5] = ' ';
                gameBoard[x - 1, y - 4] = gameBoard[x - 1, y - 3] = gameBoard[x - 1, y - 2] = gameBoard[x - 1, y - 1] = gameBoard[x - 1, y] = gameBoard[x - 1, y + 1] = gameBoard[x - 1, y + 2] = gameBoard[x - 1, y + 3] = gameBoard[x - 1, y + 4] = ' ';
                Console.SetCursorPosition(y - 3, x - 2);
                Console.Write("       ");
                gameBoard[x - 2, y - 3] = gameBoard[x - 2, y - 1] = gameBoard[x - 2, y + 1] = ' ';
                Game.boss.Y++;
                y++;
                Console.SetCursorPosition(y - 3, x);
                Console.Write("o  o  o");
                gameBoard[x, y - 3] = gameBoard[x, y - 1] = gameBoard[x, y + 1] = '+';
                Console.SetCursorPosition(y - 5, x - 1);
                Console.Write("<--------->");
                gameBoard[x - 1, y - 5] = gameBoard[x - 1, y + 5] = '&';
                gameBoard[x - 1, y - 4] = gameBoard[x - 1, y - 3] = gameBoard[x - 1, y - 2] = gameBoard[x - 1, y - 1] = gameBoard[x - 1, y] = gameBoard[x - 1, y + 1] = gameBoard[x - 1, y + 2] = gameBoard[x - 1, y + 3] = gameBoard[x - 1, y + 4] = '+';
                Console.SetCursorPosition(y - 3, x - 2);
                Console.Write("!  !  !");
                gameBoard[x - 2, y - 3] = gameBoard[x - 2, y - 1] = gameBoard[x - 2, y + 1] = '+';
            }/*
            if (gameBoard[Game.boss.X, Game.boss.Y + 1] == ' ' || gameBoard[Game.boss.X, Game.boss.Y + 1] == '*')
            {

                gameBoard[Game.boss.X, Game.boss.Y] = ' ';
                Console.SetCursorPosition(Game.boss.Y, Game.boss.X);
                Console.Write(" ");
                gameBoard[Game.boss.X, Game.boss.Y] = 'W';
                Console.SetCursorPosition(Game.boss.Y, Game.boss.X);
                Console.Write("W");
            }*/
        }
        static void moveLeft(ref game Game)
        {
            int x = Game.boss.X;
            int y = Game.boss.Y;
            if (Game.boss.Y -6 > 0)
            {
                Console.SetCursorPosition(y - 3, x);
                Console.Write("       ");
                gameBoard[x, y - 3] = gameBoard[x, y - 1] = gameBoard[x, y + 1] = ' ';
                Console.SetCursorPosition(y - 5, x - 1);
                Console.Write("           ");
                gameBoard[x - 1, y - 5] = gameBoard[x - 1, y + 5] = ' ';
                gameBoard[x - 1, y - 4] = gameBoard[x - 1, y - 3] = gameBoard[x - 1, y - 2] = gameBoard[x - 1, y - 1] = gameBoard[x - 1, y] = gameBoard[x - 1, y + 1] = gameBoard[x - 1, y + 2] = gameBoard[x - 1, y + 3] = gameBoard[x - 1, y + 4] = ' ';
                Console.SetCursorPosition(y - 3, x - 2);
                Console.Write("       ");
                gameBoard[x - 2, y - 3] = gameBoard[x - 2, y - 1] = gameBoard[x - 2, y + 1] = ' ';
                Game.boss.Y--;
                y--;
                Console.SetCursorPosition(y - 3, x);
                Console.Write("o  o  o");
                gameBoard[x, y - 3] = gameBoard[x, y - 1] = gameBoard[x, y + 1] = '+';
                Console.SetCursorPosition(y - 5, x - 1);
                Console.Write("<--------->");
                gameBoard[x - 1, y - 5] = gameBoard[x - 1, y + 5] = '&';
                gameBoard[x - 1, y - 4] = gameBoard[x - 1, y - 3] = gameBoard[x - 1, y - 2] = gameBoard[x - 1, y - 1] = gameBoard[x - 1, y] = gameBoard[x - 1, y + 1] = gameBoard[x - 1, y + 2] = gameBoard[x - 1, y + 3] = gameBoard[x - 1, y + 4] = '+';
                Console.SetCursorPosition(y - 3, x - 2);
                Console.Write("!  !  !");
                gameBoard[x - 2, y - 3] = gameBoard[x - 2, y - 1] = gameBoard[x - 2, y + 1] = '+';
            }/*
            if (gameBoard[Game.boss.X, Game.boss.Y - 1] == ' ' || gameBoard[Game.boss.X, Game.boss.Y - 1] == '*')
            {
                gameBoard[Game.boss.X, Game.boss.Y] = ' ';
                Console.SetCursorPosition(Game.boss.Y, Game.boss.X);
                Console.Write(" ");
                Game.boss.Y--;
                gameBoard[Game.boss.X, Game.boss.Y] = 'W';
                Console.SetCursorPosition(Game.boss.Y, Game.boss.X);
                Console.Write("W");
            }*/
        }
        static string menu()
        {
            header();

            Console.Write("\n");
            Console.Write("OPTIONS:");
            Console.Write("\n");
            Console.Write("1.Play Game from Start.");
            Console.Write("\n");
            Console.Write("2.Play Last Game.");
            Console.Write("\n");
            Console.Write("3.View High Scores.");
            Console.Write("\n");
            Console.Write("4.Exit.");
            Console.Write("\n");
            Console.Write("Choose Any option ...");
            string option;
            //  option = Console.ReadKey().KeyChar;
            option = Console.ReadLine();

            return option;
        }
        static void header()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Write("*************************************");

            Console.Write("\n");
            Console.Write("*");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("            Pro Shooter            ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.BackgroundColor = ConsoleColor.DarkGreen;

            Console.Write("*");
            Console.Write("\n");
            Console.Write("*************************************");
            Console.Write("\n");
            Console.Write("\n");

            Console.ResetColor();
        }
        static void calculateScore(ref int score)
        {
            score = score + 5;
        }
        static void printScore(ref game Game)
        {

            Console.SetCursorPosition(Game.cursor.X, Game.cursor.Y);
            Console.Write("Level No. ");
            Console.Write(Game.counters.level);
            Game.cursor.Y++;
            Console.SetCursorPosition(Game.cursor.X, Game.cursor.Y);
            Console.Write("Scores : ");
            Console.Write(Game.counters.scores);
            Game.cursor.Y++;
            Console.SetCursorPosition(Game.cursor.X, Game.cursor.Y);
            Console.Write("Boss Health : " + Game.health.boss + " ");
            Game.cursor.Y++;
            Console.SetCursorPosition(Game.cursor.X, Game.cursor.Y);
            Console.Write("Enemy 1 Health : " + Game.health.Enemy1 + " ");
            Game.cursor.Y++;
            Console.SetCursorPosition(Game.cursor.X, Game.cursor.Y);
            Console.Write("Enemy 2 Health : " + Game.health.Enemy2 + " ");
            Game.cursor.Y++;
            Console.SetCursorPosition(Game.cursor.X, Game.cursor.Y);
            Console.Write("Enemy 3 Health : " + Game.health.Enemy3 + " ");
            Game.cursor.Y++;

            levelCompleted(ref Game);

            Game.cursor.X = 110;
            Game.cursor.Y = 1;
        }
        static void gameOver(ref game Game)
        {
            if (Game.health.boss > 0)
            {
                Game.health.boss-=4;
            }

            if (Game.health.boss == 0)
            {
                Game.health.over = true;
            }
        }
        static void printEnemy1(ref int enemyX, ref int enemyY, int health)
        {
            if (health > 0)
            {
                Console.SetCursorPosition(enemyY, enemyX);
                Console.Write("_____");
                gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = '_';
                Console.SetCursorPosition(enemyY - 1, enemyX + 1);
                Console.Write("/  *  \\");
                gameBoard[enemyX + 1, enemyY - 1] = '/';
                gameBoard[enemyX + 1, enemyY + 2] = '*';
                gameBoard[enemyX + 1, enemyY + 5] = '/';
                Console.SetCursorPosition(enemyY - 2, enemyX + 2);
                Console.Write("|___o___|");
                gameBoard[enemyX + 2, enemyY - 2] = '|';
                gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = '_';
                gameBoard[enemyX + 2, enemyY + 2] = 'o';
                gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = '_';
                gameBoard[enemyX + 2, enemyY + 6] = '|';
                Console.SetCursorPosition(enemyY - 2, enemyX + 3);
                Console.Write("\\       /");
                gameBoard[enemyX + 3, enemyY - 2] = '/';
                gameBoard[enemyX + 3, enemyY + 6] = '/';
                Console.SetCursorPosition(enemyY - 1, enemyX + 4);
                Console.Write("\\(---)/");
                gameBoard[enemyX + 4, enemyY - 1] = '/';
                gameBoard[enemyX + 4, enemyY] = '(';
                gameBoard[enemyX + 4, enemyY + 1] = '-';
                gameBoard[enemyX + 4, enemyY + 2] = '-';
                gameBoard[enemyX + 4, enemyY + 3] = '-';
                gameBoard[enemyX + 4, enemyY + 4] = ')';
                gameBoard[enemyX + 4, enemyY + 5] = '/';
                Console.SetCursorPosition(enemyY, enemyX + 5);
                Console.Write("|   |");
                gameBoard[enemyX + 5, enemyY] = '%';
                gameBoard[enemyX + 5, enemyY + 4] = '%';
                Console.SetCursorPosition(enemyY + 1, enemyX + 6);
                Console.Write("---");
                gameBoard[enemyX + 6, enemyY + 1] = '%';
                gameBoard[enemyX + 6, enemyY + 2] = '%';
                gameBoard[enemyX + 6, enemyY + 3] = '%';
                Console.SetCursorPosition(enemyY + 2, enemyX + 7);
                Console.Write("|");
                gameBoard[enemyX + 7, enemyY + 2] = '%';
            }
        }
        static void moveEnemy1(ref int enemyX, ref int enemyY, ref int enemyMove)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            if (enemyMove > 0)
            {
                moveEnemyLeft1(ref enemyX, ref enemyY);
                enemyMove--;
                if (enemyMove == 0)
                {
                    enemyMove = -26;
                }
            }
            if (enemyMove <= 0)
            {
                moveEnemyRight1(ref enemyX, ref enemyY);
                enemyMove++;
                if (enemyMove == 0)
                {
                    enemyMove = 26;
                }
            }
            Console.ResetColor();
        }
        static void moveEnemyLeft1(ref int enemyX, ref int enemyY)
        {
            Console.SetCursorPosition(enemyY, enemyX);
            Console.Write("     ");
            gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = ' ';
            Console.SetCursorPosition(enemyY - 1, enemyX + 1);
            Console.Write("     ");
            gameBoard[enemyX + 1, enemyY - 1] = ' ';
            gameBoard[enemyX + 1, enemyY + 2] = '*';
            gameBoard[enemyX + 1, enemyY + 5] = ' ';
            Console.SetCursorPosition(enemyY - 2, enemyX + 2);
            Console.Write("         ");
            gameBoard[enemyX + 2, enemyY - 2] = ' ';
            gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = ' ';
            gameBoard[enemyX + 2, enemyY + 2] = ' ';
            gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = '_';
            gameBoard[enemyX + 2, enemyY + 6] = ' ';
            Console.SetCursorPosition(enemyY - 2, enemyX + 3);
            Console.Write("         ");
            gameBoard[enemyX + 3, enemyY - 2] = ' ';
            gameBoard[enemyX + 3, enemyY + 6] = ' ';
            Console.SetCursorPosition(enemyY - 1, enemyX + 4);
            Console.Write("        ");
            gameBoard[enemyX + 4, enemyY - 1] = ' ';
            gameBoard[enemyX + 4, enemyY] = ' ';
            gameBoard[enemyX + 4, enemyY + 1] = ' ';
            gameBoard[enemyX + 4, enemyY + 2] = ' ';
            gameBoard[enemyX + 4, enemyY + 3] = ' ';
            gameBoard[enemyX + 4, enemyY + 4] = ' ';
            gameBoard[enemyX + 4, enemyY + 5] = ' ';
            Console.SetCursorPosition(enemyY, enemyX + 5);
            Console.Write("     ");
            gameBoard[enemyX + 5, enemyY] = ' ';
            gameBoard[enemyX + 5, enemyY + 4] = ' ';
            Console.SetCursorPosition(enemyY + 1, enemyX + 6);
            Console.Write("   ");
            gameBoard[enemyX + 6, enemyY + 1] = ' ';
            gameBoard[enemyX + 6, enemyY + 2] = ' ';
            gameBoard[enemyX + 6, enemyY + 3] = ' ';
            Console.SetCursorPosition(enemyY + 2, enemyX + 7);
            Console.Write(" ");
            gameBoard[enemyX + 7, enemyY + 2] = ' ';
            enemyY--;
            Console.SetCursorPosition(enemyY, enemyX);
            Console.Write("_____");
            gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = '_';
            Console.SetCursorPosition(enemyY - 1, enemyX + 1);
            Console.Write("/  *  \\ ");
            gameBoard[enemyX + 1, enemyY - 1] = '/';
            gameBoard[enemyX + 1, enemyY + 2] = '*';
            gameBoard[enemyX + 1, enemyY + 5] = '/';
            Console.SetCursorPosition(enemyY - 2, enemyX + 2);
            Console.Write("|___o___|");
            gameBoard[enemyX + 2, enemyY - 2] = '|';
            gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = '_';
            gameBoard[enemyX + 2, enemyY + 2] = 'o';
            gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = '_';
            gameBoard[enemyX + 2, enemyY + 6] = '|';
            Console.SetCursorPosition(enemyY - 2, enemyX + 3);
            Console.Write("\\       /");
            gameBoard[enemyX + 3, enemyY - 2] = '/';
            gameBoard[enemyX + 3, enemyY + 6] = '/';
            Console.SetCursorPosition(enemyY - 1, enemyX + 4);
            Console.Write("\\(---)/");
            gameBoard[enemyX + 4, enemyY - 1] = '/';
            gameBoard[enemyX + 4, enemyY] = '(';
            gameBoard[enemyX + 4, enemyY + 1] = '-';
            gameBoard[enemyX + 4, enemyY + 2] = '-';
            gameBoard[enemyX + 4, enemyY + 3] = '-';
            gameBoard[enemyX + 4, enemyY + 4] = ')';
            gameBoard[enemyX + 4, enemyY + 5] = '/';
            Console.SetCursorPosition(enemyY, enemyX + 5);
            Console.Write("|   |");
            gameBoard[enemyX + 5, enemyY] = '%';
            gameBoard[enemyX + 5, enemyY + 4] = '%';
            Console.SetCursorPosition(enemyY + 1, enemyX + 6);
            Console.Write("---");
            gameBoard[enemyX + 6, enemyY + 1] = '%';
            gameBoard[enemyX + 6, enemyY + 2] = '%';
            gameBoard[enemyX + 6, enemyY + 3] = '%';
            Console.SetCursorPosition(enemyY + 2, enemyX + 7);
            Console.Write("|");
            gameBoard[enemyX + 7, enemyY + 2] = '%';
        }
        static void moveEnemyRight1(ref int enemyX, ref int enemyY)
        {

            Console.SetCursorPosition(enemyY, enemyX);
            Console.Write("     ");
            gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = ' ';
            Console.SetCursorPosition(enemyY - 1, enemyX + 1);
            Console.Write("     ");
            gameBoard[enemyX + 1, enemyY - 1] = ' ';
            gameBoard[enemyX + 1, enemyY + 2] = '*';
            gameBoard[enemyX + 1, enemyY + 5] = ' ';
            Console.SetCursorPosition(enemyY - 2, enemyX + 2);
            Console.Write("         ");
            gameBoard[enemyX + 2, enemyY - 2] = ' ';
            gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = ' ';
            gameBoard[enemyX + 2, enemyY + 2] = ' ';
            gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = '_';
            gameBoard[enemyX + 2, enemyY + 6] = ' ';
            Console.SetCursorPosition(enemyY - 2, enemyX + 3);
            Console.Write("         ");
            gameBoard[enemyX + 3, enemyY - 2] = ' ';
            gameBoard[enemyX + 3, enemyY + 6] = ' ';
            Console.SetCursorPosition(enemyY - 1, enemyX + 4);
            Console.Write("        ");
            gameBoard[enemyX + 4, enemyY - 1] = ' ';
            gameBoard[enemyX + 4, enemyY] = ' ';
            gameBoard[enemyX + 4, enemyY + 1] = ' ';
            gameBoard[enemyX + 4, enemyY + 2] = ' ';
            gameBoard[enemyX + 4, enemyY + 3] = ' ';
            gameBoard[enemyX + 4, enemyY + 4] = ' ';
            gameBoard[enemyX + 4, enemyY + 5] = ' ';
            Console.SetCursorPosition(enemyY, enemyX + 5);
            Console.Write("     ");
            gameBoard[enemyX + 5, enemyY] = ' ';
            gameBoard[enemyX + 5, enemyY + 4] = ' ';
            Console.SetCursorPosition(enemyY + 1, enemyX + 6);
            Console.Write("   ");
            gameBoard[enemyX + 6, enemyY + 1] = ' ';
            gameBoard[enemyX + 6, enemyY + 2] = ' ';
            gameBoard[enemyX + 6, enemyY + 3] = ' ';
            Console.SetCursorPosition(enemyY + 2, enemyX + 7);
            Console.Write(" ");
            gameBoard[enemyX + 7, enemyY + 2] = ' ';
            enemyY++;
            Console.SetCursorPosition(enemyY, enemyX);
            Console.Write("_____");
            gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = '_';
            Console.SetCursorPosition(enemyY - 1, enemyX + 1);
            Console.Write("/  *  \\");
            gameBoard[enemyX + 1, enemyY - 1] = '/';
            gameBoard[enemyX + 1, enemyY + 2] = '*';
            gameBoard[enemyX + 1, enemyY + 5] = '/';
            Console.SetCursorPosition(enemyY - 2, enemyX + 2);
            Console.Write("|___o___|");
            gameBoard[enemyX + 2, enemyY - 2] = '|';
            gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = '_';
            gameBoard[enemyX + 2, enemyY + 2] = 'o';
            gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = '_';
            gameBoard[enemyX + 2, enemyY + 6] = '|';
            Console.SetCursorPosition(enemyY - 2, enemyX + 3);
            Console.Write("\\       /");
            gameBoard[enemyX + 3, enemyY - 2] = '/';
            gameBoard[enemyX + 3, enemyY + 6] = '/';
            Console.SetCursorPosition(enemyY - 1, enemyX + 4);
            Console.Write("\\(---)/");
            gameBoard[enemyX + 4, enemyY - 1] = '/';
            gameBoard[enemyX + 4, enemyY] = '(';
            gameBoard[enemyX + 4, enemyY + 1] = '-';
            gameBoard[enemyX + 4, enemyY + 2] = '-';
            gameBoard[enemyX + 4, enemyY + 3] = '-';
            gameBoard[enemyX + 4, enemyY + 4] = ')';
            gameBoard[enemyX + 4, enemyY + 5] = '/';
            Console.SetCursorPosition(enemyY, enemyX + 5);
            Console.Write("|   |");
            gameBoard[enemyX + 5, enemyY] = '%';
            gameBoard[enemyX + 5, enemyY + 4] = '%';
            Console.SetCursorPosition(enemyY + 1, enemyX + 6);
            Console.Write("---");
            gameBoard[enemyX + 6, enemyY + 1] = '%';
            gameBoard[enemyX + 6, enemyY + 2] = '%';
            gameBoard[enemyX + 6, enemyY + 3] = '%';
            Console.SetCursorPosition(enemyY + 2, enemyX + 7);
            Console.Write("|");
            gameBoard[enemyX + 7, enemyY + 2] = '%';
        }
        static void printEnemy2(ref int enemyX, ref int enemyY, int health)
        {
            if (health > 0)
            {
                Console.SetCursorPosition(enemyY, enemyX);
                Console.Write("_____");
                gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = '_';
                Console.SetCursorPosition(enemyY - 1, enemyX + 1);
                Console.Write("/  *  \\");
                gameBoard[enemyX + 1, enemyY - 1] = '/';
                gameBoard[enemyX + 1, enemyY + 2] = '*';
                gameBoard[enemyX + 1, enemyY + 5] = '/';
                Console.SetCursorPosition(enemyY - 2, enemyX + 2);
                Console.Write("|___o___|");
                gameBoard[enemyX + 2, enemyY - 2] = '|';
                gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = '_';
                gameBoard[enemyX + 2, enemyY + 2] = 'o';
                gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = '_';
                gameBoard[enemyX + 2, enemyY + 6] = '|';
                Console.SetCursorPosition(enemyY - 2, enemyX + 3);
                Console.Write("\\       /");
                gameBoard[enemyX + 3, enemyY - 2] = '/';
                gameBoard[enemyX + 3, enemyY + 6] = '/';
                Console.SetCursorPosition(enemyY - 1, enemyX + 4);
                Console.Write("\\(---)/");
                gameBoard[enemyX + 4, enemyY - 1] = '/';
                gameBoard[enemyX + 4, enemyY] = '(';
                gameBoard[enemyX + 4, enemyY + 1] = '-';
                gameBoard[enemyX + 4, enemyY + 2] = '-';
                gameBoard[enemyX + 4, enemyY + 3] = '-';
                gameBoard[enemyX + 4, enemyY + 4] = ')';
                gameBoard[enemyX + 4, enemyY + 5] = '/';
                Console.SetCursorPosition(enemyY, enemyX + 5);
                Console.Write("|   |");
                gameBoard[enemyX + 5, enemyY] = '@';
                gameBoard[enemyX + 5, enemyY + 4] = '@';
                Console.SetCursorPosition(enemyY + 1, enemyX + 6);
                Console.Write("---");
                gameBoard[enemyX + 6, enemyY + 1] = '@';
                gameBoard[enemyX + 6, enemyY + 2] = '@';
                gameBoard[enemyX + 6, enemyY + 3] = '@';
                Console.SetCursorPosition(enemyY + 2, enemyX + 7);
                Console.Write("|");
                gameBoard[enemyX + 7, enemyY + 2] = '@';
            }
        }
        static void moveEnemy2(ref int enemyX, ref int enemyY, ref int enemyMove)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            if (enemyMove > 0)
            {
                enemyMove--;
                moveEnemyRight2(ref enemyX, ref enemyY);
                if (enemyMove == 0)
                {
                    enemyMove = -30;
                }
            }
            if (enemyMove <= 0)
            {
                moveEnemyLeft2(ref enemyX, ref enemyY);
                enemyMove++;
                if (enemyMove == 0)
                {
                    enemyMove = 30;
                }
            }
            Console.ResetColor();
        }
        static void moveEnemyLeft2(ref int enemyX, ref int enemyY)
        {
            Console.SetCursorPosition(enemyY, enemyX);
            Console.Write("     ");
            gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = ' ';
            Console.SetCursorPosition(enemyY - 1, enemyX + 1);
            Console.Write("     ");
            gameBoard[enemyX + 1, enemyY - 1] = ' ';
            gameBoard[enemyX + 1, enemyY + 2] = '*';
            gameBoard[enemyX + 1, enemyY + 5] = ' ';
            Console.SetCursorPosition(enemyY - 2, enemyX + 2);
            Console.Write("         ");
            gameBoard[enemyX + 2, enemyY - 2] = ' ';
            gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = ' ';
            gameBoard[enemyX + 2, enemyY + 2] = ' ';
            gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = '_';
            gameBoard[enemyX + 2, enemyY + 6] = ' ';
            Console.SetCursorPosition(enemyY - 2, enemyX + 3);
            Console.Write("         ");
            gameBoard[enemyX + 3, enemyY - 2] = ' ';
            gameBoard[enemyX + 3, enemyY + 6] = ' ';
            Console.SetCursorPosition(enemyY - 1, enemyX + 4);
            Console.Write("        ");
            gameBoard[enemyX + 4, enemyY - 1] = ' ';
            gameBoard[enemyX + 4, enemyY] = ' ';
            gameBoard[enemyX + 4, enemyY + 1] = ' ';
            gameBoard[enemyX + 4, enemyY + 2] = ' ';
            gameBoard[enemyX + 4, enemyY + 3] = ' ';
            gameBoard[enemyX + 4, enemyY + 4] = ' ';
            gameBoard[enemyX + 4, enemyY + 5] = ' ';
            Console.SetCursorPosition(enemyY, enemyX + 5);
            Console.Write("     ");
            gameBoard[enemyX + 5, enemyY] = ' ';
            gameBoard[enemyX + 5, enemyY + 4] = ' ';
            Console.SetCursorPosition(enemyY + 1, enemyX + 6);
            Console.Write("   ");
            gameBoard[enemyX + 6, enemyY + 1] = ' ';
            gameBoard[enemyX + 6, enemyY + 2] = ' ';
            gameBoard[enemyX + 6, enemyY + 3] = ' ';
            Console.SetCursorPosition(enemyY + 2, enemyX + 7);
            Console.Write(" ");
            gameBoard[enemyX + 7, enemyY + 2] = ' ';
            enemyY--;
            Console.SetCursorPosition(enemyY, enemyX);
            Console.Write("_____");
            gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = '_';
            Console.SetCursorPosition(enemyY - 1, enemyX + 1);
            Console.Write("/  *  \\ ");
            gameBoard[enemyX + 1, enemyY - 1] = '/';
            gameBoard[enemyX + 1, enemyY + 2] = '*';
            gameBoard[enemyX + 1, enemyY + 5] = '/';
            Console.SetCursorPosition(enemyY - 2, enemyX + 2);
            Console.Write("|___o___|");
            gameBoard[enemyX + 2, enemyY - 2] = '|';
            gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = '_';
            gameBoard[enemyX + 2, enemyY + 2] = 'o';
            gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = '_';
            gameBoard[enemyX + 2, enemyY + 6] = '|';
            Console.SetCursorPosition(enemyY - 2, enemyX + 3);
            Console.Write("\\       /");
            gameBoard[enemyX + 3, enemyY - 2] = '/';
            gameBoard[enemyX + 3, enemyY + 6] = '/';
            Console.SetCursorPosition(enemyY - 1, enemyX + 4);
            Console.Write("\\(---)/");
            gameBoard[enemyX + 4, enemyY - 1] = '/';
            gameBoard[enemyX + 4, enemyY] = '(';
            gameBoard[enemyX + 4, enemyY + 1] = '-';
            gameBoard[enemyX + 4, enemyY + 2] = '-';
            gameBoard[enemyX + 4, enemyY + 3] = '-';
            gameBoard[enemyX + 4, enemyY + 4] = ')';
            gameBoard[enemyX + 4, enemyY + 5] = '/';
            Console.SetCursorPosition(enemyY, enemyX + 5);
            Console.Write("|   |");
            gameBoard[enemyX + 5, enemyY] = '@';
            gameBoard[enemyX + 5, enemyY + 4] = '@';
            Console.SetCursorPosition(enemyY + 1, enemyX + 6);
            Console.Write("---");
            gameBoard[enemyX + 6, enemyY + 1] = '@';
            gameBoard[enemyX + 6, enemyY + 2] = '@';
            gameBoard[enemyX + 6, enemyY + 3] = '@';
            Console.SetCursorPosition(enemyY + 2, enemyX + 7);
            Console.Write("|");
            gameBoard[enemyX + 7, enemyY + 2] = '@';
        }
        static void moveEnemyRight2(ref int enemyX, ref int enemyY)
        {

            Console.SetCursorPosition(enemyY, enemyX);
            Console.Write("     ");
            gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = ' ';
            Console.SetCursorPosition(enemyY - 1, enemyX + 1);
            Console.Write("     ");
            gameBoard[enemyX + 1, enemyY - 1] = ' ';
            gameBoard[enemyX + 1, enemyY + 2] = '*';
            gameBoard[enemyX + 1, enemyY + 5] = ' ';
            Console.SetCursorPosition(enemyY - 2, enemyX + 2);
            Console.Write("         ");
            gameBoard[enemyX + 2, enemyY - 2] = ' ';
            gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = ' ';
            gameBoard[enemyX + 2, enemyY + 2] = ' ';
            gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = '_';
            gameBoard[enemyX + 2, enemyY + 6] = ' ';
            Console.SetCursorPosition(enemyY - 2, enemyX + 3);
            Console.Write("         ");
            gameBoard[enemyX + 3, enemyY - 2] = ' ';
            gameBoard[enemyX + 3, enemyY + 6] = ' ';
            Console.SetCursorPosition(enemyY - 1, enemyX + 4);
            Console.Write("        ");
            gameBoard[enemyX + 4, enemyY - 1] = ' ';
            gameBoard[enemyX + 4, enemyY] = ' ';
            gameBoard[enemyX + 4, enemyY + 1] = ' ';
            gameBoard[enemyX + 4, enemyY + 2] = ' ';
            gameBoard[enemyX + 4, enemyY + 3] = ' ';
            gameBoard[enemyX + 4, enemyY + 4] = ' ';
            gameBoard[enemyX + 4, enemyY + 5] = ' ';
            Console.SetCursorPosition(enemyY, enemyX + 5);
            Console.Write("     ");
            gameBoard[enemyX + 5, enemyY] = ' ';
            gameBoard[enemyX + 5, enemyY + 4] = ' ';
            Console.SetCursorPosition(enemyY + 1, enemyX + 6);
            Console.Write("   ");
            gameBoard[enemyX + 6, enemyY + 1] = ' ';
            gameBoard[enemyX + 6, enemyY + 2] = ' ';
            gameBoard[enemyX + 6, enemyY + 3] = ' ';
            Console.SetCursorPosition(enemyY + 2, enemyX + 7);
            Console.Write(" ");
            gameBoard[enemyX + 7, enemyY + 2] = ' ';
            enemyY++;
            Console.SetCursorPosition(enemyY, enemyX);
            Console.Write("_____");
            gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = '_';
            Console.SetCursorPosition(enemyY - 1, enemyX + 1);
            Console.Write("/  *  \\");
            gameBoard[enemyX + 1, enemyY - 1] = '/';
            gameBoard[enemyX + 1, enemyY + 2] = '*';
            gameBoard[enemyX + 1, enemyY + 5] = '/';
            Console.SetCursorPosition(enemyY - 2, enemyX + 2);
            Console.Write("|___o___|");
            gameBoard[enemyX + 2, enemyY - 2] = '|';
            gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = '_';
            gameBoard[enemyX + 2, enemyY + 2] = 'o';
            gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = '_';
            gameBoard[enemyX + 2, enemyY + 6] = '|';
            Console.SetCursorPosition(enemyY - 2, enemyX + 3);
            Console.Write("\\       /");
            gameBoard[enemyX + 3, enemyY - 2] = '/';
            gameBoard[enemyX + 3, enemyY + 6] = '/';
            Console.SetCursorPosition(enemyY - 1, enemyX + 4);
            Console.Write("\\(---)/");
            gameBoard[enemyX + 4, enemyY - 1] = '/';
            gameBoard[enemyX + 4, enemyY] = '(';
            gameBoard[enemyX + 4, enemyY + 1] = '-';
            gameBoard[enemyX + 4, enemyY + 2] = '-';
            gameBoard[enemyX + 4, enemyY + 3] = '-';
            gameBoard[enemyX + 4, enemyY + 4] = ')';
            gameBoard[enemyX + 4, enemyY + 5] = '/';
            Console.SetCursorPosition(enemyY, enemyX + 5);
            Console.Write("|   |");
            gameBoard[enemyX + 5, enemyY] = '@';
            gameBoard[enemyX + 5, enemyY + 4] = '@';
            Console.SetCursorPosition(enemyY + 1, enemyX + 6);
            Console.Write("---");
            gameBoard[enemyX + 6, enemyY + 1] = '@';
            gameBoard[enemyX + 6, enemyY + 2] = '@';
            gameBoard[enemyX + 6, enemyY + 3] = '@';
            Console.SetCursorPosition(enemyY + 2, enemyX + 7);
            Console.Write("|");
            gameBoard[enemyX + 7, enemyY + 2] = '@';
        }
        static void printEnemy3(ref int enemyX, ref int enemyY, int health)
        {
            if (health > 0)
            {
                Console.SetCursorPosition(enemyY, enemyX);
                Console.Write("_____");
                gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = '_';
                Console.SetCursorPosition(enemyY - 1, enemyX + 1);
                Console.Write("/  *  \\");
                gameBoard[enemyX + 1, enemyY - 1] = '/';
                gameBoard[enemyX + 1, enemyY + 2] = '*';
                gameBoard[enemyX + 1, enemyY + 5] = '/';
                Console.SetCursorPosition(enemyY - 2, enemyX + 2);
                Console.Write("|___o___|");
                gameBoard[enemyX + 2, enemyY - 2] = '|';
                gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = '_';
                gameBoard[enemyX + 2, enemyY + 2] = 'o';
                gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = '_';
                gameBoard[enemyX + 2, enemyY + 6] = '|';
                Console.SetCursorPosition(enemyY - 2, enemyX + 3);
                Console.Write("\\       /");
                gameBoard[enemyX + 3, enemyY - 2] = '/';
                gameBoard[enemyX + 3, enemyY + 6] = '/';
                Console.SetCursorPosition(enemyY - 1, enemyX + 4);
                Console.Write("\\(---)/");
                gameBoard[enemyX + 4, enemyY - 1] = '/';
                gameBoard[enemyX + 4, enemyY] = '(';
                gameBoard[enemyX + 4, enemyY + 1] = '-';
                gameBoard[enemyX + 4, enemyY + 2] = '-';
                gameBoard[enemyX + 4, enemyY + 3] = '-';
                gameBoard[enemyX + 4, enemyY + 4] = ')';
                gameBoard[enemyX + 4, enemyY + 5] = '/';
                Console.SetCursorPosition(enemyY, enemyX + 5);
                Console.Write("|   |");
                gameBoard[enemyX + 5, enemyY] = '!';
                gameBoard[enemyX + 5, enemyY + 4] = '!';
                Console.SetCursorPosition(enemyY + 1, enemyX + 6);
                Console.Write("---");
                gameBoard[enemyX + 6, enemyY + 1] = '!';
                gameBoard[enemyX + 6, enemyY + 2] = '!';
                gameBoard[enemyX + 6, enemyY + 3] = '!';
                Console.SetCursorPosition(enemyY + 2, enemyX + 7);
                Console.Write("|");
                gameBoard[enemyX + 7, enemyY + 2] = '!';
            }
        }
        static void moveEnemy3(ref int enemyX, ref int enemyY, ref int enemyMove)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;

            if (enemyMove > 0)
            {
                moveEnemyLeft3(ref enemyX, ref enemyY);
                enemyMove--;
                if (enemyMove == 0)
                {
                    enemyMove = -26;
                }
            }
            if (enemyMove <= 0)
            {
                moveEnemyRight3(ref enemyX, ref enemyY);
                enemyMove++;
                if (enemyMove == 0)
                {
                    enemyMove = 26;
                }
            }
            Console.ResetColor();
        }
        static void moveEnemyLeft3(ref int enemyX, ref int enemyY)
        {
            Console.SetCursorPosition(enemyY, enemyX);
            Console.Write("     ");
            gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = ' ';
            Console.SetCursorPosition(enemyY - 1, enemyX + 1);
            Console.Write("     ");
            gameBoard[enemyX + 1, enemyY - 1] = ' ';
            gameBoard[enemyX + 1, enemyY + 2] = '*';
            gameBoard[enemyX + 1, enemyY + 5] = ' ';
            Console.SetCursorPosition(enemyY - 2, enemyX + 2);
            Console.Write("         ");
            gameBoard[enemyX + 2, enemyY - 2] = ' ';
            gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = ' ';
            gameBoard[enemyX + 2, enemyY + 2] = ' ';
            gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = '_';
            gameBoard[enemyX + 2, enemyY + 6] = ' ';
            Console.SetCursorPosition(enemyY - 2, enemyX + 3);
            Console.Write("         ");
            gameBoard[enemyX + 3, enemyY - 2] = ' ';
            gameBoard[enemyX + 3, enemyY + 6] = ' ';
            Console.SetCursorPosition(enemyY - 1, enemyX + 4);
            Console.Write("        ");
            gameBoard[enemyX + 4, enemyY - 1] = ' ';
            gameBoard[enemyX + 4, enemyY] = ' ';
            gameBoard[enemyX + 4, enemyY + 1] = ' ';
            gameBoard[enemyX + 4, enemyY + 2] = ' ';
            gameBoard[enemyX + 4, enemyY + 3] = ' ';
            gameBoard[enemyX + 4, enemyY + 4] = ' ';
            gameBoard[enemyX + 4, enemyY + 5] = ' ';
            Console.SetCursorPosition(enemyY, enemyX + 5);
            Console.Write("     ");
            gameBoard[enemyX + 5, enemyY] = ' ';
            gameBoard[enemyX + 5, enemyY + 4] = ' ';
            Console.SetCursorPosition(enemyY + 1, enemyX + 6);
            Console.Write("   ");
            gameBoard[enemyX + 6, enemyY + 1] = ' ';
            gameBoard[enemyX + 6, enemyY + 2] = ' ';
            gameBoard[enemyX + 6, enemyY + 3] = ' ';
            Console.SetCursorPosition(enemyY + 2, enemyX + 7);
            Console.Write(" ");
            gameBoard[enemyX + 7, enemyY + 2] = ' ';
            enemyY--;
            Console.SetCursorPosition(enemyY, enemyX);
            Console.Write("_____");
            gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = '_';
            Console.SetCursorPosition(enemyY - 1, enemyX + 1);
            Console.Write("/  *  \\ ");
            gameBoard[enemyX + 1, enemyY - 1] = '/';
            gameBoard[enemyX + 1, enemyY + 2] = '*';
            gameBoard[enemyX + 1, enemyY + 5] = '/';
            Console.SetCursorPosition(enemyY - 2, enemyX + 2);
            Console.Write("|___o___|");
            gameBoard[enemyX + 2, enemyY - 2] = '|';
            gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = '_';
            gameBoard[enemyX + 2, enemyY + 2] = 'o';
            gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = '_';
            gameBoard[enemyX + 2, enemyY + 6] = '|';
            Console.SetCursorPosition(enemyY - 2, enemyX + 3);
            Console.Write("\\       /");
            gameBoard[enemyX + 3, enemyY - 2] = '/';
            gameBoard[enemyX + 3, enemyY + 6] = '/';
            Console.SetCursorPosition(enemyY - 1, enemyX + 4);
            Console.Write("\\(---)/");
            gameBoard[enemyX + 4, enemyY - 1] = '/';
            gameBoard[enemyX + 4, enemyY] = '(';
            gameBoard[enemyX + 4, enemyY + 1] = '-';
            gameBoard[enemyX + 4, enemyY + 2] = '-';
            gameBoard[enemyX + 4, enemyY + 3] = '-';
            gameBoard[enemyX + 4, enemyY + 4] = ')';
            gameBoard[enemyX + 4, enemyY + 5] = '/';
            Console.SetCursorPosition(enemyY, enemyX + 5);
            Console.Write("|   |");
            gameBoard[enemyX + 5, enemyY] = '!';
            gameBoard[enemyX + 5, enemyY + 4] = '!';
            Console.SetCursorPosition(enemyY + 1, enemyX + 6);
            Console.Write("---");
            gameBoard[enemyX + 6, enemyY + 1] = '!';
            gameBoard[enemyX + 6, enemyY + 2] = '!';
            gameBoard[enemyX + 6, enemyY + 3] = '!';
            Console.SetCursorPosition(enemyY + 2, enemyX + 7);
            Console.Write("|");
            gameBoard[enemyX + 7, enemyY + 2] = '!';
        }
        static void moveEnemyRight3(ref int enemyX, ref int enemyY)
        {

            Console.SetCursorPosition(enemyY, enemyX);
            Console.Write("     ");
            gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = ' ';
            Console.SetCursorPosition(enemyY - 1, enemyX + 1);
            Console.Write("     ");
            gameBoard[enemyX + 1, enemyY - 1] = ' ';
            gameBoard[enemyX + 1, enemyY + 2] = '*';
            gameBoard[enemyX + 1, enemyY + 5] = ' ';
            Console.SetCursorPosition(enemyY - 2, enemyX + 2);
            Console.Write("         ");
            gameBoard[enemyX + 2, enemyY - 2] = ' ';
            gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = ' ';
            gameBoard[enemyX + 2, enemyY + 2] = ' ';
            gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = '_';
            gameBoard[enemyX + 2, enemyY + 6] = ' ';
            Console.SetCursorPosition(enemyY - 2, enemyX + 3);
            Console.Write("         ");
            gameBoard[enemyX + 3, enemyY - 2] = ' ';
            gameBoard[enemyX + 3, enemyY + 6] = ' ';
            Console.SetCursorPosition(enemyY - 1, enemyX + 4);
            Console.Write("        ");
            gameBoard[enemyX + 4, enemyY - 1] = ' ';
            gameBoard[enemyX + 4, enemyY] = ' ';
            gameBoard[enemyX + 4, enemyY + 1] = ' ';
            gameBoard[enemyX + 4, enemyY + 2] = ' ';
            gameBoard[enemyX + 4, enemyY + 3] = ' ';
            gameBoard[enemyX + 4, enemyY + 4] = ' ';
            gameBoard[enemyX + 4, enemyY + 5] = ' ';
            Console.SetCursorPosition(enemyY, enemyX + 5);
            Console.Write("     ");
            gameBoard[enemyX + 5, enemyY] = ' ';
            gameBoard[enemyX + 5, enemyY + 4] = ' ';
            Console.SetCursorPosition(enemyY + 1, enemyX + 6);
            Console.Write("   ");
            gameBoard[enemyX + 6, enemyY + 1] = ' ';
            gameBoard[enemyX + 6, enemyY + 2] = ' ';
            gameBoard[enemyX + 6, enemyY + 3] = ' ';
            Console.SetCursorPosition(enemyY + 2, enemyX + 7);
            Console.Write(" ");
            gameBoard[enemyX + 7, enemyY + 2] = ' ';
            enemyY++;
            Console.SetCursorPosition(enemyY, enemyX);
            Console.Write("_____");
            gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = '_';
            Console.SetCursorPosition(enemyY - 1, enemyX + 1);
            Console.Write("/  *  \\");
            gameBoard[enemyX + 1, enemyY - 1] = '/';
            gameBoard[enemyX + 1, enemyY + 2] = '*';
            gameBoard[enemyX + 1, enemyY + 5] = '/';
            Console.SetCursorPosition(enemyY - 2, enemyX + 2);
            Console.Write("|___o___|");
            gameBoard[enemyX + 2, enemyY - 2] = '|';
            gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = '_';
            gameBoard[enemyX + 2, enemyY + 2] = 'o';
            gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = '_';
            gameBoard[enemyX + 2, enemyY + 6] = '|';
            Console.SetCursorPosition(enemyY - 2, enemyX + 3);
            Console.Write("\\       /");
            gameBoard[enemyX + 3, enemyY - 2] = '/';
            gameBoard[enemyX + 3, enemyY + 6] = '/';
            Console.SetCursorPosition(enemyY - 1, enemyX + 4);
            Console.Write("\\(---)/");
            gameBoard[enemyX + 4, enemyY - 1] = '/';
            gameBoard[enemyX + 4, enemyY] = '(';
            gameBoard[enemyX + 4, enemyY + 1] = '-';
            gameBoard[enemyX + 4, enemyY + 2] = '-';
            gameBoard[enemyX + 4, enemyY + 3] = '-';
            gameBoard[enemyX + 4, enemyY + 4] = ')';
            gameBoard[enemyX + 4, enemyY + 5] = '/';
            Console.SetCursorPosition(enemyY, enemyX + 5);
            Console.Write("|   |");
            gameBoard[enemyX + 5, enemyY] = '!';
            gameBoard[enemyX + 5, enemyY + 4] = '!';
            Console.SetCursorPosition(enemyY + 1, enemyX + 6);
            Console.Write("---");
            gameBoard[enemyX + 6, enemyY + 1] = '!';
            gameBoard[enemyX + 6, enemyY + 2] = '!';
            gameBoard[enemyX + 6, enemyY + 3] = '!';
            Console.SetCursorPosition(enemyY + 2, enemyX + 7);
            Console.Write("|");
            gameBoard[enemyX + 7, enemyY + 2] = '!';
        }
        static void creatEnemy(ref int enemyX, ref int enemyY)
        {
            gameBoard[enemyX + 8, enemyY + 2] = '*';
            Console.SetCursorPosition(enemyY + 2, enemyX + 8);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("*");
            Console.ResetColor();

        }
        static void levelCompleted(ref game Game)
        {
            if (completedLevel(ref Game.health))
            {


                Console.SetCursorPosition(Game.cursor.X, Game.cursor.Y);
                Console.Write("Congratulations ! Level ");
                Console.Write(Game.counters.level);
                Console.Write(" completed.");
                Game.cursor.Y++;
                Console.SetCursorPosition(Game.cursor.X, Game.cursor.Y);
                Thread.Sleep(1000);
                Console.Write("Press Any Key To Continue...");
                Console.ReadLine();
                Game.highScores.Add(Game.counters.scores);
                congrats();
                Main();
            }
            if (Game.health.over)
            {
                Game.health.over = false;

                Console.SetCursorPosition(Game.cursor.X, Game.cursor.Y);
                Console.Write("OOPS ! Game Over.");
                Game.cursor.Y++;
                Console.SetCursorPosition(Game.cursor.X, Game.cursor.Y);
                Game.highScores.Add(Game.counters.scores);
                Thread.Sleep(1000);
                Console.Write("Press Any Key To Continue...");
                Console.ReadLine();
                storeScores(Game.highScores);
                Main();
            }
            Game.cursor.X = 110;
            Game.cursor.Y = 1;
        }
        static bool completedLevel(ref Health h)
        {
            if (h.Enemy1 <= 0 && h.Enemy2 <= 0 && h.Enemy3 <= 0)
            {
                return true;
            }
            return false;
        }
        static void congrats()
        {

        }
        static void empty(int enemyX, int enemyY)
        {
            Console.SetCursorPosition(enemyY, enemyX);
            Console.Write("     ");
            gameBoard[enemyX, enemyY] = gameBoard[enemyX, enemyY + 1] = gameBoard[enemyX, enemyY + 2] = gameBoard[enemyX, enemyY + 3] = gameBoard[enemyX, enemyY + 4] = ' ';
            Console.SetCursorPosition(enemyY - 1, enemyX + 1);
            Console.Write("       ");
            gameBoard[enemyX + 1, enemyY - 1] = ' ';
            gameBoard[enemyX + 1, enemyY + 2] = ' ';
            gameBoard[enemyX + 1, enemyY + 5] = ' ';
            Console.SetCursorPosition(enemyY - 2, enemyX + 2);
            Console.Write("         ");
            gameBoard[enemyX + 2, enemyY - 2] = ' ';
            gameBoard[enemyX + 2, enemyY - 1] = gameBoard[enemyX + 2, enemyY + 1] = gameBoard[enemyX + 2, enemyY] = ' ';
            gameBoard[enemyX + 2, enemyY + 2] = ' ';
            gameBoard[enemyX + 2, enemyY + 3] = gameBoard[enemyX + 2, enemyY + 4] = gameBoard[enemyX + 2, enemyY + 5] = ' ';
            gameBoard[enemyX + 2, enemyY + 6] = ' ';
            Console.SetCursorPosition(enemyY - 2, enemyX + 3);
            Console.Write("         ");
            gameBoard[enemyX + 3, enemyY - 2] = ' ';
            gameBoard[enemyX + 3, enemyY + 6] = ' ';
            Console.SetCursorPosition(enemyY - 1, enemyX + 4);
            Console.Write("       ");
            gameBoard[enemyX + 4, enemyY - 1] = ' ';
            gameBoard[enemyX + 4, enemyY] = ' ';
            gameBoard[enemyX + 4, enemyY + 1] = ' ';
            gameBoard[enemyX + 4, enemyY + 2] = ' ';
            gameBoard[enemyX + 4, enemyY + 3] = ' ';
            gameBoard[enemyX + 4, enemyY + 4] = ' ';
            gameBoard[enemyX + 4, enemyY + 5] = ' ';
            Console.SetCursorPosition(enemyY, enemyX + 5);
            Console.Write("     ");
            gameBoard[enemyX + 5, enemyY] = ' ';
            gameBoard[enemyX + 5, enemyY + 4] = ' ';
            Console.SetCursorPosition(enemyY + 1, enemyX + 6);
            Console.Write("   ");
            gameBoard[enemyX + 6, enemyY + 1] = ' ';
            gameBoard[enemyX + 6, enemyY + 2] = ' ';
            gameBoard[enemyX + 6, enemyY + 3] = ' ';
            Console.SetCursorPosition(enemyY + 2, enemyX + 7);
            Console.Write(" ");
            gameBoard[enemyX + 7, enemyY + 2] = ' ';

        }
    }
}
