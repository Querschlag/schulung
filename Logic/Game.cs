using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace Schulung.Logic
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
    ///  class implement the functionality of the game
    /// </summary>
    public class Game : IGame
    {
        /// <summary>
        ///  storage for all point of the game
        /// </summary>
        private int _points = 0;

        /// <summary>
        ///  storage for the list of used events
        /// </summary>
        private List<string> _usedEvents = new List<string>();

        /// <summary>
        ///  event handler for the message event
        /// </summary>
        public event OnMessage Message;

        /// <summary>
        ///  event handler for the budget reduction
        /// </summary>
        public event OnBudget Budget;

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

            // increase country
            this._countryPoints += country - GetShareOf(economy);

            // increase economy
            this._economyPoints += (economy - GetShareOf(country)) - GetShareOf(terror);

            // increase terror
            this._terrorPoint += terror - GetShareOf(economy);

            // check attack, assault and economy crises
            if (this.IsAttack == false && this.IsAssault == false && this.IsEconomyCrises == false)
            {
                // calculate difference country points
                double diffCountry = this._countryPoints - this._dangerCountry;

                // calculate difference economy points
                double diffEconomy = this._economyPoints - this._dangerEconomy;

                // calculate difference terror points
                double diffTerror = this._terrorPoint - this._dangerTerror;

                // check if changes for the country points are lower then economy points and terror points
                if (diffCountry < diffEconomy && diffCountry < diffTerror)
                {
                    // check and raise message event
                    if (Message != null) Message(GetCountryMessage());
                }
                // check if changes for economy points are lower then country points and terror points
                else if (diffEconomy < diffCountry && diffEconomy < diffTerror)
                {
                    // check and raise message event
                    if (Message != null) Message(GetEconomyMessage());
                }
                // check if changes for the terror points are lower then country points and economy points
                else if (diffTerror < diffCountry && diffTerror < diffEconomy)
                {
                    // check and raise message event
                    if (Message != null) Message(GetTerrorMessage());
                }

                // get random event
                int randomEvent = _random.Next(0, 10);

                // check random event number
                if (randomEvent == 3)
                {
                    // new budget
                    double budget = this._points * this._random.Next(1, 4);

                    // increase budget
                    if (Budget != null) Budget((int)Math.Ceiling(budget));

                    // show message
                    if (Message != null) Message(MessageResource.GameEventIncreaseBudget);
                }
                else if (randomEvent == 5)
                {
                    // new budget
                    double budget = this._points / this._random.Next(1, 4);

                    // increase budget
                    if (Budget != null) Budget((int)Math.Ceiling(budget));

                    // show message
                    if (Message != null) Message(MessageResource.GameEventGovermentShutdown);
                }
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

            // list of game events
            List<string> events = new List<string>();

            // get resources of executing assembly
            ResourceReader reader = ReadResources();

            // iteration over all resources
            foreach (DictionaryEntry resource in reader)
            {
                // convert key to string
                string keyString = resource.Key == null ? string.Empty : resource.Key.ToString();

                // check key
                if (string.IsNullOrWhiteSpace(keyString) == false)
                {
                    // check key and value
                    if (keyString.StartsWith("GameEventCountry") && resource.Value != null)
                    {
                        // add game event
                        events.Add(resource.Value.ToString());
                    }
                }
            }

            // check if all events are already used
            if (events.TrueForAll(c => _usedEvents.Contains(c)) == false)
            {
                // iteration over all used events
                foreach (string eventString in _usedEvents)
                {
                    // remove used event
                    events.Remove(eventString);
                }
            }
            else
            {
                // iteration over all events
                foreach (string eventString in events)
                {
                    // remove used event
                    _usedEvents.Remove(eventString);
                }
            }

            // select random event
            result = events[this._random.Next(0, events.Count)];

            // add to used event
            this._usedEvents.Add(result);

            // return country message
            return result;
        }

        /// <summary>
        ///  method to read resources
        /// </summary>
        /// <returns> resourcen reader </returns>
        private ResourceReader ReadResources()
        {
            // return resource readern
            return new ResourceReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("Schulung.MessageResource.resources"));
        }

        /// <summary>
        ///  method to get economy message
        /// </summary>
        /// <returns> economy message </returns>
        private string GetEconomyMessage()
        {
            // economy message to return
            string result = string.Empty;

            // list of game events
            List<string> events = new List<string>();

            // get resources of executing assembly
            ResourceReader reader = ReadResources();

            // iteration over all resources
            foreach (DictionaryEntry resource in reader)
            {
                // convert key to string
                string keyString = resource.Key == null ? string.Empty : resource.Key.ToString();

                // check key
                if (string.IsNullOrWhiteSpace(keyString) == false)
                {
                    // check key and value
                    if (keyString.StartsWith("GameEventEconomy") && resource.Value != null)
                    {
                        // add game event
                        events.Add(resource.Value.ToString());
                    }
                }
            }

            // check if all events are already used
            if (events.TrueForAll(c => _usedEvents.Contains(c)) == false)
            {
                // iteration over all used events
                foreach (string eventString in _usedEvents)
                {
                    // remove used event
                    events.Remove(eventString);
                }
            }
            else
            {
                // iteration over all events
                foreach (string eventString in events)
                {
                    // remove used event
                    _usedEvents.Remove(eventString);
                }
            }

            // select random event
            result = events[this._random.Next(0, events.Count)];

            // add to used event
            this._usedEvents.Add(result);

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

            // list of game events
            List<string> events = new List<string>();

            // get resources of executing assembly
            ResourceReader reader = ReadResources();

            // iteration over all resources
            foreach (DictionaryEntry resource in reader)
            {
                // convert key to string
                string keyString = resource.Key == null ? string.Empty : resource.Key.ToString();

                // check key
                if (string.IsNullOrWhiteSpace(keyString) == false)
                {
                    // check key and value
                    if (keyString.StartsWith("GameEventTerror") && resource.Value != null)
                    {
                        // add game event
                        events.Add(resource.Value.ToString());
                    }
                }
            }

            // check if all events are already used
            if (events.TrueForAll(c => _usedEvents.Contains(c)) == false)
            {
                // iteration over all used events
                foreach (string eventString in _usedEvents)
                {
                    // remove used event
                    events.Remove(eventString);
                }
            }
            else
            {
                // iteration over all events
                foreach (string eventString in events)
                {
                    // remove used event
                    _usedEvents.Remove(eventString);
                }
            }

            // select random event
            result = events[this._random.Next(0, events.Count)];

            // add to used event
            this._usedEvents.Add(result);

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
            int maximum = (int)(Math.Round(this._researchPoints == 0 ? 1 : this._researchPoints, 4) * factor);

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
