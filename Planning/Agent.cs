using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Planning
{
    public class Agent : IEquatable<Agent>
    {
        #region Fields

        private List<Action> _actionList;

        private List<Observation> _observationList;

        #endregion

        #region Properties
        public string Name { get; set; }

        public IReadOnlyList<Action> ActionList
        {
            get { return _actionList; }
        }

        public IReadOnlyList<Observation> ObservationList
        {
            get { return _observationList; }
        }

        #endregion

        #region Constructors

        public Agent(string name)
        {
            Name = name;
            _actionList = new List<Action>();
            _observationList = new List<Observation>();
        }

        #endregion

        #region Methods

        public void AddAction(Action action)
        {
            _actionList.Add(action);
        }

        public void AddObservation(Observation observation)
        {
            _observationList.Add(observation);
        }

        #endregion

        public bool Equals(Agent other)
        {
            return Name == other.Name;
        }
    }
}
