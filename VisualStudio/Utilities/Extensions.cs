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

		// This is implemented in a fashion that is easily expandable. Instead of checking many things and then returning a value based on those, those things are checked in the body of the method, logged and then the 'results' int is incremented if applicable. This allows for a part of the method to be unchanged even when adding new checks
		/// <summary>
		/// Makes it more simple to know if a panel is patchable. Will also output a reason if applicable
		/// </summary>
		/// <remarks>
		/// <para>And allows me to change how this is calculated in one place.</para>
		/// <para> The <see cref="Panel_AutoReferenced"/> class is extended by panels as of <c>2.39</c>.</para>
		/// </remarks>
		/// <param name="panel">The panel to check.</param>
		/// <param name="reason">The reason the panel is not patchable, should this be false</param>
		/// <returns><see langword="true"/> if the panel is not null, <see langword="false"/> otherwise.</returns>
		public static bool IsPanelPatchable<Panel_AutoReferenced>(this Panel_AutoReferenced panel, out string reason)
		{
			StringBuilder sb = new();
			int results = 0;

			if (panel == null)
			{
				sb.AppendLine("Current PANEL is null");
				results++;
			}

			reason = sb.ToString();
			return results == 0;
		}
	}
	#endregion
}
