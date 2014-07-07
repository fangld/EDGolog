using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning
{
    public class Event : ConstContainer
    {
        #region Fields

        private List<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>> _condEffect;

        private HashSet<Predicate> _effectPredSet;

        #endregion

        #region Properties

        public CUDDNode Precondition { get; set; }

        public int CuddIndex { get; set; }

        public IReadOnlyList<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>> CondEffect
        {
            get { return _condEffect; }
        }

        public HashSet<Predicate> EffectPredSet
        {
            get { return _effectPredSet; }
        }

        public CUDDNode SuccessorStateAxiom { get; set; }

        public CUDDNode ParitalSuccessorStateAxiom { get; set; }

        #endregion

        #region Constructors

        public Event(PlanningParser.EventDefineContext context, IReadOnlyDictionary<string, Predicate> predDict,
            string[] constArray, Dictionary<string, string> assignment, ref int initialCuddInex): base(constArray)
        {
            CuddIndex = initialCuddInex;
            initialCuddInex++;
            Name = context.eventSymbol().GetText();
            Console.WriteLine(FullName);
            GeneratePrecondition(context.emptyOrPreGD(), predDict, assignment);
            Console.WriteLine("Finishing event define precondition");
            Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(Precondition));
            if (context.emptyOrPreGD() != null)
            {
                Console.WriteLine("  Context: {0}", context.emptyOrPreGD().GetText());
            }
            //string filename = string.Format("{0}.dot", FullName);
            //CUDD.Print.PrintBDDTree(Precondition, filename);
            //CUDD.Print.PrintMinterm(Precondition);
            //Console.ReadLine();

            GenerateEffect(context.emptyOrEffect(), predDict, assignment);
            Console.WriteLine("Finishing event effect");
            GeneratePartialSuccessorStateAxiom(predDict);
            Console.WriteLine("Finishing event parital successor state axiom");
            Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(ParitalSuccessorStateAxiom));

            //GenerateSuccessorStateAxiom(predDict);
            //Console.WriteLine("Finishing event define SSA");
            //Console.WriteLine("  Number of nodes: {0}", CUDD.GetNumNodes(SuccessorStateAxiom));

        }

        #endregion

        #region Methods

        #region Methods for generating precondition

        private void GeneratePrecondition(PlanningParser.EmptyOrPreGDContext context, IReadOnlyDictionary<string, Predicate> predDict, Dictionary<string, string> assignment)
        {
            Precondition = CUDD.ONE;
            CUDD.Ref(Precondition);

            if (context != null)
            {
                PlanningParser.GdContext gdContext = context.gd();
                if (context.gd() != null)
                {
                    CUDD.Deref(Precondition);
                    Precondition = GetCuddNode(gdContext, predDict, assignment);
                }
            }
        }

        private CUDDNode GetCuddNode(PlanningParser.TermAtomFormContext context, IReadOnlyDictionary<string, Predicate> predDict, Dictionary<string, string> assignment)
        {
            CUDDNode result;
            if (context.pred() != null)
            {
                string predFullName = GetFullName(context, assignment);
                Predicate pred = predDict[predFullName];
                int cuddIndex = pred.PreviousCuddIndex;
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

            CUDD.Ref(result);
            return result;
        }

        private CUDDNode GetCuddNode(PlanningParser.GdContext context, IReadOnlyDictionary<string, Predicate> predDict, Dictionary<string, string> assignment)
        {
            CUDDNode result;
            if (context.termAtomForm() != null)
            {
                result = GetCuddNode(context.termAtomForm(), predDict, assignment);
            }
            else if (context.AND() != null)
            {
                result = CUDD.ONE;
                CUDD.Ref(result);
                //result = GetCuddNode(context.gd()[0], predDict, assignment);
                for (int i = 0; i < context.gd().Count; i++)
                {
                    CUDDNode gdNode = GetCuddNode(context.gd()[i], predDict, assignment);
                    if (gdNode.Equals(CUDD.ZERO))
                    {
                        CUDD.Deref(result);
                        CUDD.Deref(gdNode);
                        result = CUDD.ZERO;
                        CUDD.Ref(result);
                        break;
                    }
                    CUDDNode andNode = CUDD.Function.And(result, gdNode);
                    CUDD.Ref(andNode);
                    CUDD.Deref(result);
                    CUDD.Deref(gdNode);
                    result = andNode;
                }
            }
            else if (context.OR() != null)
            {
                result = CUDD.ZERO;
                CUDD.Ref(result);
                //result = GetCuddNode(context.gd()[0], predDict, assignment);
                for (int i = 0; i < context.gd().Count; i++)
                {
                    CUDDNode gdNode = GetCuddNode(context.gd()[i], predDict, assignment);
                    if (gdNode.Equals(CUDD.ONE))
                    {
                        CUDD.Deref(result);
                        CUDD.Deref(gdNode);
                        result = CUDD.ONE;
                        CUDD.Ref(result);
                        break;
                    }
                    CUDDNode orNode = CUDD.Function.Or(result, gdNode);
                    CUDD.Ref(orNode);
                    CUDD.Deref(result);
                    CUDD.Deref(gdNode);
                    result = orNode;
                }
            }
            else if (context.NOT() != null)
            {
                CUDDNode gdNode = GetCuddNode(context.gd()[0], predDict, assignment);
                result = CUDD.Function.Not(gdNode);
                CUDD.Ref(result);
                CUDD.Deref(gdNode);
            }
            else if (context.IMPLY() != null)
            {
                CUDDNode gdNode0 = GetCuddNode(context.gd()[0], predDict, assignment);
                CUDDNode gdNode1 = GetCuddNode(context.gd()[1], predDict, assignment);
                result = CUDD.Function.Implies(gdNode0, gdNode1);
                CUDD.Ref(result);
                CUDD.Deref(gdNode0);
                CUDD.Deref(gdNode1);
            }
            else
            {
                var listVariableContext = context.listVariable();
                var collection = listVariableContext.GetCollection();
                var varNameList = listVariableContext.GetVarNameList();

                bool isForall = context.FORALL() != null;
                result = ScanVarList(context.gd(0), predDict, assignment, varNameList, collection, 0, isForall);

                //if (context.FORALL() != null)
                //{
                //    result = ScanVarList(context.gd(0), predDict, assignment, varNameList, collection);
                //}
                //else
                //{
                //    result = ScanVarList(context.gd(0), predDict, assignment, varNameList, collection, 0, false);
                //}
            }

            return result;
        }

        private CUDDNode ScanVarList(PlanningParser.GdContext context, IReadOnlyDictionary<string, Predicate> predDict,
            Dictionary<string, string> assignment, IReadOnlyList<string> varNameList,
            IReadOnlyList<List<string>> collection, int currentLevel = 0, bool isForall = true)
        {
            CUDDNode result = isForall ? CUDD.ONE: CUDD.ZERO;
            CUDD.Ref(result);
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

                    CUDDNode gdNode = ScanVarList(context, predDict, assignment, varNameList, collection,
                        currentLevel + 1, isForall);

                    CUDDNode terminalNode = isForall ? CUDD.ZERO : CUDD.ONE;

                    if (gdNode.Equals(terminalNode))
                    {
                        CUDD.Deref(result);
                        result = terminalNode;
                        CUDD.Ref(result);
                        break;
                    }

                    CUDDNode quantifiedNode = isForall ? CUDD.Function.And(result, gdNode) : CUDD.Function.Or(result, gdNode);
                    CUDD.Ref(quantifiedNode);
                    CUDD.Deref(result);
                    CUDD.Deref(gdNode);
                    result = quantifiedNode;
                }
            }
            else
            {
                result = GetCuddNode(context, predDict, assignment);
            }
            return result;
        }

        #endregion

        #region Methods for generating effect

        private void GenerateEffect(PlanningParser.EmptyOrEffectContext context,
            IReadOnlyDictionary<string, Predicate> predDict, Dictionary<string, string> assignment)
        {
            _condEffect = new List<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>>();
            if (context != null)
            {
                PlanningParser.EffectContext effectContext = context.effect();
                if (effectContext != null)
                {
                    foreach (var cEffectContext in effectContext.cEffect())
                    {
                        CUDDNode initialCuddNode = CUDD.ONE;
                        CUDD.Ref(initialCuddNode);
                        var condEffect = GetCondEffectList(initialCuddNode, cEffectContext, predDict, assignment);
                        _condEffect.AddRange(condEffect);
                    }
                }
            }
        }

        private List<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>> GetCondEffectList(CUDDNode currentCondNode,
            PlanningParser.EffectContext context, IReadOnlyDictionary<string, Predicate> predDict,
            Dictionary<string, string> assignment)
        {
            var result = new List<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>>();

            foreach (var cEffectContext in context.cEffect())
            {
                var condEffect = GetCondEffectList(currentCondNode, cEffectContext, predDict, assignment);
                result.AddRange(condEffect);
            }

            return result;
        }

        private List<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>> GetCondEffectList(CUDDNode currentCondNode,
            PlanningParser.CEffectContext context, IReadOnlyDictionary<string, Predicate> predDict,
            Dictionary<string, string> assignment)
        {
            List<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>> result;
            if (context.FORALL() != null)
            {
                var listVariableContext = context.listVariable();
                var varNameList = listVariableContext.GetVarNameList();
                var collection = listVariableContext.GetCollection();
                result = ScanMixedRadix(varNameList, collection, assignment, currentCondNode, context.effect(), predDict);
            }
            else if (context.WHEN() != null)
            {
                result = new List<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>>();
                CUDDNode gdNode = GetCuddNode(context.gd(), predDict, assignment);
                CUDDNode condNode = CUDD.Function.And(currentCondNode, gdNode);
                CUDD.Ref(condNode);
                CUDD.Deref(gdNode);
                if (!condNode.Equals(CUDD.ZERO))
                {
                    var gndLiterals = GetLiteralList(context.condEffect(), predDict, assignment);
                    var condEffect = new Tuple<CUDDNode, List<Tuple<Predicate, bool>>>(condNode, gndLiterals);
                    result.Add(condEffect);
                }
            }
            else
            {
                result = new List<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>>();
                var gndLiterals = GetLiteralList(context.condEffect(), predDict, assignment);
                var condEffect = new Tuple<CUDDNode, List<Tuple<Predicate, bool>>>(currentCondNode, gndLiterals);
                result.Add(condEffect);
            }
            return result;
        }

        private List<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>> ScanMixedRadix(IReadOnlyList<string> varNameList,
            IReadOnlyList<List<string>> collection, Dictionary<string, string> assignment, CUDDNode currentCondNode,
            PlanningParser.EffectContext context, IReadOnlyDictionary<string, Predicate> predDict)
        {
            var result = new List<Tuple<CUDDNode, List<Tuple<Predicate, bool>>>>();
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

                var condEffectList = GetCondEffectList(currentCondNode, context, predDict, assignment);
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

        private List<Tuple<Predicate, bool>> GetLiteralList(PlanningParser.CondEffectContext context,
            IReadOnlyDictionary<string, Predicate> predDict, Dictionary<string, string> assignment)
        {
            List<Tuple<Predicate, bool>> result =
                new List<Tuple<Predicate, bool>>(context.termLiteral().Count);
            foreach (var termLiteralContext in context.termLiteral())
            {
                var gndLiteral = GetLiteral(termLiteralContext, predDict, assignment);
                result.Add(gndLiteral);
            }
            return result;
        }

        private Tuple<Predicate, bool> GetLiteral(PlanningParser.TermLiteralContext context,
            IReadOnlyDictionary<string, Predicate> predDict, Dictionary<string, string> assignment)
        {
            string predFullName = GetFullName(context.termAtomForm(), assignment);
            Predicate pred = predDict[predFullName];
            bool isPositive = context.NOT() == null;
            return new Tuple<Predicate, bool>(pred, isPositive);
        }

        #endregion

        #region Methods for generating partial successor state axiom

        private void GeneratePartialSuccessorStateAxiom(IReadOnlyDictionary<string, Predicate> predDict)
        {
            CUDDNode effectNode = GetEffectNode();
            CUDDNode frameNode = GetPartialFrameNode(predDict);

            ParitalSuccessorStateAxiom = CUDD.Function.And(effectNode, frameNode);

            CUDD.Ref(ParitalSuccessorStateAxiom);
            CUDD.Deref(effectNode);
            CUDD.Deref(frameNode);
        }

        private CUDDNode GetEffectNode()
        {
            CUDDNode result = CUDD.ONE;
            CUDD.Ref(result);

            foreach (var cEffect in _condEffect)
            {
                var firstLiteral = cEffect.Item2[0];

                CUDDNode gndPred = CUDD.Var(firstLiteral.Item1.SuccessiveCuddIndex);
                CUDDNode literalsNode = firstLiteral.Item2 ? gndPred : CUDD.Function.Not(gndPred);
                CUDD.Ref(literalsNode);
                
                for (int i = 1; i < cEffect.Item2.Count; i++)
                {
                    var literal = cEffect.Item2[i];
                    CUDDNode conjLiterals = literalsNode;
                    gndPred = CUDD.Var(literal.Item1.SuccessiveCuddIndex);
                    CUDDNode literalNode = literal.Item2 ? gndPred : CUDD.Function.Not(gndPred);
                    literalsNode = CUDD.Function.And(conjLiterals, literalNode);
                    CUDD.Ref(literalsNode);
                    CUDD.Deref(conjLiterals);
                }

                CUDDNode cEffectNode = CUDD.Function.Implies(cEffect.Item1, literalsNode);
                CUDD.Ref(cEffectNode);
                CUDD.Deref(literalsNode);

                CUDDNode conjCEffectNode = result;
                result = CUDD.Function.And(conjCEffectNode, cEffectNode);
                CUDD.Ref(result);
                CUDD.Deref(conjCEffectNode);
            }

            return result;
        }

        private CUDDNode GetPartialFrameNode(IReadOnlyDictionary<string, Predicate> predDict)
        {
            _effectPredSet = new HashSet<Predicate>();
            CUDDNode result = CUDD.ONE;
            CUDD.Ref(result);
            foreach (var pred in predDict.Values)
            {
                CUDDNode frameCondition = CUDD.ONE;
                CUDD.Ref(frameCondition);
                foreach (var cEffect in _condEffect)
                {
                    if (cEffect.Item2.Exists(literal => literal.Item1.Equals(pred)))
                    {
                        CUDD.Ref(cEffect.Item1);
                        CUDDNode negCondition = CUDD.Function.Not(cEffect.Item1);
                        CUDD.Ref(negCondition);
                        //CUDD.Deref(cEffect.Item1); //???
                        CUDDNode tmpNode = CUDD.Function.And(frameCondition, negCondition);
                        CUDD.Ref(tmpNode);
                        CUDD.Deref(frameCondition);
                        CUDD.Deref(negCondition);
                        frameCondition = tmpNode;
                    }
                }

                if (frameCondition.Equals(CUDD.ONE))
                {
                    CUDD.Deref(frameCondition);
                }
                else
                {
                    _effectPredSet.Add(pred);
                    CUDDNode prePredNode = CUDD.Var(pred.PreviousCuddIndex);
                    CUDDNode sucPredNode = CUDD.Var(pred.SuccessiveCuddIndex);
                    CUDDNode invariantNode = CUDD.Function.Equal(prePredNode, sucPredNode);
                    CUDD.Ref(invariantNode);

                    CUDDNode frame = CUDD.Function.Implies(frameCondition, invariantNode);
                    CUDD.Ref(frame);
                    CUDD.Deref(frameCondition);
                    CUDD.Deref(invariantNode);
                    
                    CUDDNode tmpNode =  CUDD.Function.And(result, frame);
                    CUDD.Ref(tmpNode);
                    CUDD.Deref(result);
                    CUDD.Deref(frame);
                    result = tmpNode;
                }
            }

            return result;
        }

        #endregion

        #endregion
    }
}
