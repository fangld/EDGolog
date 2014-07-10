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
            //Console.WriteLine(FullName);
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
                var listVariableContext = context.listVariable();

                IReadOnlyList<IList<string>> collection = listVariableContext.GetCollection();
                IReadOnlyList<string> variableNameList = listVariableContext.GetVariableNameList();
                ScanMixedRadix(context, variableNameList, collection, eventDict, assignment);
            }
            else
            {
                Response response = new Response(context, eventDict, assignment);
                _responseDict.Add(response.FullName, response);
            }
        }

        private void ScanMixedRadix(PlanningParser.ResponseDefineContext context, IReadOnlyList<string> variableNameList, IReadOnlyList<IList<string>> collection, IReadOnlyDictionary<string, Event> eventDict, StringDictionary assignment)
        {
            int count = collection.Count;
            string[] scanArray = new string[count];
            int[] index = new int[count];
            int[] maxIndex = new int[count];
            Parallel.For(0, count, i => maxIndex[i] = collection[i].Count);

            do
            {
                for (int i = 0; i < count; i ++)
                {
                    scanArray[i] = collection[i][index[i]];
                    string varName = variableNameList[i];
                    if (assignment.ContainsKey(varName))
                    {
                        assignment[varName] = scanArray[i];
                    }
                    else
                    {
                        assignment.Add(varName, scanArray[i]);
                    }
                }

                Response response = new Response(context, eventDict, scanArray, assignment);
                _responseDict.Add(response.FullName, response);

                int j = count - 1;
                while (j != -1)
                {
                    if (index[j] == maxIndex[j] - 1)
                    {
                        index[j] = 0;
                        j--;
                        continue;
                    }
                    break;
                }
                if (j == -1)
                    return;
                index[j]++;
            } while (true);
        }

        #endregion
    }
}
