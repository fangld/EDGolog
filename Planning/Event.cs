using System;
using System.Collections.Generic;
using System.Globalization;
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

        private List<Tuple<CUDDNode, List<Tuple<GroundPredicate, bool>>>> _condEffect;

        private Dictionary<string, string> _assignment;

        #endregion

        #region Properties

        public CUDDNode Precondition { get; set; }

        public IReadOnlyList<Tuple<CUDDNode, List<Tuple<GroundPredicate, bool>>>> CondEffect
        {
            get { return _condEffect; }
        }

        public IReadOnlyList<string> ConstList
        {
            get { return _constList; }
        }

        public CUDDNode SuccessorStateAxiom { get; set; }

        #endregion

        #region Constructors

        private Event()
        {
            _constList = new List<string>();
            _condEffect = new List<Tuple<CUDDNode, List<Tuple<GroundPredicate, bool>>>>();
        }

        #endregion

        #region Methods

        public static Event From(PlanningParser.EventDefineContext context,
            IReadOnlyDictionary<string, GroundPredicate> gndPredDict, string[] constArray)
        {
            Event result = new Event();
            result.GenerateVariableList(context.listVariable());
            result.SetConstList(constArray);
            result.BuildAssignment();
            string eventName = context.eventSymbol().GetText();
            result.Name = GetFullName(eventName, result._constList);
            result.GeneratePrecondition(context.emptyOrPreGD(), gndPredDict);
            result.GenerateEffect(context.emptyOrEffect(), gndPredDict);
            result.GenerateSuccessorStateAxiom(gndPredDict);
            return result;
        }

        private void SetConstList(string[] constArray)
        {
            _constList.AddRange(constArray);
        }

        private void BuildAssignment()
        {
            _assignment = new Dictionary<string, string>();
            for (int i = 0; i < _constList.Count; i++)
            {
                _assignment.Add(VariableList[i].Item1, _constList[i]);
            }
        }

        #region Methods for generating precondition

        private void GeneratePrecondition(PlanningParser.EmptyOrPreGDContext context,
            IReadOnlyDictionary<string, GroundPredicate> gndPredDict)
        {
            Precondition = CUDD.ONE;

            if (context != null)
            {
                PlanningParser.GdContext gdContext = context.gd();
                if (context.gd() != null)
                {
                    Precondition = GetCuddNode(gdContext, gndPredDict, _assignment);
                }
            }
        }

        private CUDDNode GetCuddNode(PlanningParser.TermAtomFormContext context,
            IReadOnlyDictionary<string, GroundPredicate> gndPredDict, Dictionary<string, string> assignment,
            bool isPrevious = true)
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

        private CUDDNode GetCuddNode(PlanningParser.GdContext context,
            IReadOnlyDictionary<string, GroundPredicate> gndPredDict, Dictionary<string, string> assignment,
            bool isPrevious = true)
        {
            CUDDNode result;

            //Console.WriteLine("  context is null:{0}", context == null);
            //Console.WriteLine("  Context:{0}", context.GetText());
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
                    result = ScanVarList(context.gd(0), gndPredDict, assignment, varNameList, collection, 0, isPrevious);
                }
                else
                {
                    result = ScanVarList(context.gd(0), gndPredDict, assignment, varNameList, collection, 0, isPrevious,
                        false);
                }
            }

            return result;
        }

        private CUDDNode ScanVarList(PlanningParser.GdContext context,
            IReadOnlyDictionary<string, GroundPredicate> gndPredDict, Dictionary<string, string> assignment,
            IReadOnlyList<string> varNameList, IReadOnlyList<List<string>> collection, int currentLevel, bool isPrevious,
            bool isForall = true)
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

                    CUDDNode gdNode = ScanVarList(context, gndPredDict, assignment, varNameList, collection,
                        currentLevel + 1, isPrevious, isForall);

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
                result = GetCuddNode(context, gndPredDict, assignment, isPrevious);
            }
            return result;
        }

        #endregion

        #region Methods for generating effect

        private void GenerateEffect(PlanningParser.EmptyOrEffectContext context,
            IReadOnlyDictionary<string, GroundPredicate> gndPredDict)
        {
            _condEffect = new List<Tuple<CUDDNode, List<Tuple<GroundPredicate, bool>>>>();
            if (context != null)
            {
                PlanningParser.EffectContext effectContext = context.effect();
                if (effectContext != null)
                {
                    foreach (var cEffectContext in effectContext.cEffect())
                    {
                        var condEffect = GetCondEffectList(CUDD.ONE, cEffectContext, gndPredDict, _assignment);
                        _condEffect.AddRange(condEffect);
                    }
                }
            }
        }

        private List<Tuple<CUDDNode, List<Tuple<GroundPredicate, bool>>>> GetCondEffectList(CUDDNode currentCondNode,
            PlanningParser.EffectContext context, IReadOnlyDictionary<string, GroundPredicate> gndPredDict,
            Dictionary<string, string> assignment)
        {
            var result = new List<Tuple<CUDDNode, List<Tuple<GroundPredicate, bool>>>>();

            foreach (var cEffectContext in context.cEffect())
            {
                var condEffect = GetCondEffectList(currentCondNode, cEffectContext, gndPredDict, assignment);
                result.AddRange(condEffect);
            }

            return result;
        }

        private List<Tuple<CUDDNode, List<Tuple<GroundPredicate, bool>>>> GetCondEffectList(CUDDNode currentCondNode,
            PlanningParser.CEffectContext context, IReadOnlyDictionary<string, GroundPredicate> gndPredDict,
            Dictionary<string, string> assignment)
        {
            List<Tuple<CUDDNode, List<Tuple<GroundPredicate, bool>>>> result;
            if (context.FORALL() != null)
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
                result = ScanMixedRadix(currentCondNode, context.effect(), gndPredDict, assignment, varNameList,
                    collection);
            }
            else if (context.WHEN() != null)
            {
                result = new List<Tuple<CUDDNode, List<Tuple<GroundPredicate, bool>>>>();
                CUDDNode gdNode = GetCuddNode(context.gd(), gndPredDict, assignment);
                CUDDNode condNode = CUDD.Function.And(currentCondNode, gdNode);
                if (!condNode.Equals(CUDD.ZERO))
                {
                    var gndLiterals = GetGroundLiteral(context.condEffect(), gndPredDict);
                    var condEffect = new Tuple<CUDDNode, List<Tuple<GroundPredicate, bool>>>(condNode, gndLiterals);
                    result.Add(condEffect);
                }
            }
            else
            {
                result = new List<Tuple<CUDDNode, List<Tuple<GroundPredicate, bool>>>>();
                var gndLiterals = GetGroundLiteral(context.condEffect(), gndPredDict);
                var condEffect = new Tuple<CUDDNode, List<Tuple<GroundPredicate, bool>>>(currentCondNode, gndLiterals);
                result.Add(condEffect);
            }
            return result;
        }

        private List<Tuple<CUDDNode, List<Tuple<GroundPredicate, bool>>>> ScanMixedRadix(CUDDNode currentCondNode,
            PlanningParser.EffectContext context, IReadOnlyDictionary<string, GroundPredicate> gndPredDict,
            Dictionary<string, string> assignment, IReadOnlyList<string> varNameList,
            IReadOnlyList<List<string>> collection)
        {
            var result = new List<Tuple<CUDDNode, List<Tuple<GroundPredicate, bool>>>>();
            int count = collection.Count;
            int[] index = new int[count];
            int[] maxIndex = new int[count];
            Parallel.For(0, count, i => maxIndex[i] = collection[i].Count);

            do
            {
                for (int i = 0; i < count; i++)
                {
                    string value = collection[i][index[i]];
                    string varName = varNameList[i];
                    if (!assignment.ContainsKey(varName))
                    {
                        assignment.Add(varName, value);
                    }
                    else
                    {
                        assignment[varName] = value;
                    }
                }

                var condEffectList = GetCondEffectList(currentCondNode, context, gndPredDict, assignment);
                result.AddRange(condEffectList);

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

        private List<Tuple<GroundPredicate, bool>> GetGroundLiteral(PlanningParser.CondEffectContext context,
            IReadOnlyDictionary<string, GroundPredicate> gndPredDict)
        {
            List<Tuple<GroundPredicate, bool>> result =
                new List<Tuple<GroundPredicate, bool>>(context.termLiteral().Count);
            foreach (var termLiteralContext in context.termLiteral())
            {
                var gndLiteral = GetGroundLiteral(termLiteralContext, gndPredDict);
                result.Add(gndLiteral);
            }
            return result;
        }

        private Tuple<GroundPredicate, bool> GetGroundLiteral(PlanningParser.TermLiteralContext context,
            IReadOnlyDictionary<string, GroundPredicate> gndPredDict)
        {
            string predFullName = GetFullName(context.termAtomForm(), _assignment);
            GroundPredicate gndPred = gndPredDict[predFullName];
            bool isPositive = context.NOT() == null;
            return new Tuple<GroundPredicate, bool>(gndPred, isPositive);
        }

        #endregion

        #region Methods for generating successor state axiom

        private void GenerateSuccessorStateAxiom(IReadOnlyDictionary<string, GroundPredicate> gndPredDict)
        {
            CUDDNode effectNode = GetEffectNode();
            CUDDNode frame = GetFrameNode(gndPredDict);

            SuccessorStateAxiom = CUDD.Function.And(effectNode, frame);

            CUDD.Ref(SuccessorStateAxiom);
            CUDD.Deref(effectNode);
            CUDD.Deref(frame);
        }

        private CUDDNode GetEffectNode()
        {
            CUDDNode result = CUDD.ONE;

            foreach (var cEffect in _condEffect)
            {
                var firstLiteral = cEffect.Item2[0];

                CUDDNode gndPred = CUDD.Var(firstLiteral.Item1.SuccessiveCuddIndex);
                CUDDNode literalsNode = firstLiteral.Item2 ? gndPred : CUDD.Function.Not(gndPred);
                
                for (int i = 1; i < cEffect.Item2.Count; i++)
                {
                    var literal = cEffect.Item2[i];
                    CUDDNode conjLiterals = literalsNode;
                    gndPred = CUDD.Var(literal.Item1.SuccessiveCuddIndex);
                    CUDDNode literalNode = literal.Item2 ? gndPred : CUDD.Function.Not(gndPred);
                    literalsNode = CUDD.Function.And(conjLiterals, literalNode);
                    CUDD.Ref(literalsNode);
                    CUDD.Deref(conjLiterals);
                    CUDD.Deref(literalNode);
                }

                CUDDNode cEffectNode = CUDD.Function.Implies(cEffect.Item1, literalsNode);
                CUDD.Ref(cEffectNode);
                CUDD.Deref(literalsNode);

                //if (Name == "leftSucWithNotice(a1,0)")
                //{
                //    Console.WriteLine("  Each CondEffect:");
                //    Console.WriteLine("    Precondition");
                //    CUDD.Print.PrintMinterm(cEffect.Item1);
                //    Console.WriteLine("    Effect:");
                //    //Console.WriteLine("  Count:{0}", e.CondEffect.Count);

                //    foreach (var tuple in cEffect.Item2)
                //    {
                //        string format = tuple.Item2 ? "{0} " : "!{0} ";
                //        Console.Write(format, tuple.Item1);
                //    }

                //    Console.WriteLine();
                //    CUDD.Print.PrintMinterm(cEffectNode);
                //}

                CUDDNode conjCEffectNode = result;
                result = CUDD.Function.And(conjCEffectNode, cEffectNode);
                CUDD.Ref(result);
                CUDD.Deref(conjCEffectNode);
            }

            return result;
        }

        private CUDDNode GetFrameNode(IReadOnlyDictionary<string, GroundPredicate> gndPredDict)
        {
            CUDDNode result = CUDD.ONE;
            //Console.WriteLine("    Previous abstract predicate count:{0}", _preAbstractPredDict.Count);
            foreach (var gndPredPair in gndPredDict)
            {
                CUDDNode frameCondition = CUDD.ONE;
                foreach (var cEffect in _condEffect)
                {
                    //Console.Write("    Literals:");
                    //for (int i = 0; i < cEffect.Item2.Count; i++)
                    //{
                    //    Console.Write("{0}, ", cEffect.Item2[i].Item1);
                    //}
                    //Console.WriteLine();
                    //Console.WriteLine("    Abstract predicate:{0}", abstractPredicate.Key);
                    //Console.WriteLine(cEffect.Item2.Exists(literal => literal.Item1.Equals(abstractPredicate.Value)));

                    if (cEffect.Item2.Exists(literal => literal.Item1.Equals(gndPredPair.Value)))
                    {
                        CUDDNode intermediate = frameCondition;
                        CUDDNode negCondition = CUDD.Function.Not(cEffect.Item1);
                        CUDD.Ref(negCondition);
                        frameCondition = CUDD.Function.And(intermediate, negCondition);
                        CUDD.Ref(frameCondition);
                        CUDD.Deref(intermediate);
                        CUDD.Deref(negCondition);
                    }
                }

                CUDDNode prePredNode = CUDD.Var(gndPredPair.Value.PreviousCuddIndex);
                CUDDNode sucPredNode = CUDD.Var(gndPredPair.Value.SuccessiveCuddIndex);

                CUDDNode invariant = CUDD.Function.Equal(prePredNode, sucPredNode);

                //Console.WriteLine("       Frame condition:");
                //CUDD.Print.PrintMinterm(frameCondition);
                //Console.WriteLine("       Invariant:");
                //CUDD.Print.PrintMinterm(invariant);
                CUDD.Ref(invariant);
                CUDDNode frame = CUDD.Function.Implies(frameCondition, invariant);
                CUDD.Ref(frame);
                CUDD.Deref(frameCondition);
                CUDD.Deref(invariant);

                CUDDNode conjunct = result;
                result = CUDD.Function.And(conjunct, frame);
                CUDD.Ref(result);
                CUDD.Deref(conjunct);
            }

            return result;
        }

        #endregion

        #endregion
    }
}
