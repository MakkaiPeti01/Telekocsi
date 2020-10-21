using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Telekocsi
{
    class Program
    {
        static List<Autok> autok = new List<Autok>();
        static List<Igeny> igeny = new List<Igeny>();
        static Dictionary<string, int> Utvonalak = new Dictionary<string, int>();
        static void Beolvas()
        {
            StreamReader olvas = new StreamReader("autok.csv");
            olvas.ReadLine();
            while (!olvas.EndOfStream)
            {
                autok.Add(new Autok(olvas.ReadLine()));
            }
            olvas.Close();
            StreamReader sr = new StreamReader("igenyek.csv");
            sr.ReadLine();
            while (!sr.EndOfStream)
            {
                igeny.Add(new Igeny(sr.ReadLine()));
            }
            sr.Close();
            Console.WriteLine("2. feladat\n\t{0} autós hirdet fuvart", autok.Count);
        }
        static void Harmadik()
        {
            int ferohely = 0;
            foreach (var i in autok)
            {
                if (i.Indulas == "Budapest" && i.Cel == "Miskolc")
                {
                    ferohely = ferohely + i.Hely;
                }
            }
            Console.WriteLine($"3. feladat\n\t Összesen {ferohely} férőhelyet hirdettek" +
                $"az autósok Budapestről Miskolcra");
        }
        static void Negyedik()
        {
            /* foreach (var a in autok)
             {
                 if (!Utvonalak.ContainsKey(a.Utvonal))
                 {
                     Utvonalak.Add(a.Utvonal, a.Hely);
                 }
                 else
                 {
                     Utvonalak[a.Utvonal] = Utvonalak[a.Utvonal] + a.Hely;
                 }
             }            
             foreach (var i in Utvonalak)
             {
                 if (i.Value > max)
                 {
                     max = i.Value;
                     utvonal = i.Key;
                 }
             }
             Console.WriteLine("4.feladat");
             Console.WriteLine($"A legtöbb férőhelyet ({max}-at) a {utvonal} " +
                 $"útvonalon ajánlották fel a hirdetők");*/
            var utvonalak = from a in autok
                            orderby a.Utvonal
                            group a by a.Utvonal into temp
                            select temp;
            int max = 0;
            string utvonal = "";
            foreach (var i in utvonalak)
            {
                //Console.WriteLine($"{i.Key} -> {i.Count()}");
                int ferohely = i.Sum(x => x.Hely);
                if (max < ferohely)
                {
                    max = ferohely;
                    utvonal = i.Key;
                }
            }
            Console.WriteLine("4. feladat");
            Console.WriteLine($"A legtöbb férőhelyet ({max}-at) a {utvonal} " +
                 $"útvonalon ajánlották fel a hirdetők");
        }
        static void Otodik()
        {
            Console.WriteLine("5. feladat");
            foreach (var igeny in igeny)
            {
                int i = 0;
                while (i < autok.Count &&
                  !(
                      igeny.Indulas == autok[i].Indulas &&
                      igeny.Cel == autok[i].Cel &&
                      igeny.Szemelyek <= autok[i].Hely
                   )
                )
                {
                    i++;
                }

                if (i < autok.Count)
                {
                    Console.WriteLine($"{igeny.Azonosito}=>{autok[i].Rendszam}");

                }
            }
        }
        static void Hatodik()
        {
            StreamWriter iro = new StreamWriter("utasuzenetek.txt");
            /* Az igénylő sikeres párosítás esetén megkapja az autó
            rendszámát és a sofőr telefonszámát, sikertelen párosítás esetén egy „Sajnos nem
            sikerült autót találni” üzenetet kap. */
            foreach (var igeny in igeny)
            {
                int i = 0;
                while (i < autok.Count &&
                  !(
                      igeny.Indulas == autok[i].Indulas &&
                      igeny.Cel == autok[i].Cel &&
                      igeny.Szemelyek <= autok[i].Hely
                   )
                )
                {
                    i++;
                }

                if (i < autok.Count)
                {
                   iro.WriteLine($"{igeny.Azonosito}: Rendszám: {autok[i].Rendszam}, Telefonszám: {autok[i].Telefonszam}");
                }
                else
                {
                    iro.WriteLine($"{igeny.Azonosito}: Sajnos nem sikerült autót találni");
                }
            }
                iro.Close();
        }
            static void Main(string[] args)
            {
                Beolvas();
                Harmadik();
                Negyedik();
                Otodik();
                Hatodik();
                Console.ReadKey();
            }
        }
    }
