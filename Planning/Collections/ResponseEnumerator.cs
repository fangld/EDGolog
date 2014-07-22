using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;
using Planning.ContextExtensions;

namespace Planning.Collections
{
    public class ResponseEnumerator : MixedRadixEnumeratorWithAssignment<PlanningParser.ResponseDefineContext>
    {
        #region Fields

        private IDictionary<string, Response> _responseDict;

        private IReadOnlyDictionary<string, Event> _eventDict;

        private List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>> _condEffectList;

        private CUDDNode _conditionNode;

        #endregion

        #region Properties

        public List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>> CondEffectList
        {
            get { return _condEffectList; }
        }

        #endregion

        #region Constructors

        public ResponseEnumerator(PlanningParser.ResponseDefineContext context,IReadOnlyDictionary<string, Event> eventDict, IDictionary<string, Response> responseDict,StringDictionary assignment)
            : base(
                context, context.listVariable().GetCollection(), context.listVariable().GetVariableNameList(),
                assignment)
        {
            _condEffectList = new List<Tuple<CUDDNode, Tuple<Predicate, bool>[]>>();
            _eventDict = eventDict;
            _responseDict = responseDict;
        }

        #endregion

        #region Overriden Methods

        public override void Execute()
        {
            Response response = new Response(_context, _eventDict, _assignment, _scanArray);
            _responseDict.Add(response.FullName, response);
        }

        #endregion
    }
}
