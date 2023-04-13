using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimCard_Manager
{
	class SimcardExtra: Simcard
	{
		#region Property-k

		private int puk;

		public int Puk
		{
			get
			{
				return puk;
			}
			set
			{
				string pukTest = Convert.ToString(value);
				if (!(pukTest.Length == 6))
					throw new Exception("A PUK kód csak 4 számjegyből állhat!");

				puk = value;
			}
		}

		#endregion

		#region Konstruktor

		public SimcardExtra(int id, int pin, int puk) :base(id, pin)
        {
			this.Puk = puk;			
		}

        #endregion


    }
}
