using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Interfaces
{
    /// <summary>
    ///  interface implement the data of each round
    /// </summary>
    public interface IRound
    {
        /// <summary>
        ///  storage for the country points
        /// </summary>
        double Points { get; }
    }
}
