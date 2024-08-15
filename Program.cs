using SpaceDead;
using System.Drawing;

Window window;
Spaceship spaceship;
bool play = false;
bool finalBoss = false;
bool ejecusion = true;
Enemy enemy1;
Enemy enemy2;
Enemy enemy3;




void Initialize()
{
    window = new Window(170, 41, ConsoleColor.Black, new Point(5, 5), new Point(160, 40));
    window.DrawMargins();
    spaceship = new Spaceship(new Point(80, 30), ConsoleColor.White, window);

    enemy1 = new Enemy(new Point(50, 10), ConsoleColor.Cyan, window, TypeEnemy.Normal, spaceship);

    enemy2 = new Enemy(new Point(100, 10), ConsoleColor.DarkYellow, window, TypeEnemy.Normal, spaceship);

    enemy3 = new Enemy(new Point(75, 10), ConsoleColor.Red, window, TypeEnemy.Boss, spaceship);

    spaceship.enemies.Add(enemy1);
    spaceship.enemies.Add(enemy2);
    spaceship.enemies.Add(enemy3);
}

void Restart ()
{ 
    Console.Clear();
    window.DrawMargins();

    spaceship.Life = 100;
    spaceship.SuperCharge = 0;
    spaceship.SpecialBullet = 0;
    spaceship.Bullets.Clear();

    enemy1.Live = true;
    enemy1.Life = 100;
    enemy2.Live = true;
    enemy2.Life = 100;
    enemy3.Live = true;
    enemy3.Life = 100;
    enemy3.EnemyPositions.Clear();

    finalBoss = false;
}
void Game()
{
    while (ejecusion)
    {
        window.Menu();
        window.Keyboard(ref ejecusion, ref play);
        while (play)
        {
            if (!enemy1.Live && !enemy2.Live && !finalBoss)
            {
                finalBoss = true;
                window.Danger();
            }
            if (finalBoss)
            {
                enemy3.MoveEnemy();
                enemy3.Information(140);
            }
            else
            {
                enemy1.MoveEnemy();
                enemy1.Information(100);
                enemy2.MoveEnemy();
                enemy2.Information(120);
            }

            spaceship.MoveSpaceship(2);
            spaceship.Shoot();
            if (spaceship.Life <= 0)
            {
                play = false;
                spaceship.Dead();
                Restart();
            }

            if (!enemy3.Live)
            {
                play = false;
                Restart();
            }
        }
    }  
}

Initialize();
Game();



/*
 * Esto se usa para saber el tamaño maximo de la consola
Console.WriteLine("Max width: " + Console.LargestWindowWidth);
Console.WriteLine("Max height: " + Console.LargestWindowHeight);
*/
