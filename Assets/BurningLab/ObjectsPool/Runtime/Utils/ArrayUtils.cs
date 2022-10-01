using System.Collections.Generic;

namespace BurningLab.ObjectsPool.Utils
{
    /// <summary>
    /// Utils for easy worn an arrays and lists.
    /// </summary>
    public static class ArrayUtils
    {
        /// <summary>
        /// Mix array.
        /// </summary>
        /// <param name="data">Array to mix.</param>
        /// <typeparam name="T">Array items type.</typeparam>
        /// <returns>Mixed array.</returns>
        public static List<T> MixArray<T>(List<T> data)
        {
            System.Random random = new System.Random();
            for (int i = data.Count - 1; i >= 1; i--)
            {
                int j = random.Next(i + 1);
                (data[j], data[i]) = (data[i], data[j]);
            }

            return data;
        }
    }
}