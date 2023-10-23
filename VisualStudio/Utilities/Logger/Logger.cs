using System.Text;

namespace FuelManager.Utilities.Logger
{
    public class Logging
    {
        public static void LogStarter()                                                     => Melon<Main>.Logger.Msg($"Mod loaded with v{BuildInfo.Version}");
        public static void LogSeperator(params object[] parameters)                         => Melon<Main>.Logger.Msg("==============================================================================", parameters);
        public static void LogIntraSeparator(string message, params object[] parameters)    => Melon<Main>.Logger.Msg($"=========================   {message}   =========================", parameters);

        public static void Log(string message, params object[] parameters)                  => Melon<Main>.Logger.Msg(message, parameters);
        public static void LogDebug(string message, params object[] parameters)             => Melon<Main>.Logger.Msg($"[DEBUG] {message}", parameters);
        public static void LogWarning(string message, params object[] parameters)           => Melon<Main>.Logger.Warning(message, parameters);
        public static void LogError(string message, params object[] parameters)             => Melon<Main>.Logger.Error(message, parameters);
        public static void LogException(string message, Exception exception, params object[] parameters)
        {
            StringBuilder sb = new();

            sb.Append("[EXCEPTION]");
            sb.Append(message);
            sb.AppendLine(exception.Message);

            Melon<Main>.Logger.Error(sb.ToString(), Color.red, parameters);
        }

        public static void LogUpdate(string[] message)
        {
            LogSeperator();
            for (int i = 0; i < message.Length; i++)
            {
                Log(message[i]);
            }
            LogSeperator();
        }
    }
}