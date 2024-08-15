using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SpaceDead
{
    // Class of the object Window
    internal class Window
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public ConsoleColor Color { get; set; }

        public Point InferiorLimit { get; set; }
        public Point SuperiorLimit { get; set; }

        private Enemy _enemy1;
        private Enemy _enemy2;
        private List<Bullet> _bullets;
        private Random _random;

        // Constructor of the class Window
        public Window(int width, int height, ConsoleColor color, Point superiorLimit, Point inferiorLimit)
        {
            Width = width;
            Height = height;
            Color = color;
            _random = new Random();
            InferiorLimit = inferiorLimit;
            SuperiorLimit = superiorLimit;

            SetSizeWindow();
            InitializeMenuElements();
        }

        // Method for setting the configuration of the window
        private void SetSizeWindow()
        {
            Console.SetWindowSize(Width, Height);
            Console.Title = "Space Dead";
            Console.CursorVisible = false;
            Console.BackgroundColor = Color;
            Console.Clear();
            Console.WriteLine("Window size set to: " + Width + "x" + Height);
        }

        private void InitializeMenuElements()
        {
            _enemy1 = new Enemy(new Point(50, 10), ConsoleColor.Cyan, this, TypeEnemy.Menu, null);
            _enemy2 = new Enemy(new Point(100, 30), ConsoleColor.DarkYellow, this, TypeEnemy.Menu, null);
            _bullets = new List<Bullet>();
            AddBullet();
        }

        // Methot draw the margins of the window

        public void DrawMargins()
        {
            Console.WriteLine("Drawing margins from " + SuperiorLimit + " to " + InferiorLimit);
            Console.ForegroundColor = ConsoleColor.White;


            // Draw top and bottom margins
            for (int i = SuperiorLimit.X; i <= InferiorLimit.X; i++)
            {
                Console.SetCursorPosition(i, SuperiorLimit.Y);
                Console.Write("═");

                Console.SetCursorPosition(i, InferiorLimit.Y);
                Console.Write("═");
            }

            // Draw left and right margins
            for (int i = SuperiorLimit.Y; i <= InferiorLimit.Y; i++)
            {
                Console.SetCursorPosition(SuperiorLimit.X, i);
                Console.Write("║");

                Console.SetCursorPosition(InferiorLimit.X, i);
                Console.Write("║");
            }

            // Draw corners
            Console.SetCursorPosition(SuperiorLimit.X, SuperiorLimit.Y);
            Console.Write("╔");

            Console.SetCursorPosition(SuperiorLimit.X, InferiorLimit.Y);
            Console.Write("╚");

            Console.SetCursorPosition(InferiorLimit.X, SuperiorLimit.Y);
            Console.Write("╗");

            Console.SetCursorPosition(InferiorLimit.X, InferiorLimit.Y);
            Console.Write("╝");
        }

        public void Danger()
        {
            Console.Clear();
            DrawMargins();
            for (int i = 0; i < 6; i++)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(Width / 2 -5, Height / 2);
                Console.WriteLine("DANGER");
                Thread.Sleep(200);
                Console.Clear();
                Console.SetCursorPosition(Width / 2 - 5, Height / 2);
                Console.WriteLine("      ");
                DrawMargins();
                Thread.Sleep(200);
            }
        }

        public void Menu ()
        {
            _enemy1.MoveEnemy();
            _enemy2.MoveEnemy();
            MoveBullets();

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(Width / 2 - 5, Height / 2 - 1);
            Console.WriteLine("[ENTER] Play");
            Console.SetCursorPosition(Width / 2 - 5, Height / 2);
            Console.WriteLine("[ESC] Exit");
        }

        public void Keyboard(ref bool ejecusion, ref bool play)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        Console.Clear();
                        play = true;
                        DrawMargins();
                        break;
                    case ConsoleKey.Escape:
                        ejecusion = false;
                        break;
                }
            }
        }

        public void AddBullet()
        {
            // Crete 20 bullets whit diferent colors
            Bullet bullet1 = new Bullet(new Point(0, 0), ConsoleColor.White, TypeBullet.Menu);
            _bullets.Add(bullet1);
            Bullet bullet2 = new Bullet(new Point(0, 0), ConsoleColor.Blue, TypeBullet.Menu);
            _bullets.Add(bullet2);
            Bullet bullet3 = new Bullet(new Point(0, 0), ConsoleColor.Red, TypeBullet.Menu);
            _bullets.Add(bullet3);
            Bullet bullet4 = new Bullet(new Point(0, 0), ConsoleColor.Yellow, TypeBullet.Menu);
            _bullets.Add(bullet4);
            Bullet bullet5 = new Bullet(new Point(0, 0), ConsoleColor.Green, TypeBullet.Menu);
            _bullets.Add(bullet5);
            Bullet bullet6 = new Bullet(new Point(0, 0), ConsoleColor.Magenta, TypeBullet.Menu);
            _bullets.Add(bullet6);
            Bullet bullet7 = new Bullet(new Point(0, 0), ConsoleColor.Cyan, TypeBullet.Menu);
            _bullets.Add(bullet7);
            Bullet bullet8 = new Bullet(new Point(0, 0), ConsoleColor.Gray, TypeBullet.Menu);
            _bullets.Add(bullet8);
            Bullet bullet9 = new Bullet(new Point(0, 0), ConsoleColor.DarkBlue, TypeBullet.Menu);
            _bullets.Add(bullet9);
            Bullet bullet10 = new Bullet(new Point(0, 0), ConsoleColor.DarkRed, TypeBullet.Menu);
            _bullets.Add(bullet10);
            Bullet bullet11 = new Bullet(new Point(0, 0), ConsoleColor.DarkYellow, TypeBullet.Menu);
            _bullets.Add(bullet11);
            Bullet bullet12 = new Bullet(new Point(0, 0), ConsoleColor.DarkGreen, TypeBullet.Menu);
            _bullets.Add(bullet12);
            Bullet bullet13 = new Bullet(new Point(0, 0), ConsoleColor.DarkMagenta, TypeBullet.Menu);
            _bullets.Add(bullet13);
            Bullet bullet14 = new Bullet(new Point(0, 0), ConsoleColor.DarkCyan, TypeBullet.Menu);
            _bullets.Add(bullet14);
            Bullet bullet15 = new Bullet(new Point(0, 0), ConsoleColor.Black, TypeBullet.Menu);
            _bullets.Add(bullet15);
            Bullet bullet16 = new Bullet(new Point(0, 0), ConsoleColor.Magenta, TypeBullet.Menu);
            _bullets.Add(bullet16);
            Bullet bullet17 = new Bullet(new Point(0, 0), ConsoleColor.Cyan, TypeBullet.Menu);
            _bullets.Add(bullet17);
            Bullet bullet18 = new Bullet(new Point(0, 0), ConsoleColor.Gray, TypeBullet.Menu);
            _bullets.Add(bullet18);
            Bullet bullet19 = new Bullet(new Point(0, 0), ConsoleColor.DarkBlue, TypeBullet.Menu);
            _bullets.Add(bullet19);
            Bullet bullet20 = new Bullet(new Point(0, 0), ConsoleColor.DarkRed, TypeBullet.Menu);
            _bullets.Add(bullet20);

            Random random = new Random();

            // Set the position of the bullets
            for (int i = 0; i < _bullets.Count; i++)
            {
                BulletAleatory(_bullets[i]);
                int Aleatory = random.Next(SuperiorLimit.Y + 1, InferiorLimit.Y);
                _bullets[i].Position = new Point(_bullets[i].Position.X, Aleatory);
            }
        }

        // Aleatory bullets 
        public void BulletAleatory(Bullet bullet)
        {

            int aleatory = _random.Next(SuperiorLimit.X + 1, InferiorLimit.X);
            bullet.Position = new Point(aleatory, InferiorLimit.Y);
        }

        // Move the bullets
        public void MoveBullets()
        {
            for (int i = 0; i < _bullets.Count; i++)
            {
                if (_bullets[i].MoveBulletMenu(1, SuperiorLimit.Y))
                {
                    BulletAleatory(_bullets[i]);
                }
            }
        }
    }
}
