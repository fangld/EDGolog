using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public class Event : VariableContainer
    {
        #region Fields

        //protected Dictionary<string, AbstractPredicate> _abstractPredDict;

        private List<Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>>> _effect;

        #endregion

        #region Properties
        
        public CUDDNode Precondition { get; set; }

        public IReadOnlyList<Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>>> Effect
        {
            get { return _effect; }
        }

        #endregion

        #region Constructors

        private Event()
        {
            _effect = new List<Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>>>();
        }

        #endregion

        #region Methods

        public static Event From(PlanningParser.EventDefineContext context)
        {
            Event result = new Event();
            return result;
        }

        #region Methods for generating precondition

        protected void GeneratePrecondition(PlanningParser.EventDefineContext context, IReadOnlyDictionary<string, GroundPredicate> gndPredDict)
        {
            Precondition = CUDD.ONE;

            if (context.PRE() != null)
            {
                if (context.emptyOrPreGD().gd() != null)
                {
                    Dictionary<string, string> varDict = new Dictionary<string, string>();
                    Precondition = GetCuddNode(context.emptyOrPreGD().gd(), gndPredDict, varDict);
                }
            }
        }

        //private string GetTermString(PlanningParser.TermContext context)
        //{
        //    string result = string.Empty;
        //    if (context.NAME() != null)
        //    {
        //        Globals.TermHandler.GetString()
        //    }
        //    else if (context.VAR() != null)
        //    {
                
        //    }
        //    else if (context.INTEGER() != null)
        //    {
        //        result = context.INTEGER().GetText();
        //    }
        //    else if (context.MINUS() != null && context.term().Count == 1)
        //    {
                
        //    }
        //    else if (context.MINUS() != null && context.term().Count == 2)
        //    {

        //    }
        //    else
        //    {
                
        //    }

        //    return result;
        //}

        private CUDDNode GetCuddNode(PlanningParser.TermAtomFormContext context, IReadOnlyDictionary<string, GroundPredicate> gndPredDict, Dictionary<string, string> assignment, bool isPrevious = true)
        {
            CUDDNode result = CUDD.ONE;
            if (context.pred() != null)
            {
                string predFullName = GetFullName(context);
                GroundPredicate gndPred = gndPredDict[predFullName];
                int cuddIndex = isPrevious ? gndPred.PreviousCuddIndex : gndPred.SuccessiveCuddIndex;
                result = CUDD.Var(cuddIndex);
            }
            else 
            {
                string firstTermString = Globals.TermHandler.GetString(context.term(0), assignment);
                string secondTermString = Globals.TermHandler.GetString(context.term(1), assignment);
                int firstValue = int.Parse(firstTermString);
                int secondValue = int.Parse(secondTermString);
                if (context.EQ() != null)
                {
                    result = firstValue == secondValue ? CUDD.ONE : CUDD.ZERO;
                }
                else if (context.NEQ() != null)
                {
                    result = firstValue != secondValue ? CUDD.ONE : CUDD.ZERO;
                }
                else if (context.LT() != null)
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

            return result;
        }

        private CUDDNode GetCuddNode(PlanningParser.GdContext context, IReadOnlyDictionary<string, GroundPredicate> gndPredDict, Dictionary<string, string> assignment, bool isPrevious = true)
        {
            CUDDNode result = null;

            if (context.termAtomForm() != null)
            {
                result = GetCuddNode(context.termAtomForm(), gndPredDict, assignment, isPrevious);
            }
            else if (context.AND() != null)
            {
                result = GetCuddNode(context.gd()[0], gndPredDict, assignment, isPrevious);
                for (int i = 1; i < context.gd().Count; i++)
                {
                    CUDDNode gdNode = GetCuddNode(context.gd()[i], gndPredDict, assignment, isPrevious);
                    CUDDNode andNode = CUDD.Function.And(result, gdNode);
                    CUDD.Ref(andNode);
                    CUDD.Deref(result);
                    CUDD.Deref(gdNode);
                    result = andNode;
                }
            }
            else if (context.OR() != null)
            {
                result = GetCuddNode(context.gd()[0], gndPredDict, assignment, isPrevious);
                for (int i = 1; i < context.gd().Count; i++)
                {
                    CUDDNode gdNode = GetCuddNode(context.gd()[i], gndPredDict, assignment, isPrevious);
                    CUDDNode orNode = CUDD.Function.Or(result, gdNode);
                    CUDD.Ref(orNode);
                    CUDD.Deref(result);
                    CUDD.Deref(gdNode);
                    result = orNode;
                }
            }
            else if (context.NOT() != null)
            {
                CUDDNode gdNode = GetCuddNode(context.gd()[0], gndPredDict, assignment, isPrevious);
                result = CUDD.Function.Not(gdNode);
                CUDD.Ref(result);
                CUDD.Deref(gdNode);
            }
            else if (context.IMPLY() != null)
            {
                CUDDNode gdNode0 = GetCuddNode(context.gd()[0], gndPredDict, assignment, isPrevious);
                CUDDNode gdNode1 = GetCuddNode(context.gd()[1], gndPredDict, assignment, isPrevious);

                result = CUDD.Function.Implies(gdNode0, gdNode1);
                CUDD.Ref(result);
                CUDD.Deref(gdNode0);
                CUDD.Deref(gdNode1);
            }
            else (context.FORALL() != null)
            {
                List<string> varNameList = null;
                
                List<List<string>> collection = new List<List<string>>();

                for (int i = 0; i < container.Count; i++)
                {
                    Tuple<string, string> variable = container.VariableList[i];
                    List<string> constList = Globals.TermHandler.GetConstList(variable.Item2);
                        //_typeConstantListMap[variable.Item2];
                    collection.Add(constList);
                }

                if (context.FORALL() != null)
                {
                    List<List<string>> collection = null;
                    result = ScanMixedRadix(CUDD.ONE, context.gd(0), gndPredDict, varNameList, collection, assignment,
                        isPrevious);
                }
                else
                {
                    
                }
            }

            return result;
        }

        private BuildContainer(Planning)

        private CUDDNode ScanMixedRadix(CUDDNode initialNode, PlanningParser.GdContext context, IReadOnlyDictionary<string, GroundPredicate> gndPredDict, IReadOnlyList<string> varNameList, IReadOnlyList<List<string>> collection, Dictionary<string, string> assignment, bool isPrevious)
        {
            CUDDNode result = initialNode;

            int count = collection.Count;
            //string[] scanArray = new string[count];
            int[] index = new int[count];
            int[] maxIndex = new int[count];
            Parallel.For(0, count, i => maxIndex[i] = collection[i].Count);

            do
            {
                Parallel.For(0, count, i =>
                {
                    string value = collection[i][index[i]];
                    if (assignment.ContainsKey(varNameList[i]))
                    {
                        assignment.Add(varNameList[i], value);
                    }
                    else
                    {
                        assignment[varNameList[i]] = value;
                    }
                });

                CUDDNode gdNode = GetCuddNode(context, gndPredDict, assignment, isPrevious);
                CUDDNode andNode = CUDD.Function.And(result, gdNode);
                CUDD.Ref(andNode);
                CUDD.Deref(result);
                CUDD.Deref(gdNode);
                result = andNode;

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
                    return result;
                index[j]++;
            } while (true);
        }

        //private void AddToGroundPredicateDict(string predName, string[] constantList)
        //{
        //    Predicate pred = _predDict[predName];
        //    GroundPredicate gndPred = new GroundPredicate(pred, constantList);
        //    gndPred.PreviousCuddIndex = _currentCuddIndex;
        //    _currentCuddIndex++;
        //    gndPred.SuccessiveCuddIndex = _currentCuddIndex;
        //    _currentCuddIndex++;
        //    _gndPredDict.Add(gndPred.ToString(), gndPred);
        //}

        //protected AbstractPredicate GetAbstractPredicate(PlanningParser.TermAtomFormContext context)
        //{
        //    string abstractPredName = VariableContainer.GetFullName(context);
        //    AbstractPredicate result = _abstractPredDict[abstractPredName];
        //    return result;
        //}

        #endregion

        #endregion
    }
}
