using System;
using System.Collections.Generic;

class Game
{
    private Player player;
    private Environment environment;
    private bool isRunning;
    private Random random;

    public Game()
    {
        player = new Player();
        environment = new Environment();
        isRunning = true;
        random = new Random();
    }

    public void Start()
    {
        Console.WriteLine("Probudil ses na opuštěném ostrově po ztroskotání lodi. Musíš přežít a najít cestu pryč.");
        Console.ReadKey();
        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("\n==============================");
            player.DisplayStatus();
            Console.WriteLine("\nCo chceš udělat?");
            Console.WriteLine("[1] Prozkoumat ostrov (explore)");
            Console.WriteLine("[2] Hledat suroviny (scavenge)");
            Console.WriteLine("[3] Postavit něco (build)");
            Console.WriteLine("[4] Jíst (eat)");
            Console.WriteLine("[5] Spát (sleep)");
            Console.WriteLine("[6] Lovit (hunt)");
            Console.WriteLine("[7] Pokusit se postavit raft (raft)");
            Console.WriteLine("[8] Odejít (exit)");
            Console.Write("Zadej číslo akce: ");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    player.Explore(random);
                    break;
                case "2":
                    player.Scavenge(random);
                    break;
                case "3":
                    player.Build();
                    break;
                case "4":
                    player.Eat();
                    break;
                case "5":
                    player.Sleep();
                    break;
                case "6":
                    player.Hunt(random);
                    break;
                case "7":
                    if (player.AttemptRaftEscape()) isRunning = false;
                    break;
                case "8":
                    Environment.ExitGame();
                    break;
                default:
                    Console.WriteLine("Neplatná volba.");
                    break;
            }

            environment.UpdateWeather(random);
            player.CheckStatus(ref isRunning);
        }
        Console.WriteLine("Konec hry.");
    }

    static void Main()
    {
        Game game = new Game();
        game.Start();
    }
}
