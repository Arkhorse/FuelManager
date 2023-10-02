namespace FuelManager
{
    public static class BuildInfo
    {
        #region Mandatory
        /// <summary>The machine readable name of the mod (no special characters or spaces)</summary>
        public const string Name = "FuelManager";
        /// <summary>Who made the mod</summary>
        public const string Author = "The Illusion";
        /// <summary>Current version (Using Major.Minor.Build) </summary>
        public const string Version = "1.2.7";
        /// <summary>Name used on GUI's, like ModSettings</summary>
        public const string GUIName = "Fuel Manager";
        #endregion

        #region Optional
        /// <summary>What the mod does</summary>
        public const string Description = "Fuel Manager adds QOL features to assist with managing various fuel items. This includes all features of Better-Fuel-Management";
        /// <summary>Company that made it</summary>
        public const string Company = null;
        /// <summary>A valid download link</summary>
        public const string DownloadLink = "https://github.com/Arkhorse/FuelManager/releases/latest";
        /// <summary>Copyright info</summary>
        public const string Copyright = "2023";
        /// <summary>Trademark info</summary>
        public const string Trademark = null;
        /// <summary>Product name (should always be the same as the name)</summary>
        public const string Product = "FuelManager";
        /// <summary>Culture info</summary>
        public const string Culture = null;
        #endregion
    }
}
