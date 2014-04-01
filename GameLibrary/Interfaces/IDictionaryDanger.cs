using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Interfaces
{
    /// <summary>
    ///  interface implement the functionality of dictionary danger entity
    /// </summary>
    public interface IDictionaryDanger
    {
        /// <summary>
        ///  storage for the danger
        /// </summary>
        List<IRound> Dangers { get; }

        /// <summary>
        ///  storage for the points
        /// </summary>
        List<IRound> Points { get; }

        /// <summary>
        ///  method to check if is too danger
        /// </summary>
        bool IsTooDanger();

        /// <summary>
        ///  method to calculate danger
        /// </summary>
        /// <returns> calculated danger </returns>
        double CalculateDanger();

        /// <summary>
        ///  method to calculate points
        /// </summary>
        /// <returns> calculated points </returns>
        double CalculatePoints();
    }
}
