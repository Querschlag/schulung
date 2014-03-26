using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Schulung
{
    /// <summary>
    ///  class implement the functionality of the game
    /// </summary>
    public class Run
    {
        /// <summary>
        ///  storage for all point of the game
        /// </summary>
        private int _points = 0;

        /// <summary>
        ///  paramized constructor
        /// </summary>
        /// <param name="points"> maximum points for the game </param>
        public Run(int points)
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

            // set default reserach points
            this._researchPoints = points;
        }

        /// <summary>
        ///  storage for the country points
        /// </summary>
        private int _countryPoints = 0;

        /// <summary>
        ///  storage for the economy points
        /// </summary>
        private int _economyPoints = 0;

        /// <summary>
        ///  storage for the terror points
        /// </summary>
        private int _terrorPoint = 0;

        /// <summary>
        ///  storage for the research points
        /// </summary>
        private double _researchPoints = 0;

        /// <summary>
        ///  storage for the country danger
        /// </summary>
        private int _dangerCountry = 0;

        /// <summary>
        ///  storage for the economy danger
        /// </summary>
        private int _dangerEconomy = 0;

        /// <summary>
        ///  storage for the terror danger
        /// </summary>
        private int _dangerTerror = 0;

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
        public void Main(int country, int economy, int terror, int research)
        {
            // decrease research
            this._researchPoints -= ((double)research / _points);

            // check point
            if (this._researchPoints < 1) this._researchPoints = 1;

            // change danger country
            this._dangerCountry += this.GenerateRandom();

            // change danger economy
            this._dangerEconomy += this.GenerateRandom();

            // change danger terror
            this._dangerTerror += this.GenerateRandom();

            // increase country
            this._countryPoints += country;

            // increase economy
            this._economyPoints += economy;

            // increase terror
            this._terrorPoint += terror;
        }

        /// <summary>
        ///  method to generate random number
        /// </summary>
        /// <returns> random number </returns>
        private int GenerateRandom()
        {
            // generate random number
            return this._random.Next(0, ((int)Math.Floor(this._researchPoints) + 1));
        }
    }
}
