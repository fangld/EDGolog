using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;
using Planning.ContextExtensions;

namespace Planning
{
    public class Response : ConstContainer
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

        public Response(PlanningParser.ResponseDefineContext context, IReadOnlyDictionary<string, Event> eventDict, string[] constArray, StringDictionary assignment)
            : base(constArray)
        {
            Name = context.responseSymbol().GetText();
            //Console.WriteLine(FullName);
            _eventCollectionArray = context.eventModel().ToEventCollectionArray(eventDict, assignment);
        }

        public Response(PlanningParser.ResponseDefineContext context, IReadOnlyDictionary<string, Event> eventDict, StringDictionary assignment)
            : base(Globals.EmptyConstArray)
        {
            Name = context.responseSymbol().GetText();
            //Console.WriteLine(FullName);
            _eventCollectionArray = context.eventModel().ToEventCollectionArray(eventDict, assignment);
        }

        #endregion
    }
}
