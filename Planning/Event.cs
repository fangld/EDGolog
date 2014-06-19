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

        private List<string> _constList;

        private List<Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>>> _effect;

        #endregion

        #region Properties
        
        public CUDDNode Precondition { get; set; }

        public IReadOnlyList<Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>>> Effect
        {
            get { return _effect; }
        }

        public IReadOnlyList<string> ConstList { get { return _constList; } }

        #endregion

        #region Constructors

        private Event()
        {
            _constList = new List<string>();
            _effect = new List<Tuple<CUDDNode, List<Tuple<AbstractPredicate, bool>>>>();
        }

        #endregion

        #region Methods

        public static Event From(PlanningParser.EventDefineContext context, IReadOnlyDictionary<string, GroundPredicate> gndPredDict, string[] constArray)
        {
            Event result = new Event();
            result.GenerateVariableList(context.listVariable());
            result.SetConstList(constArray);
            string eventName = context.eventSymbol().NAME().GetText();
            result.Name = GetFullName(eventName, result.ConstList);
            result.GeneratePrecondition(context, gndPredDict);
            return result;
        }

        #region Methods for generating precondition

        private void SetConstList(string[] constArray)
        {
            for (int i = 0; i < constArray.Length; i++)
            {
                _constList.Add(constArray[i]);
            }
        }

        protected void GeneratePrecondition(PlanningParser.EventDefineContext context, IReadOnlyDictionary<string, GroundPredicate> gndPredDict)
        {
            Precondition = CUDD.ONE;

            if (context.PRE() != null)
            {
                if (context.emptyOrPreGD().gd() != null)
                {
                    Dictionary<string, string> assignment = new Dictionary<string, string>();
                    for (int i = 0; i < _constList.Count; i++)
                    {
                        assignment.Add(VariableList[i].Item1, _constList[i]);
                    }
                    Precondition = GetCuddNode(context.emptyOrPreGD().gd(), gndPredDict, assignment);
                }
            }
        }

        private CUDDNode GetCuddNode(PlanningParser.TermAtomFormContext context, IReadOnlyDictionary<string, GroundPredicate> gndPredDict, Dictionary<string, string> assignment, bool isPrevious = true)
        {
            CUDDNode result = CUDD.ONE;
            if (context.pred() != null)
            {
                string predFullName = GetFullName(context, assignment);
                GroundPredicate gndPred = gndPredDict[predFullName];
                int cuddIndex = isPrevious ? gndPred.PreviousCuddIndex : gndPred.SuccessiveCuddIndex;
                result = CUDD.Var(cuddIndex);
            }
            else 
            {
                string firstTermString = Globals.TermHandler.GetString(context.term(0), assignment);
                string secondTermString = Globals.TermHandler.GetString(context.term(1), assignment);
                //Console.WriteLine("FirstTermString:{0}, SecondTermString:{1}", firstTermString, secondTermString);
                //int firstValue = int.Parse(firstTermString);
                //int secondValue = int.Parse(secondTermString);
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
            else
            {
                List<string> varNameList = new List<string>();
                
                List<List<string>> collection = new List<List<string>>();

                var listVariableContext = context.listVariable();
                do
                {
                    if (listVariableContext.VAR().Count != 0)
                    {
                        string type = listVariableContext.type() == null ? PlanningType.ObjectType.Name : listVariableContext.type().GetText();

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
                    result = ScanMixedRadix(context.gd(0), gndPredDict, assignment, varNameList, collection, 0,
                        isPrevious);
                }
                else
                {
                    result = ScanMixedRadix(context.gd(0), gndPredDict, assignment, varNameList, collection, 0,
                        isPrevious);
                }
            }

            return result;
        }

        private CUDDNode ScanMixedRadix(PlanningParser.GdContext context, IReadOnlyDictionary<string, GroundPredicate> gndPredDict, Dictionary<string, string> assignment, IReadOnlyList<string> varNameList, IReadOnlyList<List<string>> collection, int currentLevel, bool isPrevious)
        {
            CUDDNode result = CUDD.ONE;
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
                    
                    CUDDNode gdNode = ScanMixedRadix(context, gndPredDict, assignment, varNameList, collection, currentLevel + 1, isPrevious);
                    if (gdNode.Equals(CUDD.ZERO))
                    {
                        break;
                    }
                    else
                    {
                        CUDDNode temp = result;
                        result = CUDDFunction.
                    }
                }
            }
            else
            {
                result = GetCuddNode(context.gd(0), gndPredDict, assignment, isPrevious);
            }
            return result;
        }

        private CUDDNode ScanMixedRadix2(PlanningParser.GdContext context, IReadOnlyDictionary<string, GroundPredicate> gndPredDict, Dictionary<string, string> assignment, IReadOnlyList<string> varNameList, IReadOnlyList<List<string>> collection, int currentLevel, bool isPrevious)
        {
            CUDDNode result = null;
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

                    result = ScanMixedRadix(context, gndPredDict, assignment, varNameList, collection, currentLevel + 1, isPrevious);
                    if (result.Equals(CUDD.ONE))
                    {
                        break;
                    }
                }
            }
            else
            {
                result = GetCuddNode(context.gd(0), gndPredDict, assignment, isPrevious);
            }
            return result;
        }

        

        #endregion

        #endregion
    }
}
