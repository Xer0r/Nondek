using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace minceb
{
    internal class Program
    {
        /// <summary>
        /// bool kontrolující existenci alespoň jedné kombinace
        /// </summary>
        public static bool IsValid { get; set; } = false;
        /// <summary>
        /// pole pro uložení možných hodnot mincí
        /// </summary>
        public static int[] CoinTypes { get; set; }
        /// <summary>
        /// StringBuilder (SB) pro uložení aktuální rozpracované kombinace mincí
        /// </summary>
        public static StringBuilder Combination = new StringBuilder();
        /// <summary>
        /// Funkce nachází a vypisuje kombinace mincí dávajících dohromady zadaný součet ('goal').
        /// </summary>
        /// <param name="goal">cílený součet</param>
        /// <param name="coinStage">indexové označení prořešených druhů mincí (při spuštění zadejte '0')</param>
        static void CombineCoins(int goal, int coinStage)
        {
            for (int counter = coinStage; counter < CoinTypes.Length; counter++)  //cyklus postupně sestupně prochází hodnoty mincí a porovnává s cílenou hodnotou
            {
                if (CoinTypes[counter] > goal) //hodnota příliš velká -> v další kombinaci již nemůže být
                {
                    continue;
                }
                else if (CoinTypes[counter] == goal) //hodnota přesně odpovidá té hledané -> našli jsme řešení -> přidáme hodnotu do SB, vypíšeme kombinaci a hodnotu zase odstraníme
                {
                    Combination.Append(CoinTypes[counter]);
                    string version = Combination.ToString();
                    Console.WriteLine(version); //vypsání řešení
                    Combination.Length--;
                    IsValid = true;
                }
                else
                {
                    Combination.Append(CoinTypes[counter]); //hodnotu lze použít při dalším hledání kombinace -> přidáme ji do SB a rekurzivně hledáme dál
                    Combination.Append(' ');
                    CombineCoins(goal - CoinTypes[counter], counter);
                    Combination.Length--;
                    Combination.Length--;
                }
            }
        }

        static void Main(string[] args)
        {
            CoinTypes = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int Sum = int.Parse(Console.ReadLine());                                 //načtení vstupu

            CombineCoins(Sum, 0);

            if (!IsValid)   //znázornění ukončení hledání kombinací a případné upozornění na neexistenci jakéhokoli řešení 
            {
                Console.WriteLine("Nelze");
            }
            else
            {
                Console.WriteLine("Hotovo");
            }

            Console.ReadLine();
        }
    }
}
