using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Il2CppTLD.UI;

namespace FuelManager.Utilities
{
	public static class Extensions
	{
		public static bool IsPanelPatchable(this Panel_AutoReferenced panel)
		{
			return !(panel == null);
		}
	}
}
