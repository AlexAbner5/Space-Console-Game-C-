using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceDead
{   
    public enum TypeBullet
    {
        Simple,
        Double,
        Triple,
        Special,
        Enemy,
        Menu
    }
    internal class Bullet
    {
        public Point Position { get; set; }

        public ConsoleColor Color { get; set; }

        public TypeBullet TypeBulletB { get; set; }

        public List<Point> BulletPositions { get; set; }

        private DateTime _time;

        public Bullet(Point position, ConsoleColor color, TypeBullet typeBullet)
        {
            Position = position;
            Color = color;
            TypeBulletB = typeBullet;
            BulletPositions = new List<Point>();
            _time = DateTime.Now;
        }

        public void Draw()
        {
            Console.ForegroundColor = Color;
            int x = Position.X;
            int y = Position.Y;

            BulletPositions.Clear();

            switch (TypeBulletB)
            {
                case TypeBullet.Simple:
                    Console.SetCursorPosition(x, y);
                    Console.Write("o");
                    BulletPositions.Add(new Point(x, y));
                    break;

                case TypeBullet.Special:
                    Console.SetCursorPosition(x+1, y);
                    Console.Write("_");
                    Console.SetCursorPosition(x, y+1);
                    Console.Write("( )");
                    Console.SetCursorPosition(x + 1, y + 2);
                    Console.Write("W");

                    BulletPositions.Add(new Point(x+1, y));
                    BulletPositions.Add(new Point(x,y+1));
                    BulletPositions.Add(new Point(x+2,y+1));
                    BulletPositions.Add(new Point(x+1,y+2));
                    break;
                case TypeBullet.Enemy:
                    Console.SetCursorPosition(x, y);
                    Console.Write("█");
                    BulletPositions.Add(new Point(x, y));
                    break;
                case TypeBullet.Menu:
                    Console.SetCursorPosition(x, y);
                    Console.Write("!");
                    BulletPositions.Add(new Point(x, y));
                    break;
            }
        }

        public void Clear()
        {
            foreach (Point position in BulletPositions)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.Write(" ");
            }
        }

        public bool MoveBullet(int speedOfBullet, int limit, List<Enemy> enemies)
        {   
            if (DateTime.Now > _time.AddMilliseconds(20))
            {
                Clear();
                switch (TypeBulletB)
                {
                    case TypeBullet.Simple:
                        Position = new Point(Position.X, Position.Y - speedOfBullet);
                        if (Position.Y <= limit)
                            return true;

                        foreach (Enemy enemy in enemies)
                        {
                            foreach (Point position in enemy.EnemyPositions)
                            {
                                if (position.X == Position.X && position.Y == Position.Y)
                                {
                                    enemy.Life -= 5;
                                    if (enemy.Life <= 0)
                                    {
                                        enemy.Life = 0;
                                        enemy.Live = false;
                                        enemy.Dead();
                                    }
                                    return true;
                                }
                            }
                        }
                        break;

                    case TypeBullet.Special:
                        Position = new Point(Position.X, Position.Y - speedOfBullet);
                        if (Position.Y <= limit)
                            return true;

                        foreach (Enemy enemy in enemies)
                        {
                            foreach (Point position in enemy.EnemyPositions)
                            {
                                foreach (Point positionB in BulletPositions)
                                {
                                    if (position.X == positionB.X && position.Y == positionB.Y)
                                    {
                                        enemy.Life -= 40;
                                        if (enemy.Life <= 0)
                                        {
                                            enemy.Life = 0;
                                            enemy.Live = false;
                                            enemy.Dead();

                                        }
                                        return true;
                                    }
                                }
                            }
                        }
                        break;
                }
                Draw();
                _time = DateTime.Now;
                
            }
            return false;
        }

        public bool MoveBullet(int speedOfBullet, int limit, Spaceship spaceship)
        {
            if (DateTime.Now > _time.AddMilliseconds(20))
            {
                Clear();
                Position = new Point(Position.X, Position.Y + speedOfBullet);
                if (Position.Y >= limit)
                {
                    return true;
                }

                foreach (Point PositionsS in spaceship.SpaceshipPositions)
                {
                    if (PositionsS.X == Position.X && PositionsS.Y == Position.Y)
                    {
                        spaceship.Life -= 2;
                        spaceship.ColorAux = Color;
                        spaceship.TimeColision = DateTime.Now;
                        return true;
                    }
                }

                Draw();
                _time = DateTime.Now;

            }
            return false;
        }

        public bool MoveBulletMenu(int speedOfBullet, int limit)
        {
            if (DateTime.Now > _time.AddMilliseconds(20))
            {
                Clear();
                Position = new Point(Position.X, Position.Y - speedOfBullet);
                if (Position.Y <= limit)
                {
                    return true;
                }

                Draw();
                _time = DateTime.Now;
            }
            return false;
        }
    }
}
