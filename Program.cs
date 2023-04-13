using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

/*59.FELADAT
 Készítsük el egy SIM kártyakezelő egyszerűsített szimulációját.A kártyakezelő egyszerű és továbbfejlesztett
 SIM kártyákat kezel, amelynek sorszáma és PIN kódja van.Az egyszerű SIM kártya csak egyszer aktiválható,
 képes a PIN kód ellenőrzésére és többszöri hibás próbálkozás után képes érvényteleníteni saját magát. A
 továbbfejlesztett SIM kártya PUK kóddal is rendelkezik, melynek segítségével érvénytelenített állapotból
 újraaktiválható.A kártyakezelő legyen képes SIM kártyák létrehozására, tárolására és a helyes PIN(illetve PUK)
 kód megadását követően jelezze a felhasználónak, hogy az adott kártya aktív és használható. A kártyakezelő
legyen képes továbbá az egyes SIM kártyákhoz tartozó egyenleg kezelésére.
*/

namespace SimCard_Manager
{
	internal class Program
	{
		static void Main(string[] args)
		{
			SimcardManager manager = new SimcardManager();

			#region Fájl-ból beolvasás és példányok létehozása!
			//Beolvasás Fájlból:
			StreamReader fajl = new StreamReader("simek.txt");
			while (!fajl.EndOfStream)
			{
				string sor = fajl.ReadLine();
				string[] adatok = sor.Split(';');

				Simcard x;
				switch (adatok.Length)
				{
					//egyszerű sim kártya
					case 4:
						x = new Simcard(int.Parse(adatok[0]), int.Parse(adatok[1]));
						break;

					//bővitett sim kártya
					case 5:
						x = new SimcardExtra(int.Parse(adatok[0]), int.Parse(adatok[1]), int.Parse(adatok[4]));
						break;


					default:
						throw new Exception(string.Format("Nem lehet értelmezni ezt az inputot: {0}", sor));
				}
				manager.HozzaadSimet(x);
			}
			fajl.Close();
			#endregion


			string Esc = "n";
			do
			{
				Console.Clear();
				Console.WriteLine("A Sim kártyák adatai:");
				foreach (var sim in manager.Listaz())
				{
					Console.WriteLine(sim);
				}

				Console.WriteLine("");
				Console.WriteLine("Add meg az AKTIVÁLANDÓ SIMkártya azonosítóját:");
				int azonosito = int.Parse(Console.ReadLine());

				foreach (var sim in manager.Listaz())
				{
					bool tovabb = true;
					int szaml = 0;

					if (azonosito == sim.Id)
					{
						while ((tovabb) && (szaml < 5))
						{
							if (sim.Letiltott)
							{
								Console.WriteLine("A Sim le van tiltva!");
                                Console.WriteLine("");

								if (sim is SimcardExtra) 
								{
									SimcardExtra esim = sim as SimcardExtra;
                                    Console.WriteLine("Adja meg a PUK kódot a feloldáshoz:");
									int userPuk = int.Parse(Console.ReadLine());
                                    if(esim.Puk == userPuk)
									{
										esim.Letiltott = false;
										Console.WriteLine("A Letiltás feloldásra került!");
									}
									else
                                        Console.WriteLine("A PUK kód hibás! Kezdd el elölről! :)");
                                }									
                                break;
							}
							else
							{
								//Pinkód ellenőrzés:
								Console.WriteLine("Add meg a Pin kódot!");
								int pin = int.Parse(Console.ReadLine());
								if (pin == sim.Pin)
								{
									szaml++;
									Console.Clear();
									Console.WriteLine("{0} azonosítójú", sim.Id);
									Console.WriteLine("Simkártya feloldva és használható!");
									Console.WriteLine("Felhasználható egyenleged: {0} Ft", sim.Egyenleg);
									Console.WriteLine("");
									sim.Aktival(pin);
									tovabb = false;
								}
								else
								{
									szaml++;
									tovabb = true;
									Console.Clear();
									Console.WriteLine("");
									Console.WriteLine("Rossz Pinkód! Maradt {0} próbálkozásod", 5 - szaml);

									if (szaml == 5)
									{
										Console.WriteLine(" +++++++++++++++++++");
										Console.WriteLine("    SIM kártya Zárolva!");
										Console.WriteLine("        +++++++++++++++++++");
										Console.WriteLine("");
										Console.WriteLine("       ++++++++++++++++");
										Console.WriteLine("     ++||||+++++++||||++++");
										Console.WriteLine("    ++_____+++++++_____+++++");
										Console.WriteLine("   ++++(_)+++++++++(_)+++++++");
										Console.WriteLine("  ++++++++++++//++++++++++++++");
										Console.WriteLine("   ++++++++++++++++++++++++++");
										Console.WriteLine("   ++++++/   FUCK     /++++++");
										Console.WriteLine("    +++++/___________/+++++");
										Console.WriteLine("     +++++++++++++++++++++");
										Console.WriteLine("      +++++++++++++++++++");
										Console.WriteLine("          ++++++++++++");
										Console.WriteLine("            +++++++++++");
										Console.WriteLine("              ++++++++++");

										// A sim kártya letiltott állapotba került!
										sim.Letiltott = true;
									}
								}
							}
						}
					}
				}

				Console.WriteLine("Szeretnél újabb SIM-et Aktiválni? ( i / n )");
			} while (Esc != Console.ReadLine());

            Console.WriteLine("");
            Console.WriteLine("EGYENLEGLEKÉRDEZÉS:");
            Console.WriteLine("Add meg a lekérdezni kívánt SIM azonosítóját:");
			int userid = int.Parse(Console.ReadLine());
            Console.WriteLine("Az egyenleged: {0} Ft", manager.EgyenlegLekerdez(userid));
            		
			Console.ReadKey();
		}
	}
}
