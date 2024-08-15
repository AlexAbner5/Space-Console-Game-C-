using System;
using System.Collections.Generic;
using System.Drawing;

namespace SpaceDead
{
    internal class Spaceship
    {
        public float Life { get; set; }
        public Point Position { get; set; }
        public ConsoleColor Color { get; set; }
        public Window WindowC { get; set; }
        public List<Point> SpaceshipPositions { get; set; }

        public List<Bullet> Bullets { get; set; }

        public float SuperCharge { get; set; }

        public bool SuperChargeCond { get; set; }

        public float SpecialBullet { get; set; }

        public List<Enemy> enemies { get; set; }

        public ConsoleColor ColorAux { get; set; }

        public DateTime TimeColision { get; set; }

        public Spaceship(Point position, ConsoleColor color, Window window)
        {
            Life = 100;
            Position = position;
            Color = color;
            WindowC = window;
            SpaceshipPositions = new List<Point>();
            Bullets = new List<Bullet>();
            enemies = new List<Enemy>();
            ColorAux = color;
            TimeColision = DateTime.Now;
        }

        public void Draw()
        {
            if (DateTime.Now >TimeColision.AddMilliseconds(1000))
            {
                Console.ForegroundColor = Color;
            }
            else
            {
                Console.ForegroundColor = ColorAux;
            }


            int x = Position.X;
            int y = Position.Y;

            // Draw the spaceship
            Console.SetCursorPosition(x + 3, y);
            Console.Write("A");

            Console.SetCursorPosition(x + 1, y + 1);
            Console.Write("<{x}>");

            Console.SetCursorPosition(x, y + 2);
            Console.Write("± W W ±");

            SpaceshipPositions.Clear();

            // Save the spaceship positions

            // Position of A
            SpaceshipPositions.Add(new Point(x + 3, y));

            // Position of <{x}>
            SpaceshipPositions.Add(new Point(x + 1, y + 1));
            SpaceshipPositions.Add(new Point(x + 2, y + 1));
            SpaceshipPositions.Add(new Point(x + 3, y + 1));
            SpaceshipPositions.Add(new Point(x + 4, y + 1));
            SpaceshipPositions.Add(new Point(x + 5, y + 1));

            // Position of ± W W ±
            SpaceshipPositions.Add(new Point(x, y + 2));
            SpaceshipPositions.Add(new Point(x + 2, y + 2));
            SpaceshipPositions.Add(new Point(x + 4, y + 2));
            SpaceshipPositions.Add(new Point(x + 6, y + 2));
        }

        public void Clear()
        {
            foreach (Point position in SpaceshipPositions)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.Write(" ");
            }
        }

        public void KeyBoard(ref Point distance, int SpeedOfSpaceship)
        {
            ConsoleKeyInfo k = Console.ReadKey(true);

            if (k.Key == ConsoleKey.W)
            {
                distance = new Point(0, -1);
            }
            else if (k.Key == ConsoleKey.S)
            {
                distance = new Point(0, 1);
            }
            else if (k.Key == ConsoleKey.A)
            {
                distance = new Point(-1, 0);
            }
            else if (k.Key == ConsoleKey.D)
            {
                distance = new Point(1, 0);
            }

            distance.X *= SpeedOfSpaceship;
            distance.Y *= SpeedOfSpaceship;

            if (k.Key == ConsoleKey.RightArrow)
            {   
                if (!SuperChargeCond)
                {
                    Bullet bullet = new Bullet(new Point(Position.X + 6, Position.Y + 2),
                    ConsoleColor.White, TypeBullet.Simple);
                    Bullets.Add(bullet);
                    
                    SuperCharge += 0.5f;
                    if (SuperCharge >= 100)
                    {
                        SuperChargeCond = true;
                        SuperCharge = 100;

                    }
                }


            }
            else if (k.Key == ConsoleKey.LeftArrow)
            {
                if (!SuperChargeCond)
                {
                    Bullet bullet = new Bullet(new Point(Position.X, Position.Y + 2),
                    ConsoleColor.White, TypeBullet.Simple);
                    Bullets.Add(bullet);

                    SuperCharge += 0.5f;
                    if (SuperCharge >= 100)
                    {
                        SuperChargeCond = true;
                        SuperCharge = 100;

                    }
                }

            }
            if (k.Key == ConsoleKey.UpArrow)
            {
                if (SpecialBullet >= 100)
                {
                    Bullet bullet = new Bullet(new Point(Position.X + 2, Position.Y - 2),
                                          ConsoleColor.White, TypeBullet.Special);
                    Bullets.Add(bullet);
                    SpecialBullet = 0;
                }
            }

        }

        public void Collisions (Point distance)
        {
            Point positionAux = new Point(Position.X + distance.X, Position.Y + distance.Y);
            if (positionAux.X<=WindowC.SuperiorLimit.X)
            {
                positionAux.X = WindowC.SuperiorLimit.X+1;
            }
            if (positionAux.X + 6 >= WindowC.InferiorLimit.X)
            {
                positionAux.X = WindowC.InferiorLimit.X - 7;
            }
            if (positionAux.Y <= (WindowC.SuperiorLimit.Y)+14)
            {
                positionAux.Y = (WindowC.SuperiorLimit.Y + 1)+14;
            }
            if (positionAux.Y + 2 >= WindowC.InferiorLimit.Y)
            {
                positionAux.Y = WindowC.InferiorLimit.Y - 3;
            }

            Position = positionAux;
        }

        public void information()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(WindowC.SuperiorLimit.X, WindowC.SuperiorLimit.Y - 1);
            Console.Write($"Life: " +(int)Life+ " % ");

            // Supercharge logic
            if (SuperChargeCond)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else Console.ForegroundColor = ConsoleColor.White;

            if (SuperCharge <= 0)
            {
                SuperCharge = 0;
            }
            else SuperCharge -= 0.0007f;

            if (SuperCharge <= 50)
            {
                SuperChargeCond = false;
            }

            Console.SetCursorPosition(WindowC.SuperiorLimit.X + 13, WindowC.SuperiorLimit.Y - 1);
            Console.Write($"Supercharge: " + (int)SuperCharge + " % ");

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(WindowC.SuperiorLimit.X + 31, WindowC.SuperiorLimit.Y - 1);
            Console.Write($"Special Bullet: " + (int)SpecialBullet + " % ");

            if (SpecialBullet <= 100)
            {
                SpecialBullet += 0.01f;
            }

        }
        public void MoveSpaceship(int SpeedOfSpaceship)
        {
            if (Console.KeyAvailable)
            {
                Clear();
                Point distance = new Point();
                KeyBoard(ref distance, SpeedOfSpaceship);
                Collisions(distance);
                Draw();  
            }
            information();
        }

        public void Shoot()
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                if (Bullets[i].MoveBullet(1, WindowC.SuperiorLimit.Y, enemies))
                {
                    Bullets.Remove(Bullets[i]);
                    --i;
                }
            }
        }

        public void Dead()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (Point position in SpaceshipPositions)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.Write("X");
                Thread.Sleep(200);
            }
            foreach (Point position in SpaceshipPositions)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.Write(" ");
            }
        }
    }

}

