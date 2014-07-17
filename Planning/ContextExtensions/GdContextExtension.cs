using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageRecognition;
using PAT.Common.Classes.CUDDLib;

namespace Planning.ContextExtensions
{
    public static class GdContextExtension
    {
        #region Methods

        public static CUDDNode GetCuddNode(this PlanningParser.GdContext context,
            IReadOnlyDictionary<string, Predicate> predicateDict, StringDictionary assignment)
        {
            CUDDNode result;
            if (context.termAtomForm() != null)
            {
                result = GetCuddNode(context.termAtomForm(), predicateDict, assignment);
            }
            else if (context.AND() != null)
            {
                var gdContextList = context.gd();
                result = GetCuddNode(gdContextList[0], predicateDict, assignment);

                if (!result.Equals(CUDD.ZERO))
                {
                    for (int i = 1; i < gdContextList.Count; i++)
                    {
                        CUDDNode gdNode = GetCuddNode(gdContextList[i], predicateDict, assignment);
                        if (gdNode.Equals(CUDD.ZERO))
                        {
                            CUDD.Deref(result);
                            result = CUDD.ZERO;
                            break;
                        }
                        result = CUDD.Function.And(result, gdNode); 
                    }
                }
            }
            else if (context.OR() != null)
            {
                var gdContextList = context.gd();
                result = GetCuddNode(gdContextList[0], predicateDict, assignment);

                if (!result.Equals(CUDD.ONE))
                {
                    for (int i = 1; i < gdContextList.Count; i++)
                    {
                        CUDDNode gdNode = GetCuddNode(gdContextList[i], predicateDict, assignment);
                        if (gdNode.Equals(CUDD.ONE))
                        {
                            CUDD.Deref(result);
                            result = CUDD.ZERO;
                            break;
                        }
                        result = CUDD.Function.Or(result, gdNode);
                    }
                }
            }
            else if (context.NOT() != null)
            {
                CUDDNode gdNode = GetCuddNode(context.gd()[0], predicateDict, assignment);
                result = CUDD.Function.Not(gdNode);
            }
            else if (context.IMPLY() != null)
            {
                var gdContextList = context.gd();
                CUDDNode gdNode0 = GetCuddNode(gdContextList[0], predicateDict, assignment);
                CUDDNode gdNode1 = GetCuddNode(gdContextList[1], predicateDict, assignment);
                result = CUDD.Function.Implies(gdNode0, gdNode1);
            }
            else
            {
                var listVariableContext = context.listVariable();
                var collection = listVariableContext.GetCollection();
                var varNameList = listVariableContext.GetVariableNameList();
                bool isForall = context.FORALL() != null;
                result = RecursiveScanMixedRaio(context.gd(0), predicateDict, varNameList, collection, assignment, 0, isForall);
            }

            return result;
        }

        private static CUDDNode GetCuddNode(this PlanningParser.TermAtomFormContext context,
            IReadOnlyDictionary<string, Predicate> predicateDict, StringDictionary assignment)
        {
            CUDDNode result;
            if (context.predicate() != null)
            {
                string predicateFullName = ConstContainer.GetFullName(context, assignment);
                //Console.WriteLine(predicateFullName);
                Predicate predicate = predicateDict[predicateFullName];
                int cuddIndex = predicate.PreviousCuddIndex;
                result = CUDD.Var(cuddIndex);
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

        private static CUDDNode RecursiveScanMixedRaio(PlanningParser.GdContext context,
            IReadOnlyDictionary<string, Predicate> predicateDict, IReadOnlyList<string> variableNameList,
            IReadOnlyList<IList<string>> collection, StringDictionary assignment, int currentLevel = 0,
            bool isForall = true)
        {
            CUDDNode result;
            if (currentLevel != variableNameList.Count)
            {
                string variableName = variableNameList[currentLevel];
                result = isForall ? CUDD.ONE : CUDD.ZERO;
                CUDD.Ref(result);

                CUDDNode equalNode = isForall ? CUDD.ZERO : CUDD.ONE;
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

                    CUDDNode gdNode = RecursiveScanMixedRaio(context, predicateDict, variableNameList, collection,
                        assignment,
                        currentLevel + 1, isForall);

                    if (gdNode.Equals(equalNode))
                    {
                        CUDD.Deref(result);
                        result = equalNode;
                        break;
                    }

                    result = boolFunc(result, gdNode);
                }
            }
            else
            {
                result = GetCuddNode(context, predicateDict, assignment);
            }

            return result;
        }

        #endregion
    }
}