using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schulung.Logic
{
    /// <summary>
    ///  event delegate for the on message event
    /// </summary>
    /// <param name="message"> message </param>
    public delegate void OnMessage(string message);

    /// <summary>
    ///  class implement the functionality of the game
    /// </summary>
    public class Game : IGame
    {
        /// <summary>
        ///  storage for all point of the game
        /// </summary>
        private int _points = 0;

        /// <summary>
        ///  event handler for the message event
        /// </summary>
        public event OnMessage Message;

        /// <summary>
        ///  paramized constructor
        /// </summary>
        /// <param name="points"> maximum points for the game </param>
        public Game(int points)
        {
            // store points
            this._points = points;

            // create random generator
            this._random = new Random();

            // set default contry points
            this._countryPoints = points;

            // set default economy points
            this._economyPoints = points;

            // set default terror points
            this._terrorPoint = points;

            // set research points
            this._researchPoints = points;
        }

        /// <summary>
        ///  storage for the country points
        /// </summary>
        private double _countryPoints = 0;

        /// <summary>
        ///  storage for the economy points
        /// </summary>
        private double _economyPoints = 0;

        /// <summary>
        ///  storage for the terror points
        /// </summary>
        private double _terrorPoint = 0;

        /// <summary>
        ///  storage for the research points
        /// </summary>
        private double _researchPoints = 0;

        /// <summary>
        ///  storage for the country danger
        /// </summary>
        private double _dangerCountry = 0;

        /// <summary>
        ///  storage for the economy danger
        /// </summary>
        private double _dangerEconomy = 0;

        /// <summary>
        ///  storage for the terror danger
        /// </summary>
        private double _dangerTerror = 0;

        /// <summary>
        ///  storage for the random generator
        /// </summary>
        private Random _random;

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

            // calculate danger
            this.CalculateDanger();

            // calculate changes country points
            double changesCountryPoints = country - GetShareOf(economy);

            // increase country
            this._countryPoints += changesCountryPoints;

            // calculate changes economy points
            double changesEconomyPoints = (economy - GetShareOf(country)) - GetShareOf(terror);

            // increase economy
            this._economyPoints += changesEconomyPoints;

            // calculate changes terror points
            double changesTerrorPoints = terror - GetShareOf(economy);

            // increase terror
            this._terrorPoint += changesTerrorPoints;

            // check if changes for the country points are lower then economy points and terror points
            if (changesCountryPoints < changesEconomyPoints && changesCountryPoints < changesTerrorPoints)
            {
                // check and raise message event
                if (Message != null) Message(GetCountryMessage());
            }
            // check if changes for economy points are lower then country points and terror points
            else if (changesEconomyPoints < changesCountryPoints && changesEconomyPoints < changesTerrorPoints)
            {
                // check and raise message event
                if (Message != null) Message(GetEconomyMessage());
            }
            // check if changes for the terror points are lower then country points and economy points
            else if (changesTerrorPoints < changesCountryPoints && changesTerrorPoints < changesEconomyPoints)
            {
                // check and raise message event
                if (Message != null) Message(GetTerrorMessage());
            }
        }

        /// <summary>
        ///  method to get country message
        /// </summary>
        /// <returns> country message </returns>
        private string GetCountryMessage()
        {
            // country message to return
            string result = string.Empty;

            // return country message
            return result;
        }

        /// <summary>
        ///  method to get economy message
        /// </summary>
        /// <returns> economy message </returns>
        private string GetEconomyMessage()
        {
            // economy message to return
            string result = string.Empty;

            // return economy message
            return result;
        }

        /// <summary>
        ///  method to get terror message
        /// </summary>
        /// <returns> economy message </returns>
        private string GetTerrorMessage()
        {
            // terror message to return
            string result = string.Empty;

            // return terror message
            return result;
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
        ///  method to calculate danger
        /// </summary>
        private void CalculateDanger()
        {
            // change danger country
            this._dangerCountry += this.GenerateRandom();

            // change danger economy
            this._dangerEconomy += this.GenerateRandom();

            // change danger terror
            this._dangerTerror += this.GenerateRandom();
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
        ///  method to generate random number
        /// </summary>
        /// <returns> random number </returns>
        private double GenerateRandom()
        {
            // calculation factor
            int factor = (int)Math.Pow(10, 4);

            // maximum value
            int maximum = (int)(Math.Round(this._researchPoints == 0 ? 1: this._researchPoints, 4) * factor);
            
            // generate random number
            double result = (double)this._random.Next(factor, maximum + 1) / factor;

            // return result
            return result;
        }

        /// <summary>
        ///  storage for the attack flag
        /// </summary>
        public bool IsAttack 
        {
            get { return (this._dangerCountry > this._countryPoints); }
        }

        /// <summary>
        ///  storage for the assault flag
        /// </summary>
        public bool IsAssault
        {
            get { return (this._dangerTerror > this._terrorPoint); }
        }

        /// <summary>
        ///  storage for the economy crises flag
        /// </summary>
        public bool IsEconomyCrises
        {
            get { return (this._dangerEconomy > this._economyPoints); }
        }
    }
}
