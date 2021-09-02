using UnityEngine;

namespace GoToApps.ObjectsPool.Utils
{
    /// <summary>
    /// Unity console tool.
    /// </summary>
    public static class UnityConsole
    {
        /// <summary>
        /// Draw log record to console.
        /// </summary>
        /// <param name="className">Class name</param>
        /// <param name="methodName">Method name</param>
        /// <param name="message">Message</param>
        public static void PrintLog(string className, string methodName, string message)
        {
#if UNITY_EDITOR
            Debug.Log($"{{<b><color=white>GoTo-</color><color=lime>Apps</color></b>}} => [{className}] - (<color=yellow>{methodName}</color>) -> {message}");
#else
            Debug.Log($"{{GoTo-Apps}} => [{className}] - ({methodName}) -> {message}");
#endif
        }
        
        /// <summary>
        /// Draw log record to console.
        /// </summary>
        /// <param name="className">Class name</param>
        /// <param name="methodName">Method name</param>
        /// <param name="message">Message</param>
        /// <param name="context">GameObject context.</param>>
        public static void PrintLog(string className, string methodName, string message, GameObject context)
        {
#if UNITY_EDITOR
            Debug.Log($"{{<b><color=white>GoTo-</color><color=lime>Apps</color></b>}} => [{className}] - (<color=yellow>{methodName}</color>) -> {message}", context);
#else
            Debug.Log($"{{GoTo-Apps}} => [{className}] - ({methodName}) -> {message}");
#endif
        }
    }
}