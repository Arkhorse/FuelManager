using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MelonLoader.Utils;

namespace FuelManager
{
	internal class Setup
	{
		public static List<string> PossibleMCFiles =
		[
			Path.Combine(MelonEnvironment.ModsDirectory, "FuelManager.modcomponent"),
			Path.Combine(MelonEnvironment.ModsDirectory, "FuelManager.ModdersGearToolbox.modcomponent"),
			Path.Combine(MelonEnvironment.ModsDirectory, "FuelManager.Shared.modcomponent")
		];
		public static bool VerifyMCFiles()
		{
			if (!File.Exists(PossibleMCFiles[2]))
			{
				Main.Logger.Log($"Main shared MC file is missing, please read the readme AGAIN and install this mod properly!", FlaggedLoggingLevel.Critical);
				return false;
			}

			if (File.Exists(PossibleMCFiles[0]) && File.Exists(PossibleMCFiles[1]))
			{
				Main.Logger.Log($"Both the default and ModdersGearToolbox MC files are present! Please read the readme AGAIN and install this mod properly!", FlaggedLoggingLevel.Critical);
				return false;
			}

			return true;
		}
	}
}
