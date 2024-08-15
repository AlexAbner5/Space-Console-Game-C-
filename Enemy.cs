using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SpaceDead
{
    public enum TypeEnemy
    {
        Normal,
        Boss,
        Menu
    }

    internal class Enemy
    {
        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }
        public float Life { get; set; }
        public bool Live { get; set; }
        public Point Position { get; set; }
        public List<Point> EnemyPositions { get; set; }
        public ConsoleColor Color { get; set; }
        public Window WindowC { get; set; }
        public TypeEnemy TypeEnemyE { get; set; }

        public List<Bullet> Bullets { get; set; }

        public Spaceship SpaceshipC { get; set; }

        private Direction _direction { get; set; }
        private DateTime _timeDirection { get; set; }
        private float _timeDirectionAleatory { get; set; }

        private DateTime _timeMovement { get; set; }
        private DateTime _timeShoot { get; set; }
        private float _timeShootAleatory;

        public Enemy(Point position, ConsoleColor color, Window window, TypeEnemy typeEnemy, Spaceship spaceship)
        {
            Life = 100;
            Live = true;
            Position = position;
            Color = color;
            WindowC = window;
            TypeEnemyE = typeEnemy;
            _direction = Direction.Right;
            _timeDirection = DateTime.Now;
            _timeDirectionAleatory = 1000;
            _timeMovement = DateTime.Now;
            _timeShoot = DateTime.Now;
            _timeShootAleatory = 1000;
            EnemyPositions = new List<Point>();
            Bullets = new List<Bullet>();
            SpaceshipC = spaceship;

        }

        public void Dead()
        {
            if (TypeEnemyE == TypeEnemy.Normal)
            {
                NormalDead();
            }
            if (TypeEnemyE == TypeEnemy.Boss)
            {
                BossDead();
            }
        }

        public void NormalDead()
        {
            Console.ForegroundColor = ConsoleColor.White;

            int x = Position.X;
            int y = Position.Y;

            // Draw the spaceship
            Console.SetCursorPosition(x + 1, y);
            Console.Write("▄▄Zzz");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("████");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("▀  ▀");

            EnemyPositions.Clear();

            // Clean bullets enemy
            foreach (Bullet bullet in Bullets)
            {
                bullet.Clear();
            }
            Bullets.Clear();
        }

        public void BossDead()
        {
            Console.ForegroundColor = Color;
            foreach (Point position in EnemyPositions)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.Write("▓");
                Thread.Sleep(200);
            }
            foreach (Point position in EnemyPositions)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.Write(" ");
                Thread.Sleep(200);
            }
            EnemyPositions.Clear();

            // Clean bullets enemy
            foreach (Bullet bullet in Bullets)
            {
                bullet.Clear();
            }
            Bullets.Clear();

        }
        public void Draw()
        {
            switch (TypeEnemyE)
            {
                case TypeEnemy.Normal:
                    DrawNormalEnemy();
                    break;
                case TypeEnemy.Boss:
                    DrawBossEnemy();
                    break;
                case TypeEnemy.Menu:
                    DrawNormalEnemy();
                    break;
            }
        }

        public void DrawNormalEnemy()
        {
            Console.ForegroundColor = Color;
            int x = Position.X;
            int y = Position.Y;

            // Draw the spaceship
            Console.SetCursorPosition(x+1, y);
            Console.Write("▄▄");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("████");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("▀  ▀");

            EnemyPositions.Clear();

            EnemyPositions.Add(new Point(x + 1, y));
            EnemyPositions.Add(new Point(x + 2, y));
            EnemyPositions.Add(new Point(x, y + 1));
            EnemyPositions.Add(new Point(x + 1, y + 1));
            EnemyPositions.Add(new Point(x + 2, y + 1));
            EnemyPositions.Add(new Point(x + 3, y + 1));
            EnemyPositions.Add(new Point(x, y + 2));
            EnemyPositions.Add(new Point(x + 3, y + 2));
        }

        public void DrawBossEnemy()
        {
            Console.ForegroundColor = Color;
            int x = Position.X;
            int y = Position.Y;

            EnemyPositions.Clear();

            // Draw the spaceship
            Console.SetCursorPosition(x+1, y);
            Console.Write("█▄▄▄▄█");
            Console.SetCursorPosition(x, y + 1);
            Console.Write("██ ██ ██");
            Console.SetCursorPosition(x, y + 2);
            Console.Write("█▀▀▀▀▀▀█");

            EnemyPositions.Add(new Point(x + 1, y));
            EnemyPositions.Add(new Point(x + 2, y));
            EnemyPositions.Add(new Point(x + 3, y));
            EnemyPositions.Add(new Point(x + 4, y));
            EnemyPositions.Add(new Point(x + 5, y));
            EnemyPositions.Add(new Point(x + 6, y));

            EnemyPositions.Add(new Point(x, y + 1));
            EnemyPositions.Add(new Point(x + 1, y + 1));
            EnemyPositions.Add(new Point(x + 3, y + 1));
            EnemyPositions.Add(new Point(x + 4, y + 1));
            EnemyPositions.Add(new Point(x + 6, y + 1));
            EnemyPositions.Add(new Point(x + 7, y + 1));

            EnemyPositions.Add(new Point(x, y + 2));
            EnemyPositions.Add(new Point(x + 1, y + 2));
            EnemyPositions.Add(new Point(x + 2, y + 2));
            EnemyPositions.Add(new Point(x + 3, y + 2));
            EnemyPositions.Add(new Point(x + 4, y + 2));
            EnemyPositions.Add(new Point(x + 5, y + 2));
            EnemyPositions.Add(new Point(x + 6, y + 2));
            EnemyPositions.Add(new Point(x + 7, y + 2));
        }

        public void Clear()
        {
            foreach (Point position in EnemyPositions)
            {
                Console.SetCursorPosition(position.X, position.Y);
                Console.Write(" ");
            }
        }

        public void MoveEnemy()
        {   
            if (!Live)
            {
                Dead();
                return;
            }

            int time = 100;
            if (TypeEnemyE == TypeEnemy.Boss)
                time = 50;
            if (DateTime.Now > _timeMovement.AddMilliseconds(time))
            {
                Clear();
                MoveEnemyAleatory();
                Point AuxPososition = Position;
                Movement(ref AuxPososition);
                Collisions(AuxPososition);
                Movement(ref AuxPososition);
                _timeMovement = DateTime.Now;
                Draw();
            }

            if (TypeEnemyE != TypeEnemy.Menu)
            {
                CreateBullets();
                Shoot();
            }
        }

        public void Collisions(Point AuxPosition)
        {
            int width = 3;
            if (TypeEnemyE == TypeEnemy.Boss)
                width = 7;

            if (AuxPosition.X <= WindowC.SuperiorLimit.X)
            {
                _direction = Direction.Right;
                AuxPosition.X = WindowC.SuperiorLimit.X + 1;
            }
            if (AuxPosition.X + width >= WindowC.InferiorLimit.X)
            {
                _direction = Direction.Left;
                AuxPosition.X = WindowC.InferiorLimit.X - 1 - width;
            }
            if (AuxPosition.Y <= WindowC.SuperiorLimit.Y)
            {
                _direction = Direction.Down;
                AuxPosition.Y = WindowC.SuperiorLimit.Y + 1;
            }
            if (AuxPosition.Y + 2>= WindowC.SuperiorLimit.Y + 15)
            {
                _direction = Direction.Up;
                AuxPosition.Y = WindowC.SuperiorLimit.Y + 15 -2;
            }

            Position = AuxPosition;
        }

        public void Movement(ref Point AuxPosition)
        {
            switch (_direction)
            {
                case Direction.Left:
                    AuxPosition.X -= 1;
                    break;
                case Direction.Right:
                    AuxPosition.X += 1;
                    break;
                case Direction.Up:
                    AuxPosition.Y -= 1;
                    break;
                case Direction.Down:
                    AuxPosition.Y += 1;
                    break;

            }

        }

        public void MoveEnemyAleatory()
        {
            if (DateTime.Now > _timeDirection.AddMilliseconds(_timeDirectionAleatory) 
                &&  (_direction ==  Direction.Right || _direction == Direction.Left))
            {
                Random random = new Random();
                int AleatoryNumber = random.Next(1, 5);
                switch (AleatoryNumber)
                {
                    case 1:
                        _direction = Direction.Right;
                        break;
                    case 2:
                        _direction = Direction.Left;
                        break;
                    case 3:
                        _direction = Direction.Up;
                        break;
                    case 4:
                        _direction = Direction.Down;
                        break;
                }
                _timeDirection = DateTime.Now;
                _timeDirectionAleatory = random.Next(1000, 2000);
            }

            if (DateTime.Now > _timeDirection.AddMilliseconds(50) &&
               (_direction == Direction.Up || _direction == Direction.Down))
            {
                Random random = new Random();
                int AleatoryNumber = random.Next(0, 3);
                switch (AleatoryNumber)
                {
                    case 1:
                        _direction = Direction.Right;
                        break;
                    case 2:
                        _direction = Direction.Left;
                        break;
                }
                _timeDirection = DateTime.Now;
            }
        }

        public void CreateBullets()
        {   
            if (DateTime.Now > _timeShoot.AddMilliseconds(_timeShootAleatory))
            {
                Random random = new Random();
                if (TypeEnemyE == TypeEnemy.Normal)
                {
                    Bullet bullet = new Bullet(new Point(Position.X + 1, Position.Y + 2), Color, TypeBullet.Enemy);
                    Bullets.Add(bullet);
                    _timeShootAleatory = random.Next(200, 500);
                }
                if (TypeEnemyE == TypeEnemy.Boss)
                {
                    Bullet bullet = new Bullet(new Point(Position.X + 4, Position.Y + 2), Color, TypeBullet.Enemy);
                    Bullets.Add(bullet);
                    _timeShootAleatory = random.Next(100, 150);
                }
                _timeShoot = DateTime.Now;
            }
        }

        public void Shoot()
        {
            for (int i = 0; i < Bullets.Count; i++)
            {
                if (Bullets[i].MoveBullet(1, WindowC.InferiorLimit.Y, SpaceshipC))
                {
                    Bullets.Remove(Bullets[i]);
                    --i;
                }
            }
        }

        public void Information(int distancex)
        {
            Console.ForegroundColor = Color;
            Console.SetCursorPosition(WindowC.SuperiorLimit.X+distancex, WindowC.SuperiorLimit.Y - 1);
            Console.Write($"Life: " + (int)Life + " % ");
        }

    }
}
