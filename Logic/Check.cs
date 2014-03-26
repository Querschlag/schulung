using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schulung.Logic
{
    /// <summary>
    ///  delegate for the attack event
    /// </summary>
    public delegate void OnAttack();

    /// <summary>
    ///  delegate for the assault event
    /// </summary>
    public delegate void OnAssault();

    /// <summary>
    ///  delegate for the economy crises event
    /// </summary>
    public delegate void OnEconomyCrises();

    /// <summary>
    ///  class to check game results
    /// </summary>
    public class Check
    {
        /// <summary>
        ///  event handler for the attack event
        /// </summary>
        public event OnAttack Attack;

        /// <summary>
        ///  event handler for the assault event
        /// </summary>
        public event OnAssault Assault;

        /// <summary>
        ///  event handler for the economy crises
        /// </summary>
        public event OnEconomyCrises EconomyCrises;

        /// <summary>
        ///  method to check for game events 
        /// </summary>
        /// <param name="game"> game to check for next event </param>
        public void CheckCurrentState(IGame game)
        {
            // check if attack
            if (game.IsAttack)
            {
                // check and raise event
                if (Attack != null) Attack();
            }
            
            // check if economy crises
            if (game.IsEconomyCrises)
            {
                // check and raise event
                if (EconomyCrises != null) EconomyCrises();
            }
            
            // check if assault
            if (game.IsAssault)
            {
                // check and raise event
                if (Assault != null) Assault();
            }
        }
    }
}
