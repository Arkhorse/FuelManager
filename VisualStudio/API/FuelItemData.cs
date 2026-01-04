using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuelManager.API
{
	public class FuelItemData : MonoBehaviour
	{
		public ItemLiquidVolume CurrentLiters;
		public ItemLiquidVolume MaxLiters;
		public bool Empty { get; set; } = false;
		public bool Full { get; set; } = false;
	}
}
