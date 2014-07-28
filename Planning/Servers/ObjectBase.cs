using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PAT.Common.Classes.CUDDLib;

namespace Planning.Servers
{
    public class ObjectBase
    {
        #region Fields

        private Dictionary<string, bool> _predBooleanMap;

        private IReadOnlyDictionary<string, Predicate> _predicateDict;

        #endregion

        #region Properties

        public CUDDNode CurrentCuddNode { get; private set; }

        #endregion

        #region Constructors

        public ObjectBase(ServerProblem problem)
        {
            _predBooleanMap = new Dictionary<string, bool>();
            _predicateDict = problem.PredicateDict;
            foreach (var predicateFullName in _predicateDict.Keys)
            {
                bool value = problem.TruePredSet.Contains(predicateFullName);
                _predBooleanMap.Add(predicateFullName, value);
            }
            CurrentCuddNode = GetKbNode();
        }

        #endregion

        #region Events

        public EventHandler<Tuple<IReadOnlyDictionary<string, bool>, Event>> ObjectBaseChanged;

        #endregion

        #region Methods

        /// <summary>
        /// [ REFS: 'result', DEREFS: none ]
        /// </summary>
        /// <returns></returns>
        private CUDDNode GetKbNode()
        {
            //OK
            CUDDNode result = CUDD.Constant(1);

            foreach (var pair in _predBooleanMap)
            {
                string name = pair.Key;
                int index = _predicateDict[name].PreviousCuddIndex;
                CUDDNode literal = pair.Value ? CUDD.Var(index) : CUDD.Function.Not(CUDD.Var(index));
                result = CUDD.Function.And(result, literal);
            }
            return result;
        }

        public Response GetActualResponse(Action action)
        {
            Response result;

            List<Response> responseList = new List<Response>();
            //Console.WriteLine("Kb:");
            //CUDD.Print.PrintMinterm(kbNode);

            foreach (var pair in action.ResponseDict)
            {
                //OK
                CUDDNode responsePrecondition = pair.Value.EventModel.KnowPrecondition;
                //Console.WriteLine("Event collection name: {0}", pair.Value.FullName);
                //CUDD.Print.PrintMinterm(responsePrecondition);

                CUDD.Ref(CurrentCuddNode);
                CUDDNode impliesNode = CUDD.Function.Implies(CurrentCuddNode, responsePrecondition);
                //Console.WriteLine("Implies : {0}", impliesNode.Equals(CUDD.ONE));
                //Console.ReadLine();

                if (impliesNode.Equals(CUDD.ONE))
                {
                    responseList.Add(pair.Value);
                }
                CUDD.Deref(impliesNode);
            }

            int selectiveRepsonseIndex = Globals.Random.Next(responseList.Count);
            //Console.WriteLine("Count of response list: {0}", responseList.Count);

            result = responseList[selectiveRepsonseIndex];
            return result;
        }

        public Event GetActualEvent(Response response)
        {
            Event result = null;

            IReadOnlyList<Event> eventList = response.EventModel.KnowEventList;

            foreach (var e in eventList)
            {
                CUDD.Ref(CurrentCuddNode);
                CUDD.Ref(e.Precondition);
                CUDDNode impliesNode = CUDD.Function.Implies(CurrentCuddNode, e.Precondition);
                if (impliesNode.Equals(CUDD.ONE))
                {
                    CUDD.Deref(impliesNode);
                    result = e;
                    break;
                }
                CUDD.Deref(impliesNode);
            }
            return result;
        }

        public void Update(Event e)
        {
            var literalList = GenerateLiteralList(CurrentCuddNode, e);
            UpdateByLiteralList(literalList);
            CUDD.Deref(CurrentCuddNode);
            CurrentCuddNode = GetKbNode();

            if (ObjectBaseChanged != null)
            {
                var tuple = new Tuple<IReadOnlyDictionary<string, bool>, Event>(_predBooleanMap, e);
                ObjectBaseChanged(this, tuple);
            }
        }

        private List<Tuple<Predicate, bool>> GenerateLiteralList(CUDDNode node, Event e)
        {
            //OK
            List<Tuple<Predicate, bool>> result = new List<Tuple<Predicate, bool>>();
            foreach (var cEffect in e.CondEffect)
            {
                CUDD.Ref(node);
                CUDD.Ref(cEffect.Item1);
                CUDDNode impliesNode = CUDD.Function.Implies(node, cEffect.Item1);
                if (impliesNode.Equals(CUDD.ONE))
                {
                    result.AddRange(cEffect.Item2);
                }
                CUDD.Deref(impliesNode);
            }
            return result;
        }

        private void UpdateByLiteralList(List<Tuple<Predicate, bool>> literalList)
        {
            foreach (var literal in literalList)
            {
                string predicateFullName = literal.Item1.ToString();
                _predBooleanMap[predicateFullName] = literal.Item2;
            }
            GetKbNode();
        }

        public void ShowInfo()
        {
            Console.WriteLine("Object base:");
            foreach (var pair in _predBooleanMap)
            {
                Console.WriteLine("  Predicate: {0}, Value: {1}", pair.Key, pair.Value);
            }
        }

        #endregion
    }
}
