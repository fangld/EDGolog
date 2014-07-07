using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;

namespace Planning
{
    public class Observation : ConstContainer
    {
        #region Fields

        private EventCollection[] _eventCollectionArray;

        #endregion

        #region Properties

        public int MaxPlausibilityDegree
        {
            get { return _eventCollectionArray.Length; }
        }

        public IReadOnlyList<EventCollection> EventCollectionList
        {
            get { return _eventCollectionArray; }
        }

        #endregion

        #region Constructors

        public Observation(PlanningParser.ObsDefineContext context, string[] constArray) : base(constArray)
        {

        }

        #endregion
    }
}
