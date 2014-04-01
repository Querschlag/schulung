using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary.Interfaces;

namespace GameLibrary.Logic
{
    /// <summary>
    ///  class implement the functionality of round
    /// </summary>
    public class Round : IRound
    {
        /// <summary>
        ///  paramized constructor
        /// </summary>
        /// <param name="points"> points of the round </param>
        public Round(double points)
        {
            // store points
            this.Points = points;
        }

        /// <summary>
        ///  storage for the points
        /// </summary>
        public double Points { get; private set; }

        /// <summary>
        ///  method to calculate maximum
        /// </summary>
        /// <param name="rounds"> list of rounds </param>
        /// <param name="factor"> change factor of value</param>
        public static double CalculateMaximum(List<IRound> rounds, double factor)
        {
            // check factor
            if (factor <= 0) throw new ArgumentNullException();

            // calculated maximum 
            double result = 0;

            // round index
            double roundMuliplier = 1;

            // iteration over all rounds
            for (int index = rounds.Count() - 1; index >= 0; index--)
            {
                // add to result
                result += rounds[index].Points * roundMuliplier;

                // decrease round multiplier
                roundMuliplier -= factor;
            }

            // check count
            if (rounds.Count() > 0)
            {
                // divide through rounds count
                result /= rounds.Count();
            }

            // return maximum
            return result;
        }
    }
}
