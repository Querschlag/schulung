using GameLibrary.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace GameLibrary.Logic
{
    /// <summary>
    ///  class implement the functionality of the game
    /// </summary>
    public class Game : IGame
    {
        /// <summary>
        ///  storage for the country danger
        /// </summary>
        private IDictionaryDanger _country = new DictionaryDanger();

        /// <summary>
        ///  storage for the economy danger
        /// </summary>
        private IDictionaryDanger _economy = new DictionaryDanger();

        /// <summary>
        ///  storage for the terror danger
        /// </summary>
        private IDictionaryDanger _terror = new DictionaryDanger();

        /// <summary>
        ///  storage for the points
        /// </summary>
        private int _points;

        /// <summary>
        ///  event handler for the message event
        /// </summary>
        public event OnMessage Message;

        /// <summary>
        ///  event handler for the budget reduction
        /// </summary>
        public event OnBudget Budget;
                
        /// <summary>        
        ///  storage for the random generator        
        /// </summary>        
        private Random _random;

        /// <summary>
        ///  storage for the research points
        /// </summary>
        private double _researchPoints = 0;

        /// <summary>
        ///  paramized constructor
        /// </summary>
        /// <param name="points"> maximum points for the game </param>
        public Game(int points)
        {
            // store points
            this._points = points;

            // store default research points
            this._researchPoints = points;

            // create random number generator
            _random = new Random();

            // add default country points
            _country.Points.Add(new Round(2 * points));

            // add default economy points
            _economy.Points.Add(new Round(2 * points));

            // add default terror points
            _terror.Points.Add(new Round(2 * points));
        }

        /// <summary>
        ///   method to calculate event
        /// </summary>
        /// <param name="country"> points used for country </param>
        /// <param name="economy"> points used for economy </param>
        /// <param name="terror"> points used for terror </param>
        /// <param name="research"> points used for personal </param>
        public void Run(int country, int economy, int terror, int research)
        {
            // calculate research
            CalculateResearch(research);

            // store country danger
            _country.Dangers.Add(new Round(Danger.Get(this._researchPoints)));

            // store country points
            _country.Points.Add(new Round(country - GetShareOf(economy)));

            // store economy danger
            _economy.Dangers.Add(new Round(Danger.Get(this._researchPoints)));

            // store economy points
            _economy.Points.Add(new Round((economy - GetShareOf(country)) - GetShareOf(terror)));

            // store terror danger
            _terror.Dangers.Add(new Round(Danger.Get(this._researchPoints)));

            // store terror points
            _terror.Points.Add(new Round(terror - GetShareOf(economy)));

            // check attack, assault and economy crises
            if (this.IsAttack == false && this.IsAssault == false && this.IsEconomyCrises == false)
            {
                // get message object
                IDictionaryDanger danger = DictionaryDanger.GetDiffSmall(_country, _economy, _terror);

                // get message string
                string msg = danger == _country ? "GameEventCountry" : danger == _economy ? "GameEventEconomy" : "GameEventTerror";

                // check and raise message event
                if(Message != null) Message(GameMessages.Instance.Get(msg.ToString()));

                // get random event
                int randomEvent = _random.Next(0, 15);
                
                // check random event number
                if (randomEvent == 3)
                {
                    // new budget
                    double budget = this._points * this._random.Next(2, 4);
                    
                    // increase budget
                    if (Budget != null) Budget((int)Math.Ceiling(budget));
                    
                    // show message
                    if (Message != null) Message(MessageResource.GameEventIncreaseBudget);
                }
                else if (randomEvent == 5)
                {
                    // new budget
                    double budget = this._points / this._random.Next(2, 4);
                    
                    // increase budget
                    if (Budget != null) Budget((int)Math.Ceiling(budget));
                    
                    // show message
                    if (Message != null) Message(MessageResource.GameEventGovermentShutdown);
                }
                else
                {
                    // reset budget
                    if (Budget != null) Budget(this._points);
                }
            }

        }

        /// <summary>
        ///  method to calculate research 
        /// </summary>
        /// <param name="research"> new research value </param>
        private void CalculateResearch(int research)
        {
            // check research
            if (research > 0)
            {
                // decrease research
                this._researchPoints = this._points - Math.Log((this._researchPoints + research), _points);
                
                // check point
                if (this._researchPoints <= 0) this._researchPoints = 1;
            }
        }

        /// <summary>        
        ///  method to get share of the value        
        /// </summary>        
        /// <param name="value"> value to get share of </param>        
        private double GetShareOf(int value)        
        {            
            // return share of            
            return value * ((double)_random.Next(0, 5) / 10.00);        
        }

        /// <summary>
        ///  storage for the attack flag
        /// </summary>
        public bool IsAttack
        {
            get { return this._country.IsTooDanger(); }
        }

        /// <summary>
        ///  storage for the assault flag
        /// </summary>
        public bool IsAssault
        {
            get { return this._economy.IsTooDanger(); }
        }

        /// <summary>
        ///  storage for the economy crises flag
        /// </summary>
        public bool IsEconomyCrises
        {
            get { return this._terror.IsTooDanger(); }
        }

        /// <summary>
        ///  method to get score points for current game
        /// </summary>
        public double GameScore
        {
            get { return this._country.CalculateDanger() + this._economy.CalculateDanger() + this._terror.CalculateDanger(); }
        }

    }
}
