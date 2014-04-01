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
    ///  class implement the functionality of message
    /// </summary>
    public class GameMessages
    {
        /// <summary>
        ///  storage for the list of used events
        /// </summary>
        private List<string> _usedEvents = new List<string>();

        /// <summary>
        ///  storage for the random number generator
        /// </summary>
        private Random _random;

        /// <summary>
        ///  storage for the instance
        /// </summary>
        private static GameMessages _instance;

        /// <summary>
        ///  default constructor
        /// </summary>
        public GameMessages()
        {
            // create random generator
            _random = new Random();
        }

        /// <summary>
        ///  storage for the instance
        /// </summary>
        public static GameMessages Instance
        {
            get
            {
                return _instance ?? (_instance = new GameMessages());
            }
        }

        /// <summary>
        ///  method to get message
        /// </summary>
        /// <returns> message </returns>
        public string Get(string start)
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
                    if (keyString.StartsWith(start) && resource.Value != null)
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
            return new ResourceReader(Assembly.GetExecutingAssembly().GetManifestResourceStream("GameLibrary.MessageResource.resources"));
        }
    }
}
