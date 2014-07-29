using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;
using Planning.ContextExtensions;

namespace Planning.Clients
{
    public class MentalAttitude
    {
        #region Fields

        private IReadOnlyDictionary<string, Predicate> _predicateDict;

        #endregion

        #region Properties

        public CUDDNode Knowledge { get; private set; }

        public CUDDNode Belief { get; private set; }

        #endregion

        #region Constructors

        public MentalAttitude(ClientProblem problem)
        {
            Knowledge = problem.InitKnowledge;
            Belief = problem.InitBelief;
            _predicateDict = problem.PredicateDict;
        }

        #endregion

        #region Methods

        public void Update(EventModel eventModel)
        {
            CUDD.Ref(Belief);
            CUDDNode impliesNode = CUDD.Function.Implies(Belief, eventModel.BelievePrecondition);

            if (impliesNode.Equals(CUDD.ONE))
            {
                UpdateBelief(eventModel.BelievePrecondition, eventModel.BelievePartialSsa,
                    eventModel.BelieveAffectedPredSet);
                return;
            }

            CUDD.Ref(Belief);
            impliesNode = CUDD.Function.Implies(Belief, eventModel.KnowPrecondition);
            if (impliesNode.Equals(CUDD.ONE))
            {
                UpdateBelief(eventModel.KnowPrecondition, eventModel.KnowPartialSsa, eventModel.KnowAffectedPredSet);
                CUDD.Deref(Belief);
                return;
            }

            CUDD.Ref(Knowledge);
            impliesNode = CUDD.Function.Implies(Knowledge, eventModel.BelievePrecondition);
            if (impliesNode.Equals(CUDD.ONE))
            {
                Belief = Knowledge;
                CUDD.Ref(Belief);
                UpdateBelief(eventModel.BelievePrecondition, eventModel.BelievePartialSsa,
                    eventModel.BelieveAffectedPredSet);
                return;
            }

            UpdateKnowledge(eventModel);
            Belief = Knowledge;
        }

        private void UpdateKnowledge(EventModel eventModel)
        {
            CUDD.Ref(eventModel.KnowPrecondition);
            CUDDNode knowledgeWithPre = CUDD.Function.And(Knowledge, eventModel.KnowPrecondition);
            CUDD.Ref(eventModel.KnowPartialSsa);
            CUDDNode knowledgeWithPreAndPssa = CUDD.Function.And(knowledgeWithPre, eventModel.KnowPartialSsa);

            CUDDVars oldVars = new CUDDVars();
            CUDDVars newVars = new CUDDVars();

            foreach (var predicate in eventModel.KnowAffectedPredSet)
            {
                CUDDNode trueRestrictBy = CUDD.Var(predicate.PreviousCuddIndex);
                CUDD.Ref(trueRestrictBy);
                CUDD.Ref(knowledgeWithPreAndPssa);
                CUDDNode trueNode = CUDD.Function.Restrict(knowledgeWithPreAndPssa, trueRestrictBy);
                CUDDNode falseRestrictBy = CUDD.Function.Not(trueRestrictBy);
                CUDDNode falseNode = CUDD.Function.Restrict(knowledgeWithPreAndPssa, falseRestrictBy);

                knowledgeWithPreAndPssa = CUDD.Function.Or(trueNode, falseNode);

                oldVars.AddVar(CUDD.Var(predicate.SuccessiveCuddIndex));
                newVars.AddVar(CUDD.Var(predicate.PreviousCuddIndex));
            }

            Knowledge = CUDD.Variable.SwapVariables(knowledgeWithPreAndPssa, oldVars, newVars);
        }

        private void UpdateBelief(CUDDNode precondition, CUDDNode partialSsa, HashSet<Predicate> affectedPredSet)
        {
            CUDD.Ref(precondition);
            CUDDNode beliefWithPre = CUDD.Function.And(Belief, precondition);
            CUDD.Ref(partialSsa);
            CUDDNode beliefWithPreAndPssa = CUDD.Function.And(beliefWithPre, partialSsa);
            CUDDVars oldVars = new CUDDVars();
            CUDDVars newVars = new CUDDVars();

            foreach (var predicate in affectedPredSet)
            {
                CUDDNode trueRestrictBy = CUDD.Var(predicate.PreviousCuddIndex);
                CUDD.Ref(trueRestrictBy);
                CUDD.Ref(beliefWithPreAndPssa);
                CUDDNode trueNode = CUDD.Function.Restrict(beliefWithPreAndPssa, trueRestrictBy);
                CUDDNode falseRestrictBy = CUDD.Function.Not(trueRestrictBy);
                CUDDNode falseNode = CUDD.Function.Restrict(beliefWithPreAndPssa, falseRestrictBy);
                beliefWithPreAndPssa = CUDD.Function.Or(trueNode, falseNode);

                oldVars.AddVar(CUDD.Var(predicate.SuccessiveCuddIndex));
                newVars.AddVar(CUDD.Var(predicate.PreviousCuddIndex));
            }

            Belief = CUDD.Variable.SwapVariables(beliefWithPreAndPssa, oldVars, newVars);
        }

        public bool Implies(PlanningParser.SubjectGdContext context, StringDictionary assignment)
        {
            bool result;
            if (context.KNOW() != null)
            {
                CUDDNode objectNode = context.gd().GetCuddNode(_predicateDict, assignment);
                CUDD.Ref(Knowledge);
                CUDDNode impliesNode = CUDD.Function.Implies(Knowledge, objectNode);
                result = impliesNode.Equals(CUDD.ONE);
            }
            else if (context.BEL() != null)
            {
                CUDDNode objectNode = context.gd().GetCuddNode(_predicateDict, assignment);
                CUDD.Ref(Belief);
                CUDDNode impliesNode = CUDD.Function.Implies(Belief, objectNode);
                result = impliesNode.Equals(CUDD.ONE);
            }
            else if (context.NOT() != null)
            {
                result = !Implies(context.subjectGd(0), assignment);
            }
            else if (context.AND() != null)
            {
                result = true;
                for (int i = 0; i < context.subjectGd().Count; i++)
                {
                    result &= Implies(context.subjectGd(i), assignment);
                    if (!result)
                    {
                        break;
                    }
                }
            }
            else if (context.OR() != null)
            {
                result = false;
                for (int i = 0; i < context.subjectGd().Count; i++)
                {
                    result |= Implies(context.subjectGd(i), assignment);
                    if (result)
                    {
                        break;
                    }
                }
            }
            else
            {
                var listVariableContext = context.listVariable();
                var collection = listVariableContext.GetCollection();
                var varNameList = listVariableContext.GetVariableNameList();
                bool isForall = context.FORALL() != null;
                result = RecursiveScanMixedRaio(context.subjectGd(0), varNameList, collection, assignment, 0, isForall);
            }

            return result;
        }

        private bool RecursiveScanMixedRaio(PlanningParser.SubjectGdContext context, IReadOnlyList<string> variableNameList,
            IReadOnlyList<IList<string>> collection, StringDictionary assignment, int currentLevel = 0,
            bool isForall = true)
        {
            bool result;
            if (currentLevel != variableNameList.Count)
            {
                string variableName = variableNameList[currentLevel];
                result = isForall;

                bool terminateCondition = !isForall;
                Func<bool, bool, bool> boolFunc;
                if (isForall)
                {
                    boolFunc = (b1, b2) => b1 && b2;
                }
                else
                {
                    boolFunc = (b1, b2) => b1 || b2;
                }

                foreach (string value in collection[currentLevel])
                {
                    assignment[variableName] = value;

                    bool gdResult = RecursiveScanMixedRaio(context, variableNameList, collection,
                        assignment,
                        currentLevel + 1, isForall);

                    if (gdResult == terminateCondition)
                    {
                        result = terminateCondition;
                        break;
                    }

                    result = boolFunc(result, gdResult);
                }
            }
            else
            {
                result = Implies(context, assignment);
            }

            return result;
        }

        public void ShowInfo()
        {
            Console.WriteLine("Init knowledge:");
            CUDD.Print.PrintMinterm(Knowledge);

            Console.WriteLine("Init belief:");
            CUDD.Print.PrintMinterm(Belief);
        }

        //private CUDDNode GetCuddNode(HighLevelProgramParser.ObjectFormulaContext context)
        //{
        //    CUDDNode result = null;
        //    if (context.AND() != null)
        //    {
        //        CUDDNode leftNode = GetCuddNode(context.objectFormula()[0]);
        //        CUDDNode rightNode = GetCuddNode(context.objectFormula()[0]);
        //        result = CUDD.Function.And(leftNode, rightNode);
        //        CUDD.Deref(leftNode);
        //        CUDD.Deref(rightNode);
        //    }
        //    else if (context.OR() != null)
        //    {
        //        CUDDNode leftNode = GetCuddNode(context.objectFormula()[0]);
        //        CUDDNode rightNode = GetCuddNode(context.objectFormula()[0]);
        //        result = CUDD.Function.Or(leftNode, rightNode);
        //        CUDD.Deref(leftNode);
        //        CUDD.Deref(rightNode);
        //    }
        //    else if (context.NOT() != null)
        //    {
        //        CUDDNode node = GetCuddNode(context.objectFormula()[0]);
        //        result = CUDD.Function.Not(node);
        //        CUDD.Deref(node);
        //    }
        //    else if (context.LB() != null)
        //    {
        //        result = GetCuddNode(context.objectFormula()[0]);
        //    }
        //    else if (context.predicate() != null)
        //    {
        //        result = GetCuddNode(context.predicate());
        //    }

        //    CUDD.Ref(result);
        //    return result;
        //}

        //private CUDDNode GetCuddNode(HighLevelProgramParser.PredicateContext context)
        //{
        //    string fullName = GetFullName(context);
        //    int index = _problem.GroundPredicateDict[fullName].PreviousCuddIndex;
        //    CUDDNode result = CUDD.Var(index);
        //    return result;
        //}

        //private string GetFullName(HighLevelProgramParser.PredicateContext context)
        //{
        //    string name = context.NAME().GetText();
        //    List<string> constantList = new List<string>();
        //    HighLevelProgramParser.ListNameContext listNameContext = context.listName();
        //    while (listNameContext != null)
        //    {
        //        string constant = context.listName().NAME().GetText();
        //        constantList.Add(constant);
        //        listNameContext = listNameContext.listName();
        //    }
        //    string result = VariableContainer.GetFullName(name, constantList);
        //    return result;

        //}

        #endregion
    }
}
