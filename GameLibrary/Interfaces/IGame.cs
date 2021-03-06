﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Interfaces
{
    /// <summary>
    ///  event delegate for the on message event
    /// </summary>
    /// <param name="message"> message </param>
    public delegate void OnMessage(string message);

    /// <summary>
    ///  event delegate for the budget reduction event
    /// </summary>
    /// <param name="points"> budget points </param>
    /// <param name="message"> message </param>
    public delegate void OnBudget(int points);

    /// <summary>
    ///  interface implement the functionality of game
    /// </summary>
    public interface IGame
    {
        /// <summary>
        ///  event handler for the message event
        /// </summary>
        event OnMessage Message;

        /// <summary>
        ///  event handler for the budget reduction
        /// </summary>
        event OnBudget Budget;

        /// <summary>
        ///  storage for the attack flag
        /// </summary>
        bool IsAttack { get; }

        /// <summary>
        ///  storage for the assault flag
        /// </summary>
        bool IsAssault { get; }

        /// <summary>
        ///  storage for the economy crises flag
        /// </summary>
        bool IsEconomyCrises { get; }

        /// <summary>
        ///   method to calculate event
        /// </summary>
        /// <param name="country"> points used for country </param>
        /// <param name="economy"> points used for economy </param>
        /// <param name="terror"> points used for terror </param>
        /// <param name="research"> points used for personal </param>
        void Run(int country, int economy, int terror, int research);

        /// <summary>
        ///  method to get score points for current game
        /// </summary>
        double GameScore { get; }
    }
}
