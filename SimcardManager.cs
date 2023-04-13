using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimCard_Manager
{
	class SimcardManager //konténer osztály
	{
		private List<Simcard> simcards = new List<Simcard>();

		public void HozzaadSimet(Simcard sim)
		{
			if (simcards.Contains(sim))
				throw new Exception("A sim már szerepel az adatbázisban!");

			simcards.Add(sim);
		}

		public List<Simcard> Listaz()
		{
			return simcards;
		}

		public int EgyenlegLekerdez(int id)
		{
			foreach (Simcard sim in simcards)
			{
				if(sim.Id == id)
				{
					return sim.Egyenleg;
				}
			}
			return 0;
		}

	}
}
