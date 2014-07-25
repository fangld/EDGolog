using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning.ContextExtensions
{
    public static class GdEventContextExtension
    {
        public static EventCollection ToEventCollection(this PlanningParser.GdEventContext context, IReadOnlyDictionary<string, Event> eventDict, StringDictionary assignment)
        {
            List<Event> eventList = new List<Event>();
            CUDDNode gdEventNode = GetCuddNode(context, eventDict, assignment);

            foreach (var e in eventDict.Values)
            {
                CUDDNode eventNode = CUDD.Var(e.CuddIndex);
                CUDD.Ref(gdEventNode);
                CUDDNode impliesNode = CUDD.Function.Implies(eventNode, gdEventNode);
                if (impliesNode.Equals(CUDD.ONE))
                {
                    eventList.Add(e);
                }
                CUDD.Deref(impliesNode);
            }
            CUDD.Deref(gdEventNode);

            EventCollection result = new EventCollection(eventList);
            return result;
        }

        private static CUDDNode GetCuddNode(PlanningParser.TermEventFormContext context, IReadOnlyDictionary<string, Event> eventDict, StringDictionary assignment)
        {
            CUDDNode result;
            if (context.eventSymbol() != null)
            {
                string eventFullName = ConstContainer.GetFullName(context, assignment);
                if (eventDict.ContainsKey(eventFullName))
                {
                    Event e = eventDict[eventFullName];
                    int cuddIndex = e.CuddIndex;
                    result = CUDD.Var(cuddIndex);
                }
                else
                {
                    result = CUDD.ZERO;
                    CUDD.Ref(result);
                }
            }
            else
            {
                string firstTermString = Globals.TermInterpreter.GetString(context.term(0), assignment);
                string secondTermString = Globals.TermInterpreter.GetString(context.term(1), assignment);
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
                CUDD.Ref(result);
            }

            return result;
        }

        private static CUDDNode GetCuddNode(PlanningParser.GdEventContext context, IReadOnlyDictionary<string, Event> eventDict, StringDictionary assignment)
        {
            CUDDNode result;

            if (context.termEventForm() != null)
            {
                result = GetCuddNode(context.termEventForm(), eventDict, assignment);
            }
            else if (context.AND() != null)
            {
                var gdEventContextList = context.gdEvent();
                result = GetCuddNode(gdEventContextList[0], eventDict, assignment);

                if (!result.Equals(CUDD.ZERO))
                {
                    for (int i = 1; i < gdEventContextList.Count; i++)
                    {
                        CUDDNode gdNode = GetCuddNode(gdEventContextList[i], eventDict, assignment);
                        if (gdNode.Equals(CUDD.ZERO))
                        {
                            CUDD.Deref(result);
                            result = CUDD.ZERO;
                            CUDD.Ref(result);
                            break;
                        }
                        result = CUDD.Function.And(result, gdNode);
                    }
                }
            }
            else if (context.OR() != null)
            {
                var gdEventContextList = context.gdEvent();
                result = GetCuddNode(gdEventContextList[0], eventDict, assignment);

                if (!result.Equals(CUDD.ONE))
                {
                    for (int i = 1; i < gdEventContextList.Count; i++)
                    {
                        CUDDNode gdNode = GetCuddNode(gdEventContextList[i], eventDict, assignment);
                        if (gdNode.Equals(CUDD.ONE))
                        {
                            CUDD.Deref(result);
                            result = CUDD.ONE;
                            CUDD.Ref(result);
                            break;
                        }
                        result = CUDD.Function.Or(result, gdNode);
                    }
                }
            }
            else if (context.NOT() != null)
            {
                CUDDNode gdEventNode = GetCuddNode(context.gdEvent()[0], eventDict, assignment);
                result = CUDD.Function.Not(gdEventNode);
            }
            else if (context.IMPLY() != null)
            {
                var gdEventContextList = context.gdEvent();
                CUDDNode gdEventNode0 = GetCuddNode(gdEventContextList[0], eventDict, assignment);
                CUDDNode gdEventNode1 = GetCuddNode(gdEventContextList[1], eventDict, assignment);
                result = CUDD.Function.Implies(gdEventNode0, gdEventNode1);
            }
            else
            {
                var listVariableContext = context.listVariable();
                var collection = listVariableContext.GetCollection();
                var varNameList = listVariableContext.GetVariableNameList();

                bool isForall = context.FORALL() != null;
                result = RecursiveScanMixedRaio(context.gdEvent(0), eventDict, varNameList, collection, assignment, 0, isForall);
            }

            return result;
        }

        private static CUDDNode RecursiveScanMixedRaio(PlanningParser.GdEventContext context,
            IReadOnlyDictionary<string, Event> eventDict, IReadOnlyList<string> variableNameList,
            IReadOnlyList<IList<string>> collection, StringDictionary assignment, int currentLevel, bool isForall = true)
        {
            CUDDNode result;
            if (currentLevel != variableNameList.Count)
            {
                string variableName = variableNameList[currentLevel];
                result = isForall ? CUDD.ONE : CUDD.ZERO;
                CUDDNode equalNode = isForall ? CUDD.ZERO : CUDD.ONE;
                CUDD.Ref(result);
                Func<CUDDNode, CUDDNode, CUDDNode> boolFunc;
                if (isForall)
                {
                    boolFunc = CUDD.Function.And;
                }
                else
                {
                    boolFunc = CUDD.Function.Or;
                }

                foreach (string value in collection[currentLevel])
                {
                    assignment[variableName] = value;

                    CUDDNode gdNode = RecursiveScanMixedRaio(context, eventDict, variableNameList, collection,
                        assignment, currentLevel + 1, isForall);

                    if (gdNode.Equals(equalNode))
                    {
                        CUDD.Deref(result);
                        result = equalNode;
                        CUDD.Ref(result);
                        break;
                    }

                    result = boolFunc(result, gdNode);
                }
            }
            else
            {
                result = GetCuddNode(context, eventDict, assignment);
            }
            return result;
        }
    }
}
