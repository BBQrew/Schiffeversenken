using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Schiffeversenken
{
    class Program
    {
        static void Main(string[] args)
        {
            var rand = new Random();
            int x, y, feldLaenge = 10, xy, lauf1 = 0;
            bool fehler = false;
            int[,] feld = new int[feldLaenge, feldLaenge];
            int[,] feldArbeit = new int[feldLaenge, feldLaenge];
            int[] schiffe = new int[] { 5, 4, 4, 3, 3, 3, 2, 2, 2, 2 };
            int schiffAnzahl = schiffe.Length;

            //Schiffe automatisch setzen
            while (lauf1 < schiffAnzahl)
            {
                x = rand.Next(0, feldLaenge);
                y = rand.Next(0, feldLaenge);
                xy = rand.Next(2);
                int schiffLaenge = schiffe[lauf1];

                //X- oder Y-Richtung
                if (xy == 0)
                    for (int a = 0; a < feldLaenge; a++) for (int b = 0; b < feldLaenge; b++) feldArbeit[a, b] = feld[a, b];
                else if (xy == 1)
                    for (int a = 0; a < feldLaenge; a++) for (int b = 0; b < feldLaenge; b++) feldArbeit[a, b] = feld[b, a];

                if (
                    //Feld leer?
                    feldArbeit[x, y] == 0
                    //Feld links (X-1) vorhanden und leer?
                    && (x == 0 || (x > 0 && feldArbeit[x - 1, y] == 0))
                    //Passt das Schiff in diese Zeile?
                    && x <= feldLaenge - schiffLaenge
                    )
                {
                    for (int lauf2 = 0; lauf2 < schiffLaenge; lauf2++)
                    {
                        //Schiff erstellen 
                        if (
                        //Feld leer?
                        feldArbeit[x + lauf2, y] == 0
                        //Feld rechts (X+1) vorhanden und leer?
                        && (x + lauf2 + 1 == feldLaenge || feldArbeit[x + lauf2 + 1, y] == 0)
                        //Feld darunter (Y+1) vorhanden und leer?
                        && (y + 1 == feldLaenge || feldArbeit[x + lauf2, y + 1] == 0)
                        //Feld darüber (Y-1) vorhanden und leer?
                        && (y == 0 || feldArbeit[x + lauf2, y - 1] == 0))
                            feldArbeit[x + lauf2, y] = 1;

                        else
                        {
                            fehler = true;
                            lauf2 = schiffLaenge;
                        }
                    }
                }

                else
                {
                    fehler = true;
                }

                if (fehler == false)
                {
                    for (int a = 0; a < feldLaenge; a++) for (int b = 0; b < feldLaenge; b++) feld[a, b] = feldArbeit[a, b];
                    lauf1++;
                }
                else
                    fehler = false;
            }

            //Versenken
            int c, schuss = 0;
            bool schiffeVorhanden = true;

            while (schiffeVorhanden == true)
            {
                Console.Clear();

                //Ausgabe
                for (y = 0; y < feldLaenge; y++)
                {
                    Console.Write($"\n  ");
                    for (x = 0; x < feldLaenge; x++)
                    {
                        switch (feld[x, y])
                        {
                            //case 1:
                            //    Console.BackgroundColor = ConsoleColor.White;
                            //    Console.ForegroundColor = ConsoleColor.White;
                            //    break;
                            case 2:
                                Console.BackgroundColor = ConsoleColor.Blue;
                                Console.ForegroundColor = ConsoleColor.Blue;
                                break;
                            case 3:
                                Console.BackgroundColor = ConsoleColor.DarkRed;
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                break;
                            default:
                                Console.BackgroundColor = ConsoleColor.Green;
                                Console.ForegroundColor = ConsoleColor.Green;
                                break;
                        }
                        Console.Write("{0}{0}", feld[x, y]);
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("  ");
                    }
                    Console.WriteLine(y + 1);
                }
                Console.Write("\n  1   2   3   4   5   6   7   8   9   10");

                Console.Write("\n\nX-Koordinate: ");
                int schussX = Convert.ToInt32(Console.ReadLine()) - 1;
                Console.Write("Y-Koordinate: ");
                int schussY = Convert.ToInt32(Console.ReadLine()) - 1;

                if (feld[schussX, schussY] == 2 || feld[schussX, schussY] == 3)
                {
                    Console.Write("\nDu hast dieses Feld bereits beschossen\nDrücke eine Taste um weiterzuspielen...");
                    Console.ReadKey();
                }
                else
                {
                    feld[schussX, schussY] += 2;
                    schuss++;
                }

                //Schiffe vorhanden?
                for (int a = 0; a < feldLaenge; a++) for (int b = 0; b < feldLaenge; b++)
                    {
                        if (feld[a, b] == 1)
                        {
                            schiffeVorhanden = true;
                            a = feldLaenge;
                            b = feldLaenge;
                        }
                        //Wenn Feld am Ende und kein Schiff vorhanden
                        else if (a == feldLaenge - 1 && b == feldLaenge - 1 && feld[a, b] == 0)
                            schiffeVorhanden = false;
                    }
            }
            Console.Clear();
            Console.Write($"\nGlückwunsch! {schuss} Schüsse");

            Console.ReadKey();
        }
    }
}
