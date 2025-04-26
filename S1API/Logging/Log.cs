#if MONOMELON || IL2CPPMELON
using MelonLoader
#else
using BepInEx.Logging;
#endif

namespace S1API.Logging
{
    /// <summary>
    /// Centralized Logging class that handles both BepInEx and MelonLoader logging.
    /// </summary>
    public class Log
    {
#if MONOMELON || IL2CPPMELON
        private readonly MelonLogger.Instance _loggerInstance;
#else
        private readonly ManualLogSource _loggerInstance;
#endif

        public Log(string sourceName)
        {
#if MONOMELON || IL2CPPMELON
            _loggerInstance = new MelonLogger.Instancer(sourceName);
#else
            _loggerInstance = new ManualLogSource(sourceName);
#endif
        }

        /// <summary>
        /// Logs a message with Info level
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Msg(string message)
        {
#if MONOMELON || IL2CPPMELON
            _loggerInstance.Msg(message);
#else
            _loggerInstance.LogInfo(message);
#endif
        }

        /// <summary>
        /// Logs a message with Warning level
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Warning(string message)
        {
#if MONOMELON || IL2CPPMELON
            _loggerInstance.Warning(message);
#else
            _loggerInstance.LogWarning(message);
#endif
        }

        /// <summary>
        /// Logs a message with Error level
        /// </summary>
        /// <param name="message">Message to log</param>
        public void Error(string message)
        {
#if MONOMELON || IL2CPPMELON
            _loggerInstance.Error(message);
#else
            _loggerInstance.LogError(message);
#endif
        }

        /// <summary>
        /// Logs a message with Fatal level
        /// </summary>
        /// <param name="message">Message to log</param>
        public void BigError(string message)
        {
#if MONOMELON || IL2CPPMELON
            _loggerInstance.BigError(message);
#else
            _loggerInstance.LogFatal(message);
#endif
        }
    }
}
