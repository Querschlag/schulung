using GameLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Logic
{
    /// <summary>
    ///  class implement the functionality of dictionary danger
    /// </summary>
    public class DictionaryDanger : IDictionaryDanger
    {
        /// <summary>
        ///  storage for the danger
        /// </summary>
        public List<IRound> Dangers { get; private set; }

        /// <summary>
        ///  storage for the points
        /// </summary>
        public List<IRound> Points { get; private set; }

        /// <summary>
        ///  default constructor
        /// </summary>
        public DictionaryDanger()
        {
            // initialize list of dangers
            Dangers = new List<IRound>();

            // initialize list of points
            Points = new List<IRound>();
        }

        /// <summary>
        ///  method to calculate danger
        /// </summary>
        /// <returns> calculated danger </returns>
        public double CalculateDanger()
        {
            // return calculated danger
            return Round.CalculateMaximum(Dangers, 0.2);
        }

        /// <summary>
        ///  method to calculate points
        /// </summary>
        /// <returns> calculated points </returns>
        public double CalculatePoints()
        {
            // return calculated points
            return Round.CalculateMaximum(Points, 0.1);
        }

        /// <summary>
        ///  method to check if is too danger
        /// </summary>
        public bool IsTooDanger()
        {
            // return too danger flag
            return CalculateDanger() > CalculatePoints();
        }

        /// <summary>
        ///  method to get smalles object
        /// </summary>
        /// <param name="keys"> list of key value pairs </param>
        /// <returns> smalles object </returns>
        public static IDictionaryDanger GetDiffSmall(params IDictionaryDanger[] dictionaries)
        {
            // object to return 
            IDictionaryDanger result = null;

            // current difference
            double diff = double.MaxValue;

            // iteration over all objects
            foreach (IDictionaryDanger value in dictionaries)
            {
                // cast pair to dictionary danger
                DictionaryDanger dict = value as DictionaryDanger;

                // check dictionary
                if (dict != null)
                {
                    // calculate difference and check
                    double temp = dict.CalculatePoints() - dict.CalculateDanger();

                    // check difference
                    if (temp < diff)
                    {
                        // set diff
                        diff = temp;

                        // set result
                        result = value;
                    }
                }
            }

            // return result
            return result;
        }
    }
}
