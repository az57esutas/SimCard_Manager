using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimCard_Manager
{
	internal class Simcard
	{
		#region Property-k

		private int id;

		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		private int pin;

		public int Pin
		{
			get 
			{ 
				return pin; 
			}
			set 
			{
				string pinTest = Convert.ToString(value);
				if (!(pinTest.Length == 4))
					throw new Exception("A PIN kód csak 4 számjegyből állhat!");

				pin = value; 
			}
		}

		private bool activated;

		public bool Activated
		{
			get 
			{ 
				return activated; 
			}
			private set 
			{
				if (activated)
					throw new Exception("A kártya már aktiválva van!");

				activated = value; 
			}
		}

		private int egyenleg;

		public int Egyenleg
		{
			get 
			{ 
				return egyenleg; 
			}
			private set 
			{ 
				egyenleg = value; 
			}
		}

		private bool letiltott;

		public bool Letiltott
		{
			get
			{
				return letiltott;
			}
			set
			{
				letiltott = value;
			}
		}

		#endregion

		#region Konstruktor
		public Simcard(int id, int pin)
        {
			this.Id = id;
			this.Pin = pin;
			this.Activated = false;
			this.Egyenleg = 3600;
			this.Letiltott = false;
            
        }
		#endregion

		#region Metódusok

		//Kártya aktiválása: Csak 1szer lehetséges!
		public void Aktival(int pin)
		{
			if (!(pin == this.Pin))
				throw new Exception("A megadott Pin kód nem egyezik meg a kártya Pin kódjával!");

			if (Activated)
				throw new Exception("A kártya már aktiválásra került!");

			this.Activated = true;	
		}

		public bool PinCheck(int pin)
		{
			if (pin == this.Pin)
				return true;

			else return false;
		}

		
		//Kiíratás
		public override string ToString()
		{
			return string.Format("Id: {0} \t PIN: {1} \t Aktivált: {2} \t Egyenleg: {3} Ft \t Letiltva:{4} ",
				this.Id,
				this.Pin,
				this.Activated,
				this.Egyenleg,
				this.Letiltott
				);
		}


		#endregion

	}
}
