using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Made by Jan Borecky for PRG seminar at Gymnazium Voderadska, year 2024-2025.
 * Extended by students.
 */

namespace Calculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * ZADANI
             * Vytvor program ktery bude fungovat jako kalkulacka. Kroky programu budou nasledujici:
             * 1) Nacte vstup pro prvni cislo od uzivatele (vyuzijte metodu Console.ReadLine() - https://learn.microsoft.com/en-us/dotnet/api/system.console.readline?view=netframework-4.8.
             * 2) Zkonvertuje vstup od uzivatele ze stringu do integeru - https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/types/how-to-convert-a-string-to-a-number.
             * 3) Nacte vstup pro druhe cislo od uzivatele a zkonvertuje ho do integeru. (zopakovani kroku 1 a 2 pro druhe cislo)
             * 4) Nacte vstup pro ciselnou operaci. Rozmysli si, jak operace nazves. Muze to byt "soucet", "rozdil" atd. nebo napr "+", "-", nebo jakkoliv jinak.
             * 5) Nadefinuj integerovou promennou result a prirad ji prozatimne hodnotu 0.
             * 6) Vytvor podminky (if statement), podle kterych urcis, co se bude s cisly dit podle zadane operace
             *    a proved danou operaci - https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/selection-statements.
             * 7) Vypis promennou result do konzole
             * 
             * Rozsireni programu pro rychliky / na doma (na poradi nezalezi):
             * 1) Vypis do konzole pred nactenim kazdeho uzivatelova vstupu co po nem chces (aby vedel, co ma zadat)
             * 2) a) Kontroluj, ze uzivatel do vstupu zadal, co mel (cisla, popr. ciselnou operaci). Pokud zadal neco jineho, napis mu, co ma priste zadat a program ukoncete.
             * 2) b) To same, co a) ale misto ukonceni programu opakovane cti vstup, dokud uzivatel nezada to, co ma
             *       - https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/statements/iteration-statements#the-while-statement
             * 3) Umozni uzivateli zadavat i desetinna cisla, tedy prekopej kalkulacku tak, aby umela pracovat s floaty
             */

            double firstNum = 0;
            double secondNum = 0;
            int thirdNum = 0;
            int decimalNum = 0;
            string thirdString = "";
            // určíme si neznámé se kterými budeme pracovat
            Console.WriteLine("Pro přístup ke kalkulačce napiš: kalkulčka. Pro přechod mezi soustavami napiš: převod");
            string answer = Console.ReadLine();
            // vybereme si zda budeme používat kalkulačku nebo převod soustav
            if (answer == "kalkulačka")
            { 
            while (true)
            {
                Console.WriteLine("Napiš první číslo.");
                string input = Console.ReadLine();
                if (double.TryParse(input, out firstNum))
                {
                    firstNum = Convert.ToDouble(input);
                    break;
                }
                else
                {
                    Console.WriteLine("Zadané číslo není platné. Zkuste to prosím znovu.");
                }
            }
            while (true)
            {
                Console.WriteLine("Napiš druhé číslo.");
                string input = Console.ReadLine();
                if (double.TryParse(input, out secondNum))
                {
                    secondNum = Convert.ToDouble(input);
                    break;
                }
                else
                {
                    Console.WriteLine("Zadané číslo není platné. Zkuste to prosím znovu.");
                }
                    // zkontrolujeme zda je input číslo, pokud ne opakujeme výzvu
                }
                while (true) 
            {
                Console.WriteLine("Napiš číselnou operaci (+,-,*,/,^,sqrt).");
                string operace = Console.ReadLine();
                if (operace == "+") 
                {
                    Console.WriteLine(firstNum + secondNum);
                    break;
                }
                else if (operace == "-") 
                {
                    Console.WriteLine(firstNum - secondNum);
                    break;
                }
                else if (operace == "*")
                {
                    Console.WriteLine(firstNum * secondNum);
                    break;
                }
                else if (operace == "/")
                {
                    if (secondNum == 0)
                    {
                        Console.WriteLine("Nelze dělit nulou. Zkuste to prosím znovu.");
                    }
                    else
                    Console.WriteLine(firstNum / secondNum);
                    break;
                }
                else if (operace == "^")
                {
                    Console.WriteLine(Math.Pow(firstNum, secondNum));
                    break;
                }
                else if (operace == "sqrt")
                {
                    if (firstNum < 0)
                    {
                        Console.WriteLine("Nelze odmocnit záporné číslo. Zkuste to prosím znovu.");
                    }
                    else
                    Console.WriteLine(Math.Sqrt(firstNum));
                    break;
                }
                else
                {
                    Console.WriteLine("Zadaná operace je neplatná. Zkuste to prosím znovu.");
                }
                    //stejný postup opakujeme pro zadání operace (program by fungoval i pomocí příkazu case)
                }
            }
            else if (answer == "převod")
            {
                Console.WriteLine("Do jaké soustavy chceš převádět? Vyćházíme z desítkové.");
                string input2 = Console.ReadLine();
                if (int.TryParse(input2, out decimalNum))
                {
                    decimalNum = Convert.ToInt32(input2);
                    Console.WriteLine("Jaké číslo chceš převádět?");
                    thirdString = Console.ReadLine();
                    thirdNum = Convert.ToInt32(thirdString);
                    string binaryNum = Convert.ToString(thirdNum, decimalNum);
                    Console.WriteLine(binaryNum);
                }
                // převod soustav - inspirován ChatGPT, vlastní přepis
            }
            else { Console.WriteLine("Zadaný příkaz není platný.");
            }
            Console.ReadKey(); //Toto nech jako posledni radek, aby se program neukoncil ihned, ale cekal na stisk klavesy od uzivatele.
        }
    }
}
