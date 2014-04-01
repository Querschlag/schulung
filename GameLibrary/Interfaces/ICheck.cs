using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameLibrary.Logic;

namespace GameLibrary.Interfaces
{
    /// <summary>
    ///  interface implement the functionality of check
    /// </summary>
    public interface ICheck
    {
        /// <summary>
        ///  event handler for the attack event
        /// </summary>
        event OnAttack Attack;

        /// <summary>
        ///  event handler for the assault event
        /// </summary>
        event OnAssault Assault;

        /// <summary>
        ///  event handler for the economy crises
        /// </summary>
        event OnEconomyCrises EconomyCrises;

        /// <summary>
        ///  method to check for game events 
        /// </summary>
        /// <param name="game"> game to check for next event </param>
        void CheckCurrentState(IGame game);
    }
}
