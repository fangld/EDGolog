using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;
using Planning.Collections;
using Planning.ContextExtensions;

namespace Planning
{
    public class Action : ConstContainer
    {
        #region Fields
        
        private Dictionary<string, Response> _responseDict;

        #endregion

        #region Properties

        public IReadOnlyDictionary<string, Response> ResponseDict
        {
            get { return _responseDict; }
        }

        #endregion

        #region Constructors

        public Action(PlanningParser.ActionDefineContext context, IReadOnlyDictionary<string, Event> eventDict, string[] constArray, StringDictionary assignment)
            : base(constArray)
        {
            Name = context.actionSymbol().GetText();
            GenerateResponses(context.responseDefine(),eventDict, assignment);
        }

        #endregion

        #region Methods

        private void GenerateResponses(IReadOnlyList<PlanningParser.ResponseDefineContext> context, IReadOnlyDictionary<string, Event> eventDict, StringDictionary assignment)
        {
            _responseDict = new Dictionary<string, Response>();
            foreach (var responseDefineContext in context)
            {
                HandleResponse(responseDefineContext, eventDict, assignment);
            }
        }

        private void HandleResponse(PlanningParser.ResponseDefineContext context, IReadOnlyDictionary<string, Event> eventDict, StringDictionary assignment)
        {
            if (context.PARAMETER() != null)
            {
                ResponseEnumerator enumerator = new ResponseEnumerator(context, eventDict, _responseDict, assignment);
                Algorithms.IterativeScanMixedRadix(enumerator);
            }
            else
            {
                Response response = new Response(context, eventDict, assignment);
                _responseDict.Add(response.FullName, response);
            }
        }

        #endregion
    }
}
