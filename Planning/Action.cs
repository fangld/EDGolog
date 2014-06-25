using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public class Action : ConstContainer
    {
        #region Fields
        
        private Dictionary<string, Response> _respDict;

        #endregion

        #region Properties

        public IReadOnlyDictionary<string, Response> RespDict
        {
            get { return _respDict; }
        }

        #endregion

        #region Constructors

        public Action(PlanningParser.ActionDefineContext context, IReadOnlyDictionary<string, Event> eventDict, string[] constArray, Dictionary<string, string> assignment) : base(constArray)
        {
            Name = context.actionSymbol().GetText();
            GenerateResponses(context.responseDefine(),eventDict, assignment);

        }

        #endregion

        #region Methods

        private void GenerateResponses(IReadOnlyList<PlanningParser.ResponseDefineContext> context, IReadOnlyDictionary<string, Event> eventDict, Dictionary<string, string> assignment)
        {
            _respDict = new Dictionary<string, Response>(context.Count);
            foreach (var responseDefineContext in context)
            {
                HandleResponse(responseDefineContext, eventDict, assignment);
            }
        }

        private void HandleResponse(PlanningParser.ResponseDefineContext context, IReadOnlyDictionary<string, Event> eventDict, Dictionary<string, string> assignment)
        {
            if (context.PARM() != null)
            {
                var listVariableContext = context.listVariable();

                IReadOnlyList<List<string>> collection = listVariableContext.GetCollection();
                IReadOnlyList<string> varNameList = listVariableContext.GetVarNameList();
                ScanMixedRadix(varNameList, collection, assignment, context, eventDict);
            }
            else
            {
                string[] emptyConstArray = new string[0];
                Response response = new Response(context, eventDict, emptyConstArray, assignment);
                _respDict.Add(response.FullName, response);
            }
        }

        private void ScanMixedRadix(IReadOnlyList<string> varNameList, IReadOnlyList<List<string>> collection,
            Dictionary<string, string> assignment, PlanningParser.ResponseDefineContext context,
            IReadOnlyDictionary<string, Event> eventDict)
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
                    string varName = varNameList[i];
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
                _respDict.Add(response.FullName, response);

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
