using GameLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Logic
{
    /// <summary>
    ///  class implement the functionality of danger
    /// </summary>
    public static class Danger
    {
        /// <summary>
        ///  storage for the random danger generator
        /// </summary>
        private static Random _random = new Random();

        /// <summary>
        ///  storage for the multiply factor
        /// </summary>
        private static double _factor = Math.Pow(10, 5);

        /// <summary>
        ///  method to get danger level
        /// </summary>
        /// <param name="maximum"> maximum </param>
        /// <returns> danger level </returns>
        public static double Get(double maximum)
        {
            // calculate maximum
            int max = (int)(maximum == 0 ? _factor : maximum * _factor);

            // return random danger generator
            return _random.Next((int)_factor, max) / _factor;
        }
    }
}
