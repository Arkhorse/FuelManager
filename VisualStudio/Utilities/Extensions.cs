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
		#region Unity Engine Extensions
		/* Most are from https://github.com/NuclearPowered/Reactor/blob/6eb0bf19c30733b78532dada41db068b2b247742/Reactor/Utilities/Extensions/UnityObjectExtensions.cs
		*/
		/// <summary>
		/// Stops <paramref name="obj"/> from being unloaded.
		/// </summary>
		/// <param name="obj">The object to stop from being unloaded.</param>
		/// <typeparam name="T">The type of the object.</typeparam>
		/// <returns>Passed <paramref name="obj"/>.</returns>
		/// <remarks><see href="https://github.com/NuclearPowered/Reactor/blob/6eb0bf19c30733b78532dada41db068b2b247742/Reactor/Utilities/Extensions/UnityObjectExtensions.cs#L30">Source</see></remarks>
		public static T DontUnload<T>(this T obj) where T : UnityEngine.Object
		{
			obj.hideFlags |= HideFlags.DontUnloadUnusedAsset;

			return obj;
		}
		/// <summary>
		/// Stops <paramref name="obj"/> from being destroyed.
		/// </summary>
		/// <param name="obj">The object to stop from being destroyed.</param>
		/// <typeparam name="T">The type of the object.</typeparam>
		/// <returns>Passed <paramref name="obj"/>.</returns>
		public static T DontDestroy<T>(this T obj) where T : UnityEngine.Object
		{
			obj.hideFlags |= HideFlags.HideAndDontSave;

			return obj.DontDestroyOnLoad();
		}
		/// <summary>
		/// Stops <paramref name="obj"/> from being destroyed on load.
		/// </summary>
		/// <param name="obj">The object to stop from being destroyed on load.</param>
		/// <typeparam name="T">The type of the object.</typeparam>
		/// <returns>Passed <paramref name="obj"/>.</returns>
		public static T DontDestroyOnLoad<T>(this T obj) where T : UnityEngine.Object
		{
			UnityEngine.Object.DontDestroyOnLoad(obj);

			return obj;
		}
		/// <summary>
		/// Makes it more simple to know if a panel is patchable.
		/// </summary>
		/// <remarks>
		/// <para>And allows me to change how this is calculated in one place.</para>
		/// <para> The <see cref="Panel_AutoReferenced"/> class is extended by panels as of <c>2.39</c>.</para>
		/// </remarks>
		/// <param name="panel">The panel to check.</param>
		/// <returns><see langword="true"/> if the panel is not null, <see langword="false"/> otherwise.</returns>
		public static bool IsPanelPatchable(this Panel_AutoReferenced panel)
		{
			return !(panel == null);
		}
	}
	#endregion
}
