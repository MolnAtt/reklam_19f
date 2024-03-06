using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;

namespace reklam_19f
{
	internal class Program
	{
		class Adat
		{
			public int nap;
			public string varos;
			public int db;

			public Adat(int nap, string varos, int db)
			{
				this.nap = nap;
				this.varos = varos;
				this.db = db;
			}
		}

		static void Main(string[] args)
		{
			List<Adat> lista = Beolvas("rendel.txt");
			Console.WriteLine("2. feladat:");
			Console.WriteLine($"A rendelések száma: {lista.Count}");
			Console.WriteLine($"3. feladat:");
			Console.Write($"Kérem, adjon meg egy napot: ");
			int input = int.Parse(Console.ReadLine());
			Console.WriteLine($"A rendelések száma az adott napon: {lista.Count(a => a.nap==input)}");
			Console.WriteLine($"4. feladat:");
			int f4 = Feladat4(lista);
			if (f4==0)
			{
				Console.WriteLine("Minden nap volt rendelésa reklámban nem érintett városból");
			}
			else
			{
				Console.WriteLine($"{f4} nap nem volt a reklámban nem érintett városból rendelés.");
			}
			Console.WriteLine($"5. feladat:");
			int max = lista.Max(a => a.db);
			Console.WriteLine($"A legnagyobb darabszám {max}, A rendelés napja {lista.First(a=> a.db == max).nap}");
			Console.WriteLine($"7. feladat:");

			Console.WriteLine($"A rendelt termékek darabszáma a 21. napon: PL: {osszes("PL", 21, lista)}, TV: {osszes("TV", 21, lista)}, NR:{osszes("NR", 21, lista)}");
			Console.WriteLine($"8. feladat:");
			Console.WriteLine($"Napok\t1..10\t11.20\t21..30");
			Console.WriteLine($"PL\t{F8("PL",1,10, lista)}\t{F8("PL", 11, 20, lista)}\t{F8("PL", 21, 30, lista)}");
			Console.WriteLine($"TV\t{F8("TV", 1, 10, lista)}\t{F8("TV", 11, 20, lista)}\t{F8("TV", 21, 30, lista)}");
			Console.WriteLine($"NR\t{F8("NR", 1, 10, lista)}\t{F8("NR", 11, 20, lista)}\t{F8("NR", 21, 30, lista)}");

			using (StreamWriter f = new StreamWriter("kampany.txt"))
			{
				f.WriteLine($"Napok\t1..10\t11.20\t21..30");
				f.WriteLine($"PL\t{F8("PL", 1, 10, lista)}\t{F8("PL", 11, 20, lista)}\t{F8("PL", 21, 30, lista)}");
				f.WriteLine($"TV\t{F8("TV", 1, 10, lista)}\t{F8("TV", 11, 20, lista)}\t{F8("TV", 21, 30, lista)}");
				f.WriteLine($"NR\t{F8("NR", 1, 10, lista)}\t{F8("NR", 11, 20, lista)}\t{F8("NR", 21, 30, lista)}");
			}


		}

		private static int F8(string varos, int mettol, int meddig, List<Adat> lista)
		{
			return lista.Where(a => a.varos == varos && a.nap <= meddig && mettol <= a.nap).Count();
		}

		private static int Feladat4(List<Adat> lista)
		{
			int db = 0;

			for (int i = 1; i <= 30; i++)
			{
				if (0==lista.Count(a => a.nap == i && a.varos == "NR"))
				{
					db++;
				}
			}
			return db;
		}


		private static int osszes(string varos, int nap, List<Adat> lista)
		{
			return lista.Where(a => a.nap == 21 && a.varos == varos).Sum(a=>a.db);
		}
		private static List<Adat> Beolvas(string fajlnev)
		{
			List<Adat> lista = new List<Adat>();
			foreach (string item in File.ReadAllLines(fajlnev))
			{
				string[] t = item.Split(' ');
				lista.Add(new Adat(int.Parse(t[0]), t[1], int.Parse(t[2])));
			}
			return lista;
		}
	}
}
