using System;

class Player
{
    public int Health { get; private set; } = 100;
    public int Energy { get; private set; } = 100;
    public int FoodSupplies { get; private set; } = 5;
    public int Wood { get; private set; } = 0;
    public int Rope { get; private set; } = 0;
    public bool HasShelter { get; private set; } = false;

    public void DisplayStatus()
    {
        Console.WriteLine($"Zdraví: {Health} | Energie: {Energy} | Jídlo: {FoodSupplies}");
        Console.WriteLine($"Dřevo: {Wood} | Lano: {Rope} | Přístřešek: {(HasShelter ? "Ano" : "Ne")}");
        
    }

    public void Explore(Random random)
    {
        Console.WriteLine("Prozkoumáváš ostrov...");
        int foundFood = random.Next(1, 5);
        FoodSupplies += foundFood;
        Console.WriteLine($"Našel jsi {foundFood} kusů jídla!");
        Energy -= 10;
    }

    public void Scavenge(Random random)
    {
        Console.WriteLine("Hledáš suroviny...");
        int foundWood = random.Next(1, 4);
        int foundRope = random.Next(0, 2);
        Wood += foundWood;
        Rope += foundRope;
        Console.WriteLine($"Dřevo: {foundWood}, Lano: {foundRope}");
        Energy -= 15;
    }

    public void Build()
    {
        if (!HasShelter && Wood >= 5)
        {
            Console.WriteLine("Postavil jsi přístřešek!");
            HasShelter = true;
            Wood -= 5;
        }
        else
        {
            Console.WriteLine("Nemáš dost surovin.");
        }
    }

    public void Eat()
    {
        if (FoodSupplies > 0)
        {
            FoodSupplies--;
            Energy += 20;
            Console.WriteLine("Najedl ses.");
        }
        else
        {
            Console.WriteLine("Nemáš jídlo.");
        }
    }

    public void Sleep()
    {
        Console.WriteLine("Jdeš spát...");
        Energy = 100;
        if (!HasShelter)
        {
            Console.WriteLine("Během noci tě napadlo zvíře! Utrpěl jsi zranění.");
            Health -= 15;
        }
    }

    public void Hunt(Random random)
    {
        Console.WriteLine("Lovíš...");
        if (random.Next(0, 2) == 1)
        {
            int meat = random.Next(2, 6);
            FoodSupplies += meat;
            Console.WriteLine($"Ulovil jsi zvíře a získal {meat} masa!");
        }
        else
        {
            Console.WriteLine("Lov se nepovedl.");
            Energy -= 20;
        }
    }

    public bool AttemptRaftEscape()
    {
        if (Wood >= 10 && Rope >= 3)
        {
            Console.WriteLine("Postavil jsi raft a unikl z ostrova! Vyhrál jsi!");
            Console.ReadKey();
            return true;

        }

        Console.WriteLine("Nemáš dost materiálu na raft!");
        
        return false;
    }

    public void CheckStatus(ref bool isRunning)
    {
        if (Energy <= 0)
        {
            Console.WriteLine("Jsi vyčerpán! Ztratil jsi část zásob.");
            FoodSupplies = Math.Max(0, FoodSupplies - 2);
            Wood = Math.Max(0, Wood - 2);
            Energy = 10;
            Console.ReadKey();
        }
        if (Health <= 0)
        {
            Console.WriteLine("Zemřel jsi.");
            Console.ReadKey();
            isRunning = false;
        }
    }
}

class Environment
{
    public static void ExitGame()
    {
        Console.WriteLine("Hra ukončena.");
        Console.ReadKey();
        System.Environment.Exit(0);
    }

    public void UpdateWeather(Random random)
    {
        bool isRaining = random.Next(0, 4) == 1;
        Console.WriteLine($"Počasí: {(isRaining ? "Déšť - energie ubývá rychleji" : "Slunečno")}");
        Console.ReadKey();
    }
}
