﻿using System;
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
            //CUDD.Print.PrintMinterm(problem.InitKnowledge);
            //CUDD.Print.PrintMinterm(problem.InitBelief);

            Knowledge = problem.InitKnowledge;
            Belief = problem.InitBelief;
            _predicateDict = problem.PredicateDict;
        }

        #endregion

        #region Methods

        public void Update(Response response)
        {
            Update(response.EventModel);
        }

        public void Update(Observation observation)
        {
            CUDD.Ref(observation.Precondition);
            Knowledge = CUDD.Function.And(Knowledge, observation.Precondition);
            
            CUDD.Ref(observation.Precondition);
            CUDD.Ref(Belief);
            CUDDNode negPrecondition = CUDD.Function.Not(observation.Precondition);
            CUDDNode impliesNode = CUDD.Function.Implies(Belief, negPrecondition);
            if (!impliesNode.Equals(CUDD.ONE))
            {
                CUDD.Ref(observation.Precondition);
                Belief = CUDD.Function.And(Belief, observation.Precondition);
            }
            else
            {
                CUDD.Deref(Belief);
                Belief = Knowledge;
                CUDD.Ref(Belief);
            }
            CUDD.Deref(impliesNode);

            Update(observation.EventModel);
        }

        private void Update(EventModel eventModel)
        {
            CUDD.Ref(Belief);
            CUDD.Ref(eventModel.BelievePrecondition);
            CUDDNode impliesNode = CUDD.Function.Implies(Belief, CUDD.Function.Not(eventModel.BelievePrecondition));

            if (!impliesNode.Equals(CUDD.ONE))
            {
                UpdateBelief(eventModel.BelievePartialSsa, eventModel.BelieveAffectedPredSet);
                UpdateKnowledge(eventModel);
                return;
            }

            CUDD.Ref(Belief);
            CUDD.Ref(eventModel.KnowPrecondition);
            impliesNode = CUDD.Function.Implies(Belief, CUDD.Function.Not(eventModel.KnowPrecondition));
            if (!impliesNode.Equals(CUDD.ONE))
            {
                UpdateBelief(eventModel.KnowPartialSsa, eventModel.KnowAffectedPredSet);
                UpdateKnowledge(eventModel);
                return;
            }

            CUDD.Deref(Belief);
            CUDD.Ref(Knowledge);
            CUDD.Ref(eventModel.BelievePrecondition);
            impliesNode = CUDD.Function.Implies(Knowledge, CUDD.Function.Not(eventModel.BelievePrecondition));
            if (!impliesNode.Equals(CUDD.ONE))
            {
                Belief = Knowledge;
                CUDD.Ref(Belief);
                UpdateBelief(eventModel.BelievePartialSsa, eventModel.BelieveAffectedPredSet);
                UpdateKnowledge(eventModel);
                return;
            }

            UpdateKnowledge(eventModel);
            Belief = Knowledge;
            CUDD.Ref(Belief);
        }

        private void UpdateKnowledge(EventModel eventModel)
        {
            //Console.WriteLine("Enter update knowledge");

            //CUDD.Ref(eventModel.KnowPrecondition);
            //Console.WriteLine("After ref know precondition");

            //CUDDNode knowledgeWithPre = CUDD.Function.And(Knowledge, eventModel.KnowPrecondition);
            CUDD.Ref(eventModel.KnowPartialSsa);
            //Console.WriteLine("After ref know partial ssa");

            CUDDNode knowledgeWithPssa = CUDD.Function.And(Knowledge, eventModel.KnowPartialSsa);

            //Console.WriteLine("After get knowledge with pre and partial ssa");

            if (eventModel.KnowAffectedPredSet.Count != 0)
            {
                CUDDVars oldVars = new CUDDVars();
                CUDDVars newVars = new CUDDVars();

                foreach (var predicate in eventModel.KnowAffectedPredSet)
                {
                    CUDDNode trueRestrictBy = CUDD.Var(predicate.PreviousCuddIndex);
                    CUDD.Ref(trueRestrictBy);
                    CUDD.Ref(knowledgeWithPssa);
                    CUDDNode trueNode = CUDD.Function.Restrict(knowledgeWithPssa, trueRestrictBy);
                    CUDDNode falseRestrictBy = CUDD.Function.Not(trueRestrictBy);
                    CUDDNode falseNode = CUDD.Function.Restrict(knowledgeWithPssa, falseRestrictBy);

                    knowledgeWithPssa = CUDD.Function.Or(trueNode, falseNode);

                    oldVars.AddVar(CUDD.Var(predicate.SuccessiveCuddIndex));
                    newVars.AddVar(CUDD.Var(predicate.PreviousCuddIndex));
                }

                Knowledge = CUDD.Variable.SwapVariables(knowledgeWithPssa, oldVars, newVars);

                oldVars.Deref();
                newVars.Deref();
            }
            else
            {
                Knowledge = knowledgeWithPssa;
            }
            //Console.WriteLine("Whether knowledge is equal to false: {0}", Knowledge.Equals(CUDD.ZERO));
            //Console.WriteLine("Finish!");
        }

        private void UpdateBelief(CUDDNode partialSsa, HashSet<Predicate> affectedPredSet)
        {
            //CUDD.Ref(precondition);
            //CUDDNode beliefWithPre = CUDD.Function.And(Belief, precondition);
            CUDD.Ref(partialSsa);
            CUDDNode beliefWithPssa = CUDD.Function.And(Belief, partialSsa);

            if (affectedPredSet.Count != 0)
            {
                CUDDVars oldVars = new CUDDVars();
                CUDDVars newVars = new CUDDVars();

                foreach (var predicate in affectedPredSet)
                {
                    CUDDNode trueRestrictBy = CUDD.Var(predicate.PreviousCuddIndex);
                    CUDD.Ref(trueRestrictBy);
                    CUDD.Ref(beliefWithPssa);
                    CUDDNode trueNode = CUDD.Function.Restrict(beliefWithPssa, trueRestrictBy);
                    CUDDNode falseRestrictBy = CUDD.Function.Not(trueRestrictBy);
                    CUDDNode falseNode = CUDD.Function.Restrict(beliefWithPssa, falseRestrictBy);
                    beliefWithPssa = CUDD.Function.Or(trueNode, falseNode);

                    oldVars.AddVar(CUDD.Var(predicate.SuccessiveCuddIndex));
                    newVars.AddVar(CUDD.Var(predicate.PreviousCuddIndex));
                }
                Belief = CUDD.Variable.SwapVariables(beliefWithPssa, oldVars, newVars);

                oldVars.Deref();
                newVars.Deref();
            }
            else
            {
                Belief = beliefWithPssa;
            }

            //Console.WriteLine("Whether belief is equal to false: {0}", Belief.Equals(CUDD.ZERO));
            //Console.WriteLine("Finish!");
        }

        public bool Implies(PlanningParser.SubjectGdContext context)
        {
            StringDictionary emtpyAssignment = new StringDictionary();
            return Implies(context, emtpyAssignment);
        }

        public bool Implies(PlanningParser.SubjectGdContext context, StringDictionary assignment)
        {
            //Console.WriteLine("Enter implies");
            bool result;
            if (context.KNOW() != null)
            {
                //Console.WriteLine("Enter Know");
                //CUDD.Print.PrintMinterm(Knowledge);
                //Console.WriteLine("Whether knowledge is equal to false: {0}", Knowledge.Equals(CUDD.ZERO));
                CUDDNode objectNode = context.gd().GetCuddNode(_predicateDict, assignment);
                //CUDD.Print.PrintMinterm(objectNode);
                CUDD.Ref(Knowledge);
                CUDDNode impliesNode = CUDD.Function.Implies(Knowledge, objectNode);
                result = impliesNode.Equals(CUDD.ONE);
            }
            else if (context.BEL() != null)
            {
                //Console.WriteLine("Enter belief");
                //Console.WriteLine("Belief");
                //CUDD.Print.PrintMinterm(Belief);
                CUDDNode objectNode = context.gd().GetCuddNode(_predicateDict, assignment);
                //Console.WriteLine("objectNode");
                //Console.WriteLine(context.gd().GetText());
                //CUDD.Print.PrintMinterm(objectNode);
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
                    if (!Implies(context.subjectGd(i), assignment))
                    {
                        result = false;
                        break;
                    }
                }
            }
            else if (context.OR() != null)
            {
                result = false;
                for (int i = 0; i < context.subjectGd().Count; i++)
                {
                    if (Implies(context.subjectGd(i), assignment))
                    {
                        result = true;
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

            //Console.WriteLine("Implies result: {0}", result);
            //Console.ReadLine();
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

        #endregion
    }
}
