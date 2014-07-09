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
            Console.WriteLine(FullName);
            var eventModelContext = context.eventModel();
            if (eventModelContext.LB() == null)
            {
                _eventCollectionArray = new EventCollection[1];
                _eventCollectionArray[0] = GenerateEventCollection(eventModelContext.gdEvent(), eventDict, assignment);
                Console.WriteLine("Finishing event collection 0 precondition");
                Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(_eventCollectionArray[0].Precondition));
                Console.WriteLine("Finishing event collection 0 partial successor state axiom");
                Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(_eventCollectionArray[0].PartialSuccessorStateAxiom));
            }
            else
            {
                _eventCollectionArray = new EventCollection[2];
                _eventCollectionArray[0] = GenerateEventCollection(eventModelContext.plGdEvent(0).gdEvent(), eventDict, assignment);

                Console.WriteLine("Finishing event collection 0 precondition");
                Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(_eventCollectionArray[0].Precondition));
                Console.WriteLine("Finishing event collection 0 partial successor state axiom");
                Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(_eventCollectionArray[0].PartialSuccessorStateAxiom));

                _eventCollectionArray[1] = GenerateEventCollection(eventModelContext.plGdEvent(1).gdEvent(), eventDict, assignment, 1);

                Console.WriteLine("Finishing event collection 1 precondition");
                Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(_eventCollectionArray[1].Precondition));
                Console.WriteLine("Finishing event collection 1 partial successor state axiom");
                Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(_eventCollectionArray[1].PartialSuccessorStateAxiom));
                //Console.ReadLine();
            }
            Console.WriteLine();
        }

        private EventCollection GenerateEventCollection(PlanningParser.GdEventContext context, IReadOnlyDictionary<string, Event> eventDict, Dictionary<string, string> assignment, int plDegree = 0)
        {
            List<Event> eventList = new List<Event>();
            CUDDNode gdEventNode = GetCuddNode(context, eventDict, assignment);
            CUDD.Ref(gdEventNode);
            foreach (var e in eventDict.Values)
            {
                CUDDNode eventNode = CUDD.Var(e.CuddIndex);
                CUDDNode impliesNode = CUDD.Function.Implies(eventNode, gdEventNode);
                if (impliesNode.Equals(CUDD.ONE))
                {
                    eventList.Add(e);
                }
                CUDD.Deref(impliesNode);
            }

            EventCollection result = new EventCollection(eventList);
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
                    CUDD.Ref(result);
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
                CUDD.Ref(result);
            }

            return result;
        }

        private CUDDNode GetCuddNode(PlanningParser.GdEventContext context, IReadOnlyDictionary<string, Event> eventDict, Dictionary<string, string> assignment)
        {
            CUDDNode result;

            if (context.termEventForm() != null)
            {
                result = GetCuddNode(context.termEventForm(), eventDict, assignment);
            }
            else if (context.AND() != null)
            {
                result = CUDD.ONE;
                CUDD.Ref(result);
                for (int i = 0; i < context.gdEvent().Count; i++)
                {
                    CUDDNode gdNode = GetCuddNode(context.gdEvent()[i], eventDict, assignment);
                    if (gdNode.Equals(CUDD.ZERO))
                    {
                        CUDD.Deref(result);
                        result = CUDD.ZERO;
                        break;
                    }
                    CUDDNode andNode = CUDD.Function.And(result, gdNode);
                    result = andNode;
                }
            }
            else if (context.OR() != null)
            {
                result = CUDD.ZERO;
                CUDD.Ref(result);
                for (int i = 0; i < context.gdEvent().Count; i++)
                {
                    CUDDNode gdNode = GetCuddNode(context.gdEvent()[i], eventDict, assignment);
                    if (gdNode.Equals(CUDD.ONE))
                    {
                        CUDD.Deref(result);
                        result = CUDD.ONE;
                        break;
                    }
                    CUDDNode orNode = CUDD.Function.Or(result, gdNode);
                    result = orNode;
                }
            }
            else if (context.NOT() != null)
            {
                CUDDNode gdNode = GetCuddNode(context.gdEvent()[0], eventDict, assignment);
                result = CUDD.Function.Not(gdNode);
            }
            else if (context.IMPLY() != null)
            {
                CUDDNode gdNode0 = GetCuddNode(context.gdEvent()[0], eventDict, assignment);
                CUDDNode gdNode1 = GetCuddNode(context.gdEvent()[1], eventDict, assignment);
                result = CUDD.Function.Implies(gdNode0, gdNode1);
            }
            else
            {
                var listVariableContext = context.listVariable();
                var collection = listVariableContext.GetCollection();
                var varNameList = listVariableContext.GetVariableNameList();

                bool isForall = context.FORALL() != null;
                result = ScanVariableList(context.gdEvent(0), eventDict, assignment, varNameList, collection, 0, isForall);
            }

            return result;
        }

        private CUDDNode ScanVariableList(PlanningParser.GdEventContext context, IReadOnlyDictionary<string, Event> eventDict,
            Dictionary<string, string> assignment, IReadOnlyList<string> variableNameList,
            IReadOnlyList<List<string>> collection, int currentLevel, bool isForall = true)
        {
            CUDDNode result;// = isForall ? CUDD.ONE : CUDD.ZERO;
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
                    if (assignment.ContainsKey(variableName))
                    {
                        assignment[variableName] = value;
                    }
                    else
                    {
                        assignment.Add(variableName, value);
                    }

                    CUDDNode gdNode = ScanVariableList(context, eventDict, assignment, variableNameList, collection,
                        currentLevel + 1);

                    if (gdNode.Equals(equalNode))
                    {
                        CUDD.Deref(result);
                        result = equalNode;
                        break;
                    }

                    CUDDNode boolNode = boolFunc(result, gdNode);
                    result = boolNode;
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
