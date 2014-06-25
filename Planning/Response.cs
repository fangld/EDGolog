using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public class Response : ConstContainer
    {
        #region Fields

        private List<Event>[] _eventListArray;

        #endregion

        #region Properties

        public int MaxPlausibilityDegree
        {
            get { return _eventListArray.Length; }
        }

        public IReadOnlyList<List<Event>> EventList
        {
            get { return _eventListArray; }
        }

        #endregion

        #region Constructors

        public Response(PlanningParser.ResponseDefineContext context, IReadOnlyDictionary<string, Event> eventDict, string[] constArray, Dictionary<string, string> assignment)
            : base(constArray)
        {
            Name = context.responseSymbol().GetText();
            GenerateEventList(context, eventDict, assignment);
        }

        #endregion

        #region Methods

        public void GenerateEventList(PlanningParser.ResponseDefineContext context, IReadOnlyDictionary<string, Event> eventDict, Dictionary<string, string> assignment)
        {
            var eventModelContext = context.eventModel();
            if (eventModelContext.LB() == null)
            {
                _eventListArray = new List<Event>[1];
                var eventList = HandleGdEvent(eventModelContext.gdEvent(), eventDict, assignment);
                _eventListArray[0] = eventList;
            }
            else
            {
                _eventListArray = new List<Event>[2];
                var eventList0 = HandleGdEvent(eventModelContext.plGdEvent(0).gdEvent(), eventDict, assignment, 0);
                _eventListArray[0] = eventList0;
                var eventList1 = HandleGdEvent(eventModelContext.plGdEvent(1).gdEvent(), eventDict, assignment, 1);
                _eventListArray[1] = eventList1;
            }
        }

        public List<Event> HandleGdEvent(PlanningParser.GdEventContext context, IReadOnlyDictionary<string, Event> eventDict, Dictionary<string, string> assignment, int plDegree = 0)
        {
            List<Event> result = new List<Event>();
            CUDDNode gdEventNode = GetCuddNode(context, eventDict, assignment);
            CUDD.Ref(gdEventNode);
            foreach (var e in eventDict.Values)
            {
                CUDDNode eventNode = CUDD.Var(e.CuddIndex);
                CUDDNode impliesNode = CUDD.Function.Implies(eventNode, gdEventNode);
                CUDD.Ref(impliesNode);
                if (impliesNode.Equals(CUDD.ONE))
                {
                    result.Add(e);
                }
                CUDD.Deref(impliesNode);
            }
            CUDD.Deref(gdEventNode);

            return result;
        }

        private CUDDNode GetCuddNode(PlanningParser.TermEventFormContext context, IReadOnlyDictionary<string, Event> eventDict, Dictionary<string, string> assignment)
        {
            CUDDNode result;
            if (context.eventSymbol() != null)
            {
                string eventFullName = GetFullName(context, assignment);
                if (eventDict.ContainsKey(eventFullName))
                {
                    Event e = eventDict[eventFullName];
                    int cuddIndex = e.CuddIndex;
                    result = CUDD.Var(cuddIndex);
                }
                else
                {
                    result = CUDD.ZERO;
                }
            }
            else
            {
                string firstTermString = Globals.TermHandler.GetString(context.term(0), assignment);
                string secondTermString = Globals.TermHandler.GetString(context.term(1), assignment);
                if (context.EQ() != null)
                {
                    result = firstTermString == secondTermString ? CUDD.ONE : CUDD.ZERO;
                }
                else if (context.NEQ() != null)
                {
                    result = firstTermString != secondTermString ? CUDD.ONE : CUDD.ZERO;
                }
                else
                {
                    int firstValue = int.Parse(firstTermString);
                    int secondValue = int.Parse(secondTermString);
                    if (context.LT() != null)
                    {
                        result = firstValue < secondValue ? CUDD.ONE : CUDD.ZERO;
                    }

                    else if (context.LEQ() != null)
                    {
                        result = firstValue <= secondValue ? CUDD.ONE : CUDD.ZERO;
                    }
                    else if (context.GT() != null)
                    {
                        result = firstValue > secondValue ? CUDD.ONE : CUDD.ZERO;
                    }
                    else
                    {
                        result = firstValue >= secondValue ? CUDD.ONE : CUDD.ZERO;
                    }
                }
            }

            return result;
        }

        private CUDDNode GetCuddNode(PlanningParser.GdEventContext context, IReadOnlyDictionary<string, Event> eventDict, Dictionary<string, string> assignment)
        {
            CUDDNode result;

            //Console.WriteLine("  context is null:{0}", context == null);
            //Console.WriteLine("  Context:{0}", context.GetText());
            if (context.termEventForm() != null)
            {
                result = GetCuddNode(context.termEventForm(), eventDict, assignment);
            }
            else if (context.AND() != null)
            {
                result = GetCuddNode(context.gdEvent()[0], eventDict, assignment);
                for (int i = 1; i < context.gdEvent().Count; i++)
                {
                    CUDDNode gdNode = GetCuddNode(context.gdEvent()[i], eventDict, assignment);
                    CUDDNode andNode = CUDD.Function.And(result, gdNode);
                    CUDD.Ref(andNode);
                    CUDD.Deref(result);
                    CUDD.Deref(gdNode);
                    result = andNode;
                }
            }
            else if (context.OR() != null)
            {
                result = GetCuddNode(context.gdEvent()[0], eventDict, assignment);
                for (int i = 1; i < context.gdEvent().Count; i++)
                {
                    CUDDNode gdNode = GetCuddNode(context.gdEvent()[i], eventDict, assignment);
                    CUDDNode orNode = CUDD.Function.Or(result, gdNode);
                    CUDD.Ref(orNode);
                    CUDD.Deref(result);
                    CUDD.Deref(gdNode);
                    result = orNode;
                }
            }
            else if (context.NOT() != null)
            {
                CUDDNode gdNode = GetCuddNode(context.gdEvent()[0], eventDict, assignment);
                result = CUDD.Function.Not(gdNode);
                CUDD.Ref(result);
                CUDD.Deref(gdNode);
            }
            else if (context.IMPLY() != null)
            {
                CUDDNode gdNode0 = GetCuddNode(context.gdEvent()[0], eventDict, assignment);
                CUDDNode gdNode1 = GetCuddNode(context.gdEvent()[1], eventDict, assignment);

                result = CUDD.Function.Implies(gdNode0, gdNode1);
                CUDD.Ref(result);
                CUDD.Deref(gdNode0);
                CUDD.Deref(gdNode1);
            }
            else
            {
                List<string> varNameList = new List<string>();

                List<List<string>> collection = new List<List<string>>();

                var listVariableContext = context.listVariable();
                do
                {
                    if (listVariableContext.VAR().Count != 0)
                    {
                        string type = listVariableContext.type() == null
                            ? PlanningType.ObjectType.Name
                            : listVariableContext.type().GetText();

                        foreach (var varNode in listVariableContext.VAR())
                        {
                            string varName = varNode.GetText();
                            varNameList.Add(varName);
                            List<string> constList = Globals.TermHandler.GetConstList(type);
                            collection.Add(constList);
                        }
                    }
                    listVariableContext = listVariableContext.listVariable();
                } while (listVariableContext != null);

                if (context.FORALL() != null)
                {
                    result = ScanVarList(context.gdEvent(0), eventDict, assignment, varNameList, collection, 0);
                }
                else
                {
                    result = ScanVarList(context.gdEvent(0), eventDict, assignment, varNameList, collection, 0, false);
                }
            }

            return result;
        }

        private CUDDNode ScanVarList(PlanningParser.GdEventContext context, IReadOnlyDictionary<string, Event> eventDict,
            Dictionary<string, string> assignment, IReadOnlyList<string> varNameList,
            IReadOnlyList<List<string>> collection, int currentLevel, bool isForall = true)
        {
            CUDDNode result = isForall ? CUDD.ONE : CUDD.ZERO;
            if (currentLevel != varNameList.Count)
            {
                for (int i = 0; i < collection[currentLevel].Count; i++)
                {
                    string value = collection[currentLevel][i];
                    string varName = varNameList[currentLevel];

                    if (!assignment.ContainsKey(varName))
                    {
                        assignment.Add(varName, value);
                    }
                    else
                    {
                        assignment[varName] = value;
                    }

                    CUDDNode gdNode = ScanVarList(context, eventDict, assignment, varNameList, collection,
                        currentLevel + 1, isForall);

                    CUDDNode invalidNode = isForall ? CUDD.ONE : CUDD.ZERO;
                    if (!gdNode.Equals(invalidNode))
                    {
                        CUDDNode temp = result;

                        result = isForall ? CUDD.Function.And(temp, gdNode) : CUDD.Function.Or(temp, gdNode);

                        CUDD.Ref(result);
                        CUDD.Deref(temp);
                        CUDD.Deref(gdNode);
                    }

                    CUDDNode terminalNode = isForall ? CUDD.ZERO : CUDD.ONE;
                    if (gdNode.Equals(terminalNode))
                    {
                        CUDD.Deref(result);
                        result = CUDD.ZERO;
                        break;
                    }
                }
            }
            else
            {
                result = GetCuddNode(context, eventDict, assignment);
            }
            return result;
        }

        #endregion
    }
}
